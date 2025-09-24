using RichtofenSurvivor.EntityStates;

internal class WeaponSniper : WeaponBase
{
    public WeaponSniper()
    {
        WeaponName = "Barrett .50cal";
        MaxAmmo = 40;
        CurrentAmmo = MaxAmmo - MagazineSize;
        MagazineSize = 5;
        MagazineAmmo = MagazineSize;
        ReloadTime = 2.5f;
        FireRate = 1.0f;
        Damage = 8.5f; //850% damage
        WeaponState = typeof(TestSniperState);
    }
}