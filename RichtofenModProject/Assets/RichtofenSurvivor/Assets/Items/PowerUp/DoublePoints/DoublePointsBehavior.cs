using RoR2;
using BepInEx;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using R2API;
using RichtofenSurvivor;
using RoR2.ContentManagement;

namespace DoublePoints
{
    [BepInDependency(ItemAPI.PluginGUID)]

    public class DoublePointsBehavior : BaseUnityPlugin
    {
        public ItemDef doublePointsDef;
        private static AssetBundle _myBundle;

        private void Awake()
        {
            var asyncOperation = AssetBundle.LoadFromFileAsync(RichtofenSurvivorMain.assetBundleDir);
            
            _myBundle = asyncOperation.assetBundle;
            doublePointsDef = _myBundle.LoadAsset<ItemDef>("DoublePointsItem");

           RichtofenSurvivorContent.RichtofenSurvivorContentPack.itemDefs.Add(new ItemDef[] { doublePointsDef });

            ContentManager.collectContentPackProviders += addContentPackProvider =>
            {
                addContentPackProvider(new RichtofenSurvivorContent());
            };

            GlobalEventManager.onCharacterDeathGlobal += GlobalEventManager_onCharacterDeathGlobal;
        }

        private void GlobalEventManager_onCharacterDeathGlobal(DamageReport report)
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

        private void Update()
        {

        }
    }

}
