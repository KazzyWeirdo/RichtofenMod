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
        WeaponItem = RichtofenSurvivorContent.readOnlyContentPack.itemDefs.Find("PistolItemDef");
    }
}
