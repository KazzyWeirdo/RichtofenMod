using RoR2;
using RichtofenSurvivor;
using UnityEngine;
using UnityEngine.Networking;

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

            if (!NetworkServer.active) return;

            var count = self.inventory.GetItemCountPermanent(maxAmmoItemDef);

            if (count > 0)
            {
                self.inventory.RemoveItemPermanent(maxAmmoItemDef);

                foreach (var body in CharacterBody.readOnlyInstancesList)
                {
                    if (body.isPlayerControlled)
                    {
                        body.AddTimedBuff(RoR2Content.Buffs.NoCooldowns, 0.1f);
                        ClearCooldownBuffs(body);
                        RestartUsedItems(body.master);
                        RestartEquipmentItems(body);
                    }
                }
            }
        }

        private static void ClearCooldownBuffs(CharacterBody body)
        {
            SkillLocator sl = body.skillLocator;
            if (sl == null) return;

            GenericSkill[] skills = { sl.primary, sl.secondary, sl.utility, sl.special };
            foreach (var skill in skills)
            {
                skill?.Reset();
            }
        }

        private static void RestartEquipmentItems(CharacterBody self)
        {
            uint slot = self.inventory.activeEquipmentSlot;
            foreach(byte equipset in self.inventory.activeEquipmentSet)
            {
                EquipmentState eq = self.inventory.GetEquipment(slot, equipset);
                self.inventory.SetEquipment(
                    new EquipmentState(eq.equipmentIndex, Run.FixedTimeStamp.now, (byte)Mathf.Max(eq.charges, (byte)1)),
                    slot,
                    equipset);
            }
        }

        private static void RestartUsedItems(CharacterMaster master)
        {
            if (!master || !master.inventory) return;
            var inv = master.inventory;

            // You might have to add the Items ONE AT THE TIME gl
            RestoreConsumed(inv, DLC1Content.Items.HealingPotionConsumed, DLC1Content.Items.HealingPotion);
            RestoreConsumed(inv, RoR2Content.Items.ExtraLifeConsumed, RoR2Content.Items.ExtraLife);
            RestoreConsumed(inv, DLC1Content.Items.ExtraLifeVoidConsumed, DLC1Content.Items.ExtraLifeVoid);

            foreach (ItemDef item in ItemCatalog.allItemDefs)
            {
                ItemIndex itemIndex = item.itemIndex;
                int count = inv.GetItemCountPermanent(itemIndex);
                if (count <= 0) return;

                var def = ItemCatalog.GetItemDef(itemIndex);
                if (def == null || !def.isConsumed) return;

                inv.RemoveItemPermanent(itemIndex, count);
                inv.GiveItemPermanent(itemIndex, count);
            }
        }

        private static void RestoreConsumed(Inventory inv, ItemDef consumed, ItemDef original)
        {
            int n = inv.GetItemCountPermanent(consumed);
            if (n <= 0) return;
            inv.RemoveItemPermanent(consumed, n);
            inv.GiveItemPermanent(original, n);
        }
    }
}