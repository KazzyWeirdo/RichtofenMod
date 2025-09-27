using EntityStates;
using RoR2;
using static RoR2.Chat;
//using ExamplePlugin.SkillDefs;

namespace RichtofenSurvivor.EntityStates
{
    internal class SwapState : BaseState
    {

        public override void OnEnter()
        {
            //SkillLocator skillLocator = base.GetComponent<SkillLocator>();
            Chat.SendBroadcastChat(new SimpleChatMessage { baseToken = "<color=#e5eefc>{0}</color>", paramTokens = new[] { "swapping: " + WeaponInventory.activeWeapon + " to: " + WeaponInventory.secondaryWeapon } });

            base.OnEnter();

            WeaponInventory.SwapWeapons();
            

        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (base.fixedAge >= 0.5f && isAuthority)
            {
                this.outer.SetNextStateToMain();
                return;
            }
        }

    }
}
