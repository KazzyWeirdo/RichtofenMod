using RichtofenSurvivor;
using RoR2;
using RoR2.Stats;
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
            if (!flag && cb.master.inventory.GetItemCount(RichtofenSurvivorContent.readOnlyContentPack.itemDefs.Find("PistolItemDef")) <= 0)
            {
                RichtofenSurvivorMain.LogInfo("se puede dar item solo al principio");
                cb.master.inventory.GiveItem(RichtofenSurvivorContent.readOnlyContentPack.itemDefs.Find("PistolItemDef"), 1);
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
        weapons[1] = new WeaponSniper();
        activeWeapon = weapons[0];
        secondaryWeapon = weapons[1];
        orig(self);
    }

    public static void RegisterInventoryHooks()
    {
        On.RoR2.Run.Start += Run_Start;
        RichtofenSurvivorMain.LogInfo("Registered Inventory Hooks");

    }


}
