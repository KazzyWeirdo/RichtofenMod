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
    public static bool runStart;

    [ConCommand(commandName = "giveweapon", helpText = "giveweapon (rifle, pistol, shotgun, sniper, raygun)", flags = ConVarFlags.None)]
    public static void GiveWeapon(ConCommandArgs args)
    {
        // do whatever
        if (args.sender != null)
        {
            var arg = args.Count > 0 ? args[0] : null;
            switch (arg)
            {
                case "rifle":
                    args.senderBody.GetComponent<WeaponInventory>().GiveWeapon(new WeaponRifle());
                    break;
                case "pistol":
                    args.senderBody.GetComponent<WeaponInventory>().GiveWeapon(new Weapon1911());
                    break;
                case "shotgun":
                    args.senderBody.GetComponent<WeaponInventory>().GiveWeapon(new WeaponShotgun());
                    break;
                case "sniper":
                    args.senderBody.GetComponent<WeaponInventory>().GiveWeapon(new WeaponSniper());
                    break;
                case "raygun":
                    args.senderBody.GetComponent<WeaponInventory>().GiveWeapon(new WeaponRaygun());
                    break;
                default:
                    RichtofenSurvivorMain.LogWarning("Unknown weapon type: " + arg);
                    break;
            }
        }
    }



    public static void SwapWeapons()
    {
        (activeWeapon, secondaryWeapon) = (secondaryWeapon, activeWeapon);
    }

    private void GiveWeapon (WeaponBase weapon)
    {
        if (weapons[1] == null)
        {
            weapons[1] = weapon;
            secondaryWeapon = weapon;
        } else
        {
            RichtofenSurvivorMain.LogWarning("Secondary weapon is not null, swapping active weapon with new weapon");
            activeWeapon = weapon;
        }
    }

    public void Awake()
    {
        WeaponItemDictionary.Init();
        //importante para cuando start se llame ya tenga el cbody
        cb = base.GetComponent<CharacterBody>();
        inventory = cb.inventory;

    }

    private void Update()
    {
        if(!NetworkServer.active) return;
        if (Input.GetKeyDown(RichtofenSurvivorMain.reloadKey.Value.MainKey))
        {
            RichtofenSurvivorMain.LogWarning("Reloading manually");
            activeWeapon.ReloadWeapon();
        }
    }

    //public void TryGiveItem()
    //{
    //    //odio tener que copiarlo del capitan pero es lo que hay :^)
    //    if (cb.master)
    //    {
    //        bool flag = false;
    //        if (cb.master.playerStatsComponent)
    //        {
    //            flag = (cb.master.playerStatsComponent.currentStats.GetStatValueDouble(PerBodyStatDef.totalTimeAlive, BodyCatalog.GetBodyName(cb.bodyIndex)) > 0.0);
    //        }
    //        if (!flag && cb.master.inventory.GetItemCountPermanent(RichtofenSurvivorContent.readOnlyContentPack.itemDefs.Find("PistolItemDef")) <= 0)
    //        {
    //            RichtofenSurvivorMain.LogInfo("Gave item from Captain code copypaste");
    //            runStart = true;
    //            cb.master.inventory.GiveItemPermanent(RichtofenSurvivorContent.readOnlyContentPack.itemDefs.Find("PistolItemDef"), 1);
    //        }
    //    }
    //}

    //public void Start()
    //{
    //    if (NetworkServer.active)
    //    {
    //        TryGiveItem();
    //    }
    //}

    public static void Run_Start(On.RoR2.Run.orig_Start orig, Run self)
    {
        weapons[0] = new Weapon1911();
        weapons[1] = null;
        //weapons[1] = new WeaponSniper();
        activeWeapon = weapons[0];
        RichtofenSurvivorMain.LogInfo("Run Start: Active Weapon: " + activeWeapon.WeaponName);
        //secondaryWeapon = weapons[1];
        orig(self);
    }

    //public static void WeaponCheck(On.RoR2.CharacterBody.orig_OnInventoryChanged orig, CharacterBody self)
    //{   
        
    //    if (self && self.inventory && self.bodyIndex == BodyCatalog.FindBodyIndex("RichtofenBody"))
    //    {
    //        var inv = self.inventory;
    //        var newItem = ItemCatalog.GetItemDef(inv.itemAcquisitionOrder[^1]);
    //        var dictionary = WeaponItemDictionary.GetWeaponItemDictionary();
    //        if (dictionary.ContainsKey(newItem))
    //        {
    //            if (weapons[1] != null)
    //            {
    //                inv.RemoveItemPermanent(activeWeapon.WeaponItem, 1);
    //                weapons[0] = (WeaponBase)Activator.CreateInstance(dictionary[newItem]);
    //                activeWeapon = weapons[0];

    //                RichtofenSurvivorMain.LogInfo("Swapped active weapon " + activeWeapon.WeaponName + " with " + newItem.nameToken);
    //            }
    //            else
    //            {
    //                RichtofenSurvivorMain.LogInfo(newItem.ToString() + ": NEW ITEM, property test - " + newItem.nameToken);
    //                weapons[1] = (WeaponBase)Activator.CreateInstance(dictionary[newItem]);
    //                secondaryWeapon = weapons[1];
    //                RichtofenSurvivorMain.LogInfo("Secondary Weapon Added & Updated: " + secondaryWeapon.WeaponName);
    //                //hello
    //            }
    //        }
    //    }
        
    //    orig(self);
    //}

    //public static void Start(On.RoR2.CharacterBody.orig_Start orig, CharacterBody self)
    //{
    //    orig(self); // ALWAYS first
    //    //var wepinv = WeaponItemDictionary.GetWeaponItemDictionary();
    //    if (!NetworkServer.active)
    //    {
    //        RichtofenSurvivorMain.LogInfo("No network server");
    //        return;
    //    }


    //    if (self.bodyIndex != BodyCatalog.FindBodyIndex("RichtofenBody")) {
    //        RichtofenSurvivorMain.LogInfo("Body is not richtofen");
    //        return; 
    //    }

    //    weapons[0] = new Weapon1911();
    //    activeWeapon = weapons[0];
        
    //}

    //public static void CheckItem(On.RoR2.Inventory.orig_GiveItemPermanent_ItemIndex_int orig, Inventory self, ItemIndex itemIndex, int count) {
        
    //    ItemDef item = ItemCatalog.GetItemDef(itemIndex);

    //    //RichtofenSurvivorMain.LogWarning("WEAPONS CURRENTLY" + weapons[0].WeaponName + ", " + weapons[1].WeaponName);

    //    CharacterMaster cm = self.GetComponent<CharacterMaster>();
    //    CharacterBody body = cm.GetBody();
    //    if (!cm) return;
    //    if (!body) return;
    //    if (body.bodyIndex != BodyCatalog.FindBodyIndex("RichtofenBody")) return;

    //    var dictionary = WeaponItemDictionary.GetWeaponItemDictionary();
    //    if (dictionary.ContainsKey(item) && !runStart) {
    //        if (weapons[1] == null)
    //        {
    //            weapons[1] = (WeaponBase)Activator.CreateInstance(dictionary[item]);
    //            secondaryWeapon = weapons[1];
    //            RichtofenSurvivorMain.LogWarning("From giveitem itemindex, second weapon is null, adding this --> " + dictionary[item] + ", " + weapons[1]);
    //        } else
    //        {
    //            if (weapons[0].WeaponItem == item || weapons[1].WeaponItem == item)
    //            {
    //                RichtofenSurvivorMain.LogWarning("From giveitem itemindex, player already has this weapon, not swapping --> " + dictionary[item]);
    //                return;
    //            }
    //            self.RemoveItemPermanent(activeWeapon.WeaponItem, 1);
    //            weapons[0] = (WeaponBase)Activator.CreateInstance(dictionary[item]);
    //            activeWeapon = weapons[0];
    //            RichtofenSurvivorMain.LogWarning("From giveitem itemindex, second weapon is not null, swapping current weapon --> " + activeWeapon.WeaponName + " <-> " + dictionary[item]);

    //        }
    //    }

    //    orig(self, itemIndex, count);
    //    runStart = false;
    //}

    //public static void CheckItemRemoved(On.RoR2.Inventory.orig_RemoveItemPermanent_ItemIndex_int orig, Inventory self, ItemIndex itemIndex, int count)
    //{
    //    ItemDef item = ItemCatalog.GetItemDef(itemIndex);
    //    CharacterMaster cm = self.GetComponent<CharacterMaster>();
    //    CharacterBody body = cm.GetBody();
    //    RichtofenSurvivorMain.LogDebug("CheckItemRemoed for item: " + item.nameToken + ", count: " + count);
    //    if (!cm) return;
    //    if (!body) return;
    //    if (body.bodyIndex != BodyCatalog.FindBodyIndex("RichtofenBody")) return;
    //    var dictionary = WeaponItemDictionary.GetWeaponItemDictionary();
    //    if (dictionary.ContainsKey(item))
    //    {
    //        if (weapons[0].WeaponItem == item)
    //        {
    //            RichtofenSurvivorMain.LogWarning("Removing active weapon: " + weapons[0].WeaponName);
    //            SwapWeapons();
    //            weapons[1] = null;
    //            secondaryWeapon = null;
    //        }
    //        else if (weapons[1] != null && weapons[1].WeaponItem == item)
    //        {
    //            RichtofenSurvivorMain.LogWarning("Removing secondary weapon: " + weapons[1].WeaponName);
    //            weapons[1] = null;
    //            secondaryWeapon = null;
    //        }
    //    }
    //    orig(self, itemIndex, count);
    //}
    public static void RegisterInventoryHooks()
    {
        On.RoR2.Run.Start += Run_Start;
        //On.RoR2.CharacterBody.Start += Start;
        //On.RoR2.Inventory.GiveItemPermanent_ItemIndex_int += CheckItem;
        //On.RoR2.Inventory.RemoveItemPermanent_ItemIndex_int += CheckItemRemoved;
        //On.RoR2.CharacterBody.OnInventoryChanged += WeaponCheck;
        RichtofenSurvivorMain.LogInfo("Registered Inventory Hooks");

    }


}
