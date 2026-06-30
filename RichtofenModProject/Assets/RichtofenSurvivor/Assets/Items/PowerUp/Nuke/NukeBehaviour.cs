using RichtofenSurvivor;
using RoR2;
using System.Linq;
using UnityEngine.Networking;

namespace PowerUp
{
    public class NukeBehaviour
    {
        private static ItemDef nukeItemDef;

        public static void RegisterHooks()
        {
            nukeItemDef = RichtofenSurvivorContent.readOnlyContentPack.itemDefs.Find("NukeItem");
            On.RoR2.CharacterBody.OnInventoryChanged += OnInventoryChangedHook;

        }

        private static void OnInventoryChangedHook(On.RoR2.CharacterBody.orig_OnInventoryChanged orig, CharacterBody self)
        {
            orig(self);

            if (!NetworkServer.active || self.inventory == null) return;

            if (self.inventory.GetItemCountPermanent(nukeItemDef) <= 0) return;

            self.inventory.RemoveItemPermanent(nukeItemDef);

            foreach (var body in CharacterBody.readOnlyInstancesList.ToList())
            {
                if (body == null)
                    continue;

                if (!body.isPlayerControlled && !body.isBoss && !body.isElite && !body.IsDrone)
                {
                    if (body.healthComponent != null && body.healthComponent.alive) body.healthComponent.Suicide();
                }
                else if (body.isPlayerControlled && body.master != null)
                {
                    body.master.GiveMoney((uint)Run.instance.GetDifficultyScaledCost(25));
                }
            }
        }
    }
}
