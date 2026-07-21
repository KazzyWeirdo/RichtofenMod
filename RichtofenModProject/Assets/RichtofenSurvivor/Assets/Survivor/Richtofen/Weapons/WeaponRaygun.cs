using RoR2.Projectile;
using R2API;
using RichtofenSurvivor;
using RichtofenSurvivor.EntityStates;
using UnityEngine;

public class WeaponRaygun : WeaponBase
{
    public static GameObject raygunProjectilePrefab;

    private static void CreateRaygunProjectile()
    {

        raygunProjectilePrefab = PrefabAPI.InstantiateClone(Resources.Load<GameObject>("Prefabs/Projectiles/MageFirebolt"), "RaygunProjectile", true);

        UnityEngine.Object.Destroy(raygunProjectilePrefab.GetComponent<ProjectileImpactExplosion>());
        ProjectileImpactExplosion raygunImpactExplosion = raygunProjectilePrefab.AddComponent<ProjectileImpactExplosion>();

        raygunImpactExplosion.blastRadius = 6.5f;
        raygunImpactExplosion.blastDamageCoefficient = 0.9f;
        raygunImpactExplosion.falloffModel = RoR2.BlastAttack.FalloffModel.None;
        raygunImpactExplosion.destroyOnEnemy = true;
        //raygunImpactExplosion.destroyOnWorld = true;
        raygunImpactExplosion.lifetime = 12f;
        raygunImpactExplosion.impactEffect = Resources.Load<GameObject>("Prefabs/Effects/ImpactEffects/ExplosionVFX");
        raygunImpactExplosion.lifetimeExpiredSound = Resources.Load<RoR2.NetworkSoundEventDef>("NetworkSoundEventDefs/sfx_mage_missile_explode");
        raygunImpactExplosion.timerAfterImpact = true;
        raygunImpactExplosion.lifetimeAfterImpact = 0.1f;
        

    }

    public static GameObject GetRaygunProjectilePrefab()
    {
        return raygunProjectilePrefab;
    }
    public WeaponRaygun()
    {
        CreateRaygunProjectile();
        WeaponName = "Raygun";
        MaxAmmo = 180;
        CurrentAmmo = MaxAmmo - MagazineSize;
        MagazineSize = 20;
        MagazineAmmo = MagazineSize;
        ReloadTime = 2.5f;
        FireRate = 0.6f;
        Damage = 1.25f;
        //Damage = 1.1f; //110% damage
        WeaponState = typeof(TestRaygunState);
        //WeaponItem = RichtofenSurvivorContent.readOnlyContentPack.itemDefs.Find("PistolItemDef");
    }
}
