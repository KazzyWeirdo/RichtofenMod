using EntityStates;
using RoR2;
using static RoR2.Chat;
//using ExamplePlugin.SkillDefs;
using System.Collections.Generic;
using RichtofenSurvivor;

namespace RichtofenSurvivor.EntityStates
{
    internal class ShootState : BaseState
    {
        
        private static readonly Dictionary<string, float> weaponDurations = new Dictionary<string, float>
        {
            { "TestPistolState", 1.5f },
            { "TestSniperState", 3.0f }
        };

        
        

        public override void OnEnter()
        {
            base.OnEnter();
            //duration = weaponDurations[CommandoTestSkillDefs.activeWeapon.Name] / attackSpeedStat;

            Chat.SendBroadcastChat(new SimpleChatMessage { baseToken = "<color=#e5eefc>{0}</color>", paramTokens = new[] { "shooting this: " + RichtofenPrimarySkillDef.activeWeapon.Name } });
            EntityState nextState = (EntityState)System.Activator.CreateInstance(RichtofenPrimarySkillDef.activeWeapon);
            RichtofenSurvivorMain.LogInfo("fefe" + nextState.GetType());

            


            RichtofenSurvivorMain.LogInfo("active weapon ammo stuff: " + RichtofenPrimarySkillDef.activeWeapon);
            
            this.outer.SetNextState(nextState);
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

        }

    }
}
