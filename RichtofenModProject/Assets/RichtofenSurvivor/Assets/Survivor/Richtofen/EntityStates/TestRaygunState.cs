using EntityStates;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using RoR2;
//Since we are using effects from Commando's Barrage skill, we will also be using the associated namespace
//You can also use Addressables or LegacyResourcesAPI to load whichever effects you like
using EntityStates.Commando.CommandoWeapon;
using RichtofenSurvivor;
using RoR2.Projectile;


namespace RichtofenSurvivor.EntityStates
{
    public class TestRaygunState : BaseSkillState
    {

        public float baseDuration = 0.1f;
        private float duration;

        private readonly WeaponRaygun weapon = new();

        public override void OnEnter()
        {
            base.OnEnter();
            baseDuration = weapon.FireRate;
            duration = baseDuration / attackSpeedStat;
            Ray aimRay = GetAimRay();
            StartAimMode(aimRay, 2f, false);
            base.PlayAnimation("Gesture Additive, Right", "FirePistol, Right");
            Util.PlaySound(FireBarrage.fireBarrageSoundString, gameObject);
            //AddRecoil(-0.6f, 0.6f, -0.6f, 0.6f);
            if (FireBarrage.effectPrefab)
            {
                EffectManager.SimpleMuzzleFlash(FireBarrage.effectPrefab, gameObject, "MuzzleRight", false);
            }

            if (isAuthority)
            {

                FireProjectileInfo fireProjectileInfo = new()
                {
                    projectilePrefab = WeaponRaygun.GetRaygunProjectilePrefab(),
                    position = aimRay.origin,
                    rotation = Util.QuaternionSafeLookRotation(aimRay.direction),
                    owner = base.gameObject,
                    damage = weapon.Damage * this.damageStat,
                    force = 0f,
                    damageTypeOverride = new DamageTypeCombo?(DamageType.Generic),
                };
                ProjectileManager.instance.FireProjectile(fireProjectileInfo);
                RichtofenSurvivorMain.LogInfo("Test base: " + base.ToString());
            }

        }

        public override void OnExit()
        {
            base.OnExit();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (fixedAge >= duration && isAuthority)
            {
                outer.SetNextStateToMain();
                return;
            }
        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.Skill;
        }

    }
}
