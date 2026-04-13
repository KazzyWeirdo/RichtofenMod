using RichtofenSurvivor;
using RoR2;
using RoR2.ContentManagement;
using RoR2.Stats;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WeaponInventory : MonoBehaviour
{
    private static WeaponBase[] weapons = new WeaponBase[2];

    public static WeaponBase activeWeapon;
    public static WeaponBase secondaryWeapon;
    private CharacterBody cb;
    private Inventory inventory;
    

    public static void SwapWeapons()
    {
        (activeWeapon, secondaryWeapon) = (secondaryWeapon, activeWeapon);
    }

    public void Awake()
    {
        WeaponItemDictionary.Init();
        //importante para cuando start se llame ya tenga el cbody
        cb = base.GetComponent<CharacterBody>();
        inventory = cb.inventory;

    }

    public void TryGiveItem()
    {
        //odio tener que copiarlo del capitan pero es lo que hay :^)
        if (cb.master)
        {
            bool flag = false;
            if (cb.master.playerStatsComponent)
            {
                flag = (cb.master.playerStatsComponent.currentStats.GetStatValueDouble(PerBodyStatDef.totalTimeAlive, BodyCatalog.GetBodyName(cb.bodyIndex)) > 0.0);
            }
            if (!flag && cb.master.inventory.GetItemCountPermanent(RichtofenSurvivorContent.readOnlyContentPack.itemDefs.Find("PistolItemDef")) <= 0)
            {
                RichtofenSurvivorMain.LogInfo("se puede dar item solo al principio");
                cb.master.inventory.GiveItemPermanent(RichtofenSurvivorContent.readOnlyContentPack.itemDefs.Find("PistolItemDef"), 1);
            }
        }
    }

    public void Start()
    {
        if (NetworkServer.active)
        {
            TryGiveItem();
        }
    }

    public static void Run_Start(On.RoR2.Run.orig_Start orig, Run self)
    {
        weapons[0] = new Weapon1911();
        //weapons[1] = new WeaponSniper();
        activeWeapon = weapons[0];
        
        //secondaryWeapon = weapons[1];
        orig(self);
    }

    public static void WeaponCheck(On.RoR2.CharacterBody.orig_OnInventoryChanged orig, CharacterBody self)
    {
        if(self && self.inventory && self.bodyIndex == BodyCatalog.FindBodyIndex("RichtofenBody"))
        {
            var inv = self.inventory;
            var newItem = ItemCatalog.GetItemDef(inv.itemAcquisitionOrder[^1]);
            var dictionary = WeaponItemDictionary.GetWeaponItemDictionary();
            if (dictionary.ContainsKey(newItem))
            {
                RichtofenSurvivorMain.LogInfo(newItem.ToString() + ": NEW ITEM, property test - " + newItem.nameToken);
                weapons[1] = (WeaponBase)Activator.CreateInstance(dictionary[newItem]);
                secondaryWeapon = weapons[1];
                RichtofenSurvivorMain.LogInfo("Secondary Weapon Added & Updated: " + secondaryWeapon.WeaponName);
                //hello
            }
        }
        //if (self && self.inventory && self.bodyIndex == BodyCatalog.FindBodyIndex("RichtofenBody"))
        //{
        //    var inv = self.inventory;
        //    foreach (var w in weapons)
        //    {
        //        int itemCount = inv.GetItemCount(w.weaponItem);
        //        if (itemCount > 0)
        //        {
        //            if (w.CurrentAmmo <= 0 && w.MagazineAmmo <= 0)
        //            {
        //                w.CurrentAmmo = w.MaxAmmo;
        //                w.MagazineAmmo = w.MagazineSize;
        //            }
        //        }
        //        else
        //        {
        //            w.CurrentAmmo = 0;
        //            w.MagazineAmmo = 0;
        //        }
        //    }
        //    //RichtofenSurvivorMain.LogInfo("Weapon Check: " + activeWeapon.WeaponName + " Ammo: " + activeWeapon.CurrentAmmo + "/" + activeWeapon.MagazineAmmo);
        //}
        orig(self);
    }

    public static void Start(On.RoR2.CharacterBody.orig_Start orig, CharacterBody self)
    {
        orig(self); // ALWAYS first
        //var wepinv = WeaponItemDictionary.GetWeaponItemDictionary();
        if (!NetworkServer.active) return;

        if (self.bodyIndex != BodyCatalog.FindBodyIndex("RichtofenBody")) return;

        weapons[0] = new Weapon1911();
        activeWeapon = weapons[0];
        var inventory = self.inventory;
        if (!inventory) return;
        
        

        if (inventory.GetItemCountPermanent(activeWeapon.WeaponItem) > 0) return;

        inventory.GiveItemPermanent(activeWeapon.WeaponItem, 1);
    }
    public static void RegisterInventoryHooks()
    { 
        On.RoR2.Run.Start += Run_Start;
        On.RoR2.CharacterBody.Start += Start;
        On.RoR2.CharacterBody.OnInventoryChanged += WeaponCheck;
        RichtofenSurvivorMain.LogInfo("Registered Inventory Hooks");

    }


}
