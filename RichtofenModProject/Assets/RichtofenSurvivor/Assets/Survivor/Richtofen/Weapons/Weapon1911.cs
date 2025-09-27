using RichtofenSurvivor;
using RichtofenSurvivor.EntityStates;

public class Weapon1911 : WeaponBase
{
    public Weapon1911()
    {
        WeaponName = "M1911";
        MaxAmmo = 80;
        CurrentAmmo = MaxAmmo - MagazineSize;
        MagazineSize = 8;
        MagazineAmmo = MagazineSize;
        ReloadTime = 2.5f;
        FireRate = 0.3f;
        Damage = 1000f;
        //Damage = 1.1f; //110% damage
        WeaponState = typeof(TestPistolState);
        weaponItem = RichtofenSurvivorContent.readOnlyContentPack.itemDefs.Find("PistolItemDef");
    }
}

//using RichtofenSurvivor.EntityStates;
//using System;
// class Weapon1911 : WeaponBase, IWeapon
//{
//    string WeaponName { get; } = "M1911";

//    string IWeapon.WeaponName => WeaponName;

//    uint IWeapon.MaxAmmo { get; } = 80;
//    uint IWeapon.CurrentAmmo { get; set; } = 72;
//    uint IWeapon.MagazineSize { get; } = 8;
//    public uint MagazineAmmo { get; set; } = 8;
//    float IWeapon.ReloadTime { get; } = 2.5f;
//    float IWeapon.FireRate { get; } = 0.3f;
//    public float Damage { get; } = 1000f;
//    Type IWeapon.WeaponState { get; } = typeof(TestPistolState);



//}