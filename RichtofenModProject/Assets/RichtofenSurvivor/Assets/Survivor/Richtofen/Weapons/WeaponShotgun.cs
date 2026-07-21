using RichtofenSurvivor;
using RichtofenSurvivor.EntityStates;

public class WeaponShotgun : WeaponBase
{
    public WeaponShotgun()
    {
        WeaponName = "SPAS12";
        MaxAmmo = 32;
        CurrentAmmo = MaxAmmo - MagazineSize;
        MagazineSize = 8;
        MagazineAmmo = MagazineSize;
        ReloadTime = 2.5f;
        FireRate = 0.75f;
        Damage = 1.25f;
        WeaponState = typeof(TestShotgunState);
        //WeaponItem = RichtofenSurvivorContent.readOnlyContentPack.itemDefs.Find("PistolItemDef");
    }
}
