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


namespace RichtofenSurvivor.EntityStates
{
    public class TestShotgunState : BaseSkillState
    {

        public float baseDuration;
        private float duration;

        public GameObject hitEffectPrefab = FireBarrage.hitEffectPrefab;
        public GameObject tracerEffectPrefab = FireBarrage.tracerEffectPrefab;
        private readonly WeaponShotgun weapon = new();

        public override void OnEnter()
        {
            base.OnEnter();
            baseDuration = weapon.FireRate;
            duration = baseDuration / attackSpeedStat;
            Ray aimRay = GetAimRay();
            StartAimMode(aimRay, 2f, false);
            base.PlayAnimation("Gesture Additive, Right", "FirePistol, Right");
            Util.PlaySound(FireBarrage.fireBarrageSoundString, gameObject);
            AddRecoil(-0.6f, 0.6f, -0.6f, 0.6f);
            if (FireBarrage.effectPrefab)
            {
                EffectManager.SimpleMuzzleFlash(FireBarrage.effectPrefab, gameObject, "MuzzleRight", false);
            }

            if (isAuthority)
            {



                new BulletAttack
                {
                    owner = gameObject,
                    weapon = gameObject,
                    origin = aimRay.origin,
                    aimVector = aimRay.direction,
                    minSpread = 0.8f,
                    maxSpread = 2.5f,
                    bulletCount = 8U,
                    procCoefficient = 1f,
                    damage = base.characterBody.damage * weapon.Damage,
                    force = 3,
                    falloffModel = BulletAttack.FalloffModel.DefaultBullet,
                    tracerEffectPrefab = tracerEffectPrefab,
                    muzzleName = "MuzzleRight",
                    hitEffectPrefab = hitEffectPrefab,
                    isCrit = false,
                    HitEffectNormal = false,
                    stopperMask = LayerIndex.world.mask,
                    smartCollision = true,
                    maxDistance = 30000f,
                    sniper = false,
                    allowTrajectoryAimAssist = true,
                    damageType = DamageType.Generic,



                }.Fire();

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
