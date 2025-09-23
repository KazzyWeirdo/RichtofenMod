using RoR2;
using BepInEx;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using R2API;
using RichtofenSurvivor;

namespace DoublePoints
{

    public static class DoublePointsBehavior
    {
        private static ItemDef doublePointsDef;

        public static void RegisterHooks()
        {
            doublePointsDef = RichtofenSurvivorContent.readOnlyContentPack.itemDefs.Find("DoublePointsItem");

            GlobalEventManager.onCharacterDeathGlobal += GlobalEventManager_onCharacterDeathGlobal;
        }

        private static void GlobalEventManager_onCharacterDeathGlobal(DamageReport report)
        {
            // If a character was killed by the world, we shouldn't do anything.
            if (!report.attacker || !report.attackerBody)
            {
                return;
            }

            var transform = report.victimBody.master.GetBodyObject().transform;

            if (Util.CheckRoll(100, report.victimBody.master))
            {
                PickupDropletController.CreatePickupDroplet(
                    PickupCatalog.FindPickupIndex(doublePointsDef.itemIndex),
                    transform.position,
                    transform.forward * 20f);
            }
        }
    }

}
