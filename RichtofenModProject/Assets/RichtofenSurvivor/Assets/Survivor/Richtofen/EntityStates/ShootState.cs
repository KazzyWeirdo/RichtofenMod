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
       
        public override void OnEnter()
        {
            base.OnEnter();
            //duration = weaponDurations[CommandoTestSkillDefs.activeWeapon.Name] / attackSpeedStat;

            Chat.SendBroadcastChat(new SimpleChatMessage { baseToken = "<color=#e5eefc>{0}</color>", paramTokens = new[] { "shooting this: " + WeaponInventory.activeWeapon.WeaponName } });
            EntityState nextState = (EntityState)System.Activator.CreateInstance(WeaponInventory.activeWeapon.WeaponState);
            RichtofenSurvivorMain.LogInfo("fefe" + nextState.GetType());

            RichtofenSurvivorMain.LogInfo("ammo before " + WeaponInventory.activeWeapon.CurrentAmmo + ", " + WeaponInventory.activeWeapon.MagazineAmmo);


            if (WeaponInventory.activeWeapon.MagazineAmmo != 0)
            { 
                WeaponInventory.activeWeapon.MagazineAmmo--;
                if(WeaponInventory.activeWeapon.MagazineAmmo == 0)
                {
                    if(WeaponInventory.activeWeapon.CurrentAmmo != 0)
                    {
                        //insertar mierda para animacion para recargar etc
                        WeaponInventory.activeWeapon.ReloadWeapon();
                        RichtofenSurvivorMain.LogInfo("reloading");
                    }
                    else
                    {
                        RichtofenSurvivorMain.LogWarning("no ammo");
                    }
                }
            }
            


                RichtofenSurvivorMain.LogInfo("ammo after " + WeaponInventory.activeWeapon.CurrentAmmo + ", " + WeaponInventory.activeWeapon.MagazineAmmo);

            this.outer.SetNextState(nextState);
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

        }

    }
}
