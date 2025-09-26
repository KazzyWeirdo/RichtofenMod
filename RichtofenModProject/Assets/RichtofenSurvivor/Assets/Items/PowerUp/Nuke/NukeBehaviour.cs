using RichtofenSurvivor;
using RoR2;

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
            var count = self.inventory.GetItemCount(nukeItemDef);

            if (count > 0)
            {
                self.inventory.RemoveItem(nukeItemDef);

                foreach (var body in CharacterBody.readOnlyInstancesList)
                {
                    if (!body.isPlayerControlled && !body.isBoss && !body.isElite)
                    {
                        body.healthComponent.Suicide();
                    }

                    if(body.isPlayerControlled)
                    {
                        body.master.GiveMoney((uint)Run.instance.GetDifficultyScaledCost(25));
                    }
                }
            }
            orig(self);
        }
    }
}
