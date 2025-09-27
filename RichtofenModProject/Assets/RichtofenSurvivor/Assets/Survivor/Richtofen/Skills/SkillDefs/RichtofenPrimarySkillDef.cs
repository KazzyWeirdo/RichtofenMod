using EntityStates;
using ExamplePlugin.EntityStates;
using R2API;
using RichtofenSurvivor;
using RichtofenSurvivor.EntityStates;
using RoR2.Skills;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RichtofenPrimarySkillDef
{
    
    public static SkillDef RichtofenPrimarySkills()
    {
        SkillFamily primary = RichtofenSurvivorContent._myBundle.LoadAsset<SkillFamily>("sfRichtofenPrimary");
        LanguageAPI.Add("RICH_RICHTOFEN_PRIMARY_NAME", "Shoot Primary Swap Test");
        LanguageAPI.Add("RICH_RICHTOFEN_PRIMARY_DESCRIPTION", $"Fire primary weapon for <style=cIsDamage>damage</style>, each gun has a different output.");



        SkillDef mySkillDef = ScriptableObject.CreateInstance<SkillDef>();

        //Check step 2 for the code of the CustomSkillsTutorial.MyEntityStates.SimpleBulletAttack class


        mySkillDef.activationState = new SerializableEntityStateType(typeof(ShootState));


        mySkillDef.activationStateMachineName = "Weapon";
        mySkillDef.baseMaxStock = 1;
        mySkillDef.baseRechargeInterval = 0f;
        mySkillDef.beginSkillCooldownOnSkillEnd = true;
        mySkillDef.canceledFromSprinting = false;
        mySkillDef.cancelSprintingOnActivation = true;
        mySkillDef.fullRestockOnAssign = true;
        mySkillDef.interruptPriority = InterruptPriority.Any;
        mySkillDef.isCombatSkill = true;
        mySkillDef.mustKeyPress = false;
        mySkillDef.rechargeStock = 1;
        mySkillDef.requiredStock = 1;
        mySkillDef.stockToConsume = 1;
        // For the skill icon, you will have to load a Sprite from your own AssetBundle
        mySkillDef.icon = RichtofenSurvivorContent._myBundle.LoadAsset<Sprite>("sdRichtofenPrimary_2");
        mySkillDef.skillDescriptionToken = "RICH_RICHTOFEN_PRIMARY_DESCRIPTION";
        mySkillDef.skillName = "RICH_RICHTOFEN_PRIMARY_NAME";
        mySkillDef.skillNameToken = "RICH_RICHTOFEN_PRIMARY_NAME";
        primary.variants = new SkillFamily.Variant[1] {new() { skillDef = mySkillDef } };



        return mySkillDef;
    }

    

}
