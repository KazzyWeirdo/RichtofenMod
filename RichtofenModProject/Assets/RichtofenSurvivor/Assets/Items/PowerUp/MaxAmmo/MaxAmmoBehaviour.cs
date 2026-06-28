using RoR2;
using RichtofenSurvivor;

namespace PowerUp
{
    public class MaxAmmoBehaviour
    {
        private static ItemDef maxAmmoItemDef;

        public static void RegisterHooks()
        {
            maxAmmoItemDef = RichtofenSurvivorContent.readOnlyContentPack.itemDefs.Find("MaxAmmoItem");
            On.RoR2.CharacterBody.OnInventoryChanged += OnInventoryChangedHook;

        }

        private static void OnInventoryChangedHook(On.RoR2.CharacterBody.orig_OnInventoryChanged orig, CharacterBody self)
        {
            orig(self);

            var count = self.inventory.GetItemCount(maxAmmoItemDef);

            if (count > 0)
            {
                self.inventory.RemoveItem(maxAmmoItemDef);

                foreach (var body in CharacterBody.readOnlyInstancesList)
                {
                    if (body.isPlayerControlled)
                    {
                        body.AddTimedBuff(RoR2Content.Buffs.NoCooldowns, 0.1f);
                        clearCooldownBuffs(body);
                        RestartUsedItems(body.master);
                    }
                }
            }
        }

        private static void clearCooldownBuffs(CharacterBody body)
        {
            foreach(BuffDef coolDownBuffDef in BuffCatalog.buffDefs)
            {
                if (coolDownBuffDef.isCooldown) body.ClearTimedBuffs(coolDownBuffDef);
            }
        }

        private static void RestartUsedItems(CharacterMaster master)
        {
            if (!master || !master.inventory) return;

            var inv = master.inventory;
            foreach (ItemDef item in ItemCatalog.allItemDefs)
            {
                ItemIndex itemIndex = item.itemIndex;
                int count = inv.GetItemCount(itemIndex);
                if (count <= 0) return;

                var def = ItemCatalog.GetItemDef(itemIndex);
                if (def == null || !def.isConsumed) return;

                inv.RemoveItem(itemIndex, count);
                inv.GiveItem(itemIndex, count);
            }
        }
    }
}