using R2API;
using RichtofenSurvivor;
using RoR2;
using RoR2.Skills;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class SurvivorDefModify
{
    //thunderkit no deja modificar el survivor def directamente
    private static SurvivorDef richtofenDef;
    
    private static void CharacterBody_Start(On.RoR2.CharacterBody.orig_Start orig, RoR2.CharacterBody self)
    {
        orig(self);
        if (self.bodyIndex != BodyCatalog.FindBodyIndex("RichtofenBody")) return;
        GameObject crosshair = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/UI/StandardCrosshair.prefab").WaitForCompletion();
        RoR2.UI.CrosshairUtils.RequestOverrideForBody(self, crosshair, RoR2.UI.CrosshairUtils.OverridePriority.PrioritySkill);
    }
    public static void ModifySurvivorDef()
    {
        On.RoR2.CharacterBody.Start += CharacterBody_Start;
    }
    
}
