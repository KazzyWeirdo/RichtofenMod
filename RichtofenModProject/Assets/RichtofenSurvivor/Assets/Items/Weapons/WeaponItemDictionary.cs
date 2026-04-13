using RichtofenSurvivor;
using RoR2;
using RoR2.ContentManagement;
using System;
using System.Collections.Generic;


public static class WeaponItemDictionary
{
    private static readonly Dictionary<ItemDef, Type> wepItemDic = new();
    
    public static void Init()
    {
        var contentPack = RichtofenSurvivorContent.readOnlyContentPack;
        wepItemDic.Add(contentPack.itemDefs.Find("PistolItemDef"), typeof(Weapon1911));
        wepItemDic.Add(contentPack.itemDefs.Find("SniperItemDef"), typeof(WeaponSniper));
    }

    public static Dictionary<ItemDef, Type> GetWeaponItemDictionary()
    {
        return wepItemDic;
    }

}
