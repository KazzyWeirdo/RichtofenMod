using EntityStates;
using System;
using UnityEngine;

public abstract class WeaponBase : IWeapon
{
    public string WeaponName { get; protected set; }
    public Type WeaponState { get; protected set; }
    public uint MaxAmmo { get; protected set; }
    public uint CurrentAmmo { get; set; }
    public uint MagazineSize { get; protected set; }
    public uint MagazineAmmo { get; set; }
    public float ReloadTime { get; protected set; }
    public float FireRate { get; protected set; }
    public float Damage { get; protected set; }

    public virtual void ReloadWeapon()
    {
        if (CurrentAmmo > MaxAmmo) CurrentAmmo = MaxAmmo;
        MagazineAmmo = MagazineSize;
        CurrentAmmo -= MagazineSize;
    }
}
