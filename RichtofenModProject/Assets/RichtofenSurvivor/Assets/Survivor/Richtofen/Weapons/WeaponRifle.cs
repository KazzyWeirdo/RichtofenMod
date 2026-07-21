using RichtofenSurvivor;
using RichtofenSurvivor.EntityStates;

public class WeaponRifle : WeaponBase
{
    public WeaponRifle()
    {
        WeaponName = "Galil";
        MaxAmmo = 315;
        CurrentAmmo = MaxAmmo - MagazineSize;
        MagazineSize = 35;
        MagazineAmmo = MagazineSize;
        ReloadTime = 2.5f;
        FireRate = 0.23f;
        Damage = 1.3f;
        //Damage = 1.1f; //110% damage
        WeaponState = typeof(TestRifleState);
        //WeaponItem = RichtofenSurvivorContent.readOnlyContentPack.itemDefs.Find("PistolItemDef");
    }
}
