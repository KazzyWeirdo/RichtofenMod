using RichtofenSurvivor;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace PowerUp
{
    public class InstantKillBehaviour
    {
        private static ItemDef instantKillItemDef;
        private static BuffDef instantKillBuffDef;

        public static void RegisterHooks()
        {
            instantKillItemDef = RichtofenSurvivorContent.readOnlyContentPack.itemDefs.Find("InstantKillItem");
            instantKillBuffDef = RichtofenSurvivorContent.readOnlyContentPack.buffDefs.Find("InstantKillBuff");

            On.RoR2.CharacterBody.OnInventoryChanged += OnInventoryChangedHook;
            On.RoR2.GlobalEventManager.OnHitEnemy += OnHitEnemyHook;

        }

        private static void OnHitEnemyHook(On.RoR2.GlobalEventManager.orig_OnHitEnemy orig, GlobalEventManager self, DamageInfo damage, GameObject victim)
        {
            if (!victim || !damage.attacker) return;

            CharacterBody attackerBody = damage.attacker.GetComponent<CharacterBody>();
            CharacterBody victimBody = victim.GetComponent<CharacterBody>();

            if (!attackerBody || !victimBody || victimBody.isPlayerControlled) return;


            if (attackerBody.HasBuff(instantKillBuffDef))
            {

                if (NetworkServer.active)
                {
                    HealthComponent victimHealth = victimBody.healthComponent;

                    if (victimHealth && victimHealth.alive)
                    {
                        if (victimBody.isBoss || victimBody.isElite)
                        {
                            damage.damage *= 2;
                        }
                        else
                        {
                            victimHealth.Suicide(attackerBody.gameObject);
                        }
                    }
                }
            }
            orig(self, damage, victim);
        }

        private static void OnInventoryChangedHook(On.RoR2.CharacterBody.orig_OnInventoryChanged orig, CharacterBody self)
        {
            var count = self.inventory.GetItemCountPermanent(instantKillItemDef);

            if (count > 0)
            {
                foreach (var body in CharacterBody.readOnlyInstancesList)
                {
                    if (body.isPlayerControlled) body.AddTimedBuff(instantKillBuffDef, 30);
                }
                self.inventory.RemoveItemPermanent(instantKillItemDef);
            }
            orig(self);
        }
    }
}

