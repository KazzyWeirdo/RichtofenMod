using RoR2;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBehaviour
{
    private static ItemDef powerUpItemDef;
    public static void RegisterMainHooks (ItemDef powerUpItem)
    {
        powerUpItemDef = powerUpItem;
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
                PickupCatalog.FindPickupIndex(powerUpItemDef.itemIndex),
                transform.position,
                transform.forward * 20f);
        }
    }

}
