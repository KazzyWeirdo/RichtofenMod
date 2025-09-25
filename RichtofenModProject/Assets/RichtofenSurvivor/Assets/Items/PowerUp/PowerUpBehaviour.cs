using RichtofenSurvivor;
using RoR2;
using System;
using UnityEngine;

public class PowerUpBehaviour
{
    public static float probability = 100;
    public static ItemDef[] powerUpItemDefinitions =
    {
        RichtofenSurvivorContent.readOnlyContentPack.itemDefs.Find("DoublePointsItem"),
        RichtofenSurvivorContent.readOnlyContentPack.itemDefs.Find("InstantKillItem")
    };
    
    public static void RegisterMainHooks()
    {
        DoublePoints.DoublePointsBehavior.RegisterHooks();
        InstantKill.InstantKillBehaviour.RegisterHooks();
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

        if (Util.CheckRoll(probability, report.victimBody.master))
        {
            PickupDropletController.CreatePickupDroplet(
                PickupCatalog.FindPickupIndex(powerUpDropRandomizer().itemIndex),
                transform.position,
                transform.forward * 20f);
        }
    }

    private static ItemDef powerUpDropRandomizer()
    {
        int r = UnityEngine.Random.Range(0, powerUpItemDefinitions.Length);
        Debug.Log($"Random value: {r}");
        return powerUpItemDefinitions[(r)];
    }

}
