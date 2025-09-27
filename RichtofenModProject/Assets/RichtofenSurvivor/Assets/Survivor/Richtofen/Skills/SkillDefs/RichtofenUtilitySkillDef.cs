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

public static class RichtofenUtilitySkillDef
{
    
    public static SkillDef RichtofenUtilitySkills()
    {
        SkillFamily utility = RichtofenSurvivorContent._myBundle.LoadAsset<SkillFamily>("sfRichtofenUtility");
        LanguageAPI.Add("RICH_RICHTOFEN_UTILITY_NAME", "Weapon Swap");
        LanguageAPI.Add("RICH_RICHTOFEN_UTILITY_DESCRIPTION", $"Swaps currently equipped weapon with secondary weapon (if possible). ");



        SkillDef mySkillDef = ScriptableObject.CreateInstance<SkillDef>();

        //Check step 2 for the code of the CustomSkillsTutorial.MyEntityStates.SimpleBulletAttack class


        mySkillDef.activationState = new SerializableEntityStateType(typeof(SwapState));


        mySkillDef.activationStateMachineName = "Weapon";
        mySkillDef.baseMaxStock = 1;
        mySkillDef.baseRechargeInterval = 0.5f;
        mySkillDef.beginSkillCooldownOnSkillEnd = false;
        mySkillDef.canceledFromSprinting = false;
        mySkillDef.cancelSprintingOnActivation = false;
        mySkillDef.fullRestockOnAssign = true;
        mySkillDef.interruptPriority = InterruptPriority.Skill;
        mySkillDef.isCombatSkill = false;
        mySkillDef.mustKeyPress = false;
        mySkillDef.rechargeStock = 1;
        mySkillDef.requiredStock = 1;
        mySkillDef.stockToConsume = 1;
        // For the skill icon, you will have to load a Sprite from your own AssetBundle
        mySkillDef.icon = RichtofenSurvivorContent._myBundle.LoadAsset<Sprite>("sdRichtofenUtility");
        mySkillDef.skillDescriptionToken = "RICH_RICHTOFEN_UTILITY_DESCRIPTION";
        mySkillDef.skillName = "RICH_RICHTOFEN_UTILITY_NAME";
        mySkillDef.skillNameToken = "RICH_RICHTOFEN_UTILITY_NAME";
        utility.variants = new SkillFamily.Variant[1] { new() { skillDef = mySkillDef } };



        return mySkillDef;
    }
}
