using System;

public interface IWeapon
{
    string WeaponName { get; }
    uint MaxAmmo { get; }
    uint CurrentAmmo { get; set; }
    uint MagazineSize { get; }
    uint MagazineAmmo { get; set; }
    float ReloadTime { get; }
    float FireRate { get; }
    float Damage { get; }
    Type WeaponState { get; }
    void ReloadWeapon()
    {
        if (CurrentAmmo > MaxAmmo) CurrentAmmo = MaxAmmo;
        CurrentAmmo -= MagazineSize;
        MagazineAmmo = MagazineSize;
    }
}