using EntityStates;
using RichtofenSurvivor;
using RoR2;
using System;
using UnityEngine;

//public abstract class WeaponBase : IWeapon
//{
//    public string WeaponName { get; protected set; }
//    public Type WeaponState { get; protected set; }
//    public uint MaxAmmo { get; protected set; }
//    public uint CurrentAmmo { get; set; }
//    public uint MagazineSize { get; protected set; }
//    public uint MagazineAmmo { get; set; }
//    public float ReloadTime { get; protected set; }
//    public float FireRate { get; protected set; }
//    public float Damage { get; protected set; }

//    public virtual void ReloadWeapon()
//    {
//        if (CurrentAmmo > MaxAmmo) CurrentAmmo = MaxAmmo;
//        if (CurrentAmmo < MagazineSize)
//        {
//            MagazineAmmo = CurrentAmmo;
//            CurrentAmmo = 0;
//            return;
//        }
//        MagazineAmmo = MagazineSize;
//        CurrentAmmo -= MagazineSize;
//    }
//}

public abstract class WeaponBase
{
    public string WeaponName { get;  set; }
    public Type WeaponState { get;  set; }
    public uint MaxAmmo { get;  set; }
    public uint CurrentAmmo { get; set; }
    public uint MagazineSize { get; set; }
    public uint MagazineAmmo { get; set; }
    public float ReloadTime { get;  set; }
    public float FireRate { get;  set; }
    public float Damage { get;  set; }
    public ItemDef WeaponItem { get; set; }

    public void ReloadWeapon()
    {
        var ammoToReload = MagazineSize - MagazineAmmo;
        if (MagazineAmmo == MagazineSize) { 
            RichtofenSurvivorMain.LogWarning("mag full, NOT reloading");
            return; }
        if(CurrentAmmo==0) { RichtofenSurvivorMain.LogWarning("no ammo, NOT reloading"); return; }
        // ammo reserve nunca mayor que maxammo
        if (CurrentAmmo > MaxAmmo) CurrentAmmo = MaxAmmo;
        if (CurrentAmmo < ammoToReload)
        {
            MagazineAmmo += CurrentAmmo;
            CurrentAmmo = 0;
            return;
        }
        CurrentAmmo -= ammoToReload;
        MagazineAmmo += ammoToReload;
    }

}