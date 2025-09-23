using RoR2;
using RichtofenSurvivor;

namespace DoublePoints
{

    public class DoublePointsBehavior : PowerUpBehaviour
    {
        private static ItemDef doublePointsDef;
        private static BuffDef doublePointsBuffDef;

        public static void RegisterHooks()
        {
            doublePointsDef = RichtofenSurvivorContent.readOnlyContentPack.itemDefs.Find("DoublePointsItem");
            doublePointsBuffDef = RichtofenSurvivorContent.readOnlyContentPack.buffDefs.Find("DoublePointBuff");

            PowerUpBehaviour.RegisterMainHooks(doublePointsDef);
            On.RoR2.CharacterMaster.GiveMoney += GiveMoneyHook;
            On.RoR2.CharacterBody.OnInventoryChanged += OnInventoryChangedHook;

        }

        private static void GiveMoneyHook(On.RoR2.CharacterMaster.orig_GiveMoney orig, CharacterMaster self, uint amount)
        {
            if (self.hasBody && self.GetBody().HasBuff(doublePointsBuffDef))
            {
                amount *= 2;
            }
            orig(self, amount);
        }

        private static void OnInventoryChangedHook(On.RoR2.CharacterBody.orig_OnInventoryChanged orig, CharacterBody self)
        {
            var count = self.inventory.GetItemCount(doublePointsDef);

            if (count > 0)
            {
                self.AddTimedBuff(doublePointsBuffDef, 30);
                self.inventory.RemoveItem(doublePointsDef);
            }
            orig(self);
        }
    }

}
