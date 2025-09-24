using RoR2;
using RichtofenSurvivor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R2API;
using RoR2.Skills;

public class SurvivorDefModify
{
    //thunderkit no deja modificar el survivor def directamente
    private static SurvivorDef richtofenDef;
    
    public static GameObject ModifySurvivorDef()
    {
        //var asyncOperation = AssetBundle.LoadFromFileAsync(RichtofenSurvivorMain.assetBundleDir);

        //while (!asyncOperation.isDone)
        //{ 
        //    yield return null;
        //}

        SkillDef sdRichtofenPrimary = RichtofenSurvivorContent._myBundle.LoadAsset<SkillDef>("sdRichtofenPrimary");
        sdRichtofenPrimary = RichtofenPrimarySkillDef.RichtofenPrimarySkills();

        richtofenDef = RichtofenSurvivorContent._myBundle.LoadAsset<SurvivorDef>("RichtofenDef");
        GameObject richtofenPrefab = richtofenDef.bodyPrefab;
        //GenericSkill richtofenPrimary = richtofenPrefab.GetComponent<SkillLocator>();

        //richtofenPrefab.GetComponent<SkillLocator>().primary = RichtofenPrimarySkillDef.RichtofenPrimarySkills();

        return richtofenPrefab;

    }
    
}
