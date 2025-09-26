using RichtofenSurvivor;
using RoR2;
using System;
using System.Reflection;
using UnityEngine;

namespace PowerUp
{
    public class FireSaleBehaviour
    {
        private static ItemDef fireSaleItemDef;
        private static BuffDef fireSaleBuffDef;
        static FieldInfo costField = typeof(PurchaseInteraction).GetField("cost", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        static FieldInfo viewerBodyField = typeof(PurchaseInteraction).GetField("viewerBody", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        public static void RegisterHooks()
        {
            fireSaleItemDef = RichtofenSurvivorContent.readOnlyContentPack.itemDefs.Find("FireSaleItem");
            fireSaleBuffDef = RichtofenSurvivorContent.readOnlyContentPack.buffDefs.Find("FireSaleBuff");

            On.RoR2.CharacterBody.OnInventoryChanged += OnInventoryChangedHook;
            On.RoR2.PurchaseInteraction.GetContextString += GetContextString;
            On.RoR2.PurchaseInteraction.CanBeAffordedByInteractor += CanBeAffordedByInteractor;
            On.RoR2.PurchaseInteraction.OnInteractionBegin += OnInteractionBegin;
            On.RoR2.PurchaseInteraction.UpdateHologramContent += UpdateHologramContent;
        }

        private static void OnInventoryChangedHook(On.RoR2.CharacterBody.orig_OnInventoryChanged orig, CharacterBody self)
        {
            var count = self.inventory.GetItemCount(fireSaleItemDef);

            if (count > 0)
            {
                foreach (var body in CharacterBody.readOnlyInstancesList)
                {
                    if (body.isPlayerControlled) body.AddTimedBuff(fireSaleBuffDef, 20);
                }
                self.inventory.RemoveItem(fireSaleItemDef);
            }
            orig(self);
        }

        private static bool CanBeAffordedByInteractor(On.RoR2.PurchaseInteraction.orig_CanBeAffordedByInteractor orig, PurchaseInteraction self, Interactor interactor)
        {
            var body = interactor.GetComponent<CharacterBody>();
            if (body != null && body.HasBuff(fireSaleBuffDef))
            {
                int original = (int)costField.GetValue(self);
                int discounted = Mathf.CeilToInt(original * 0.25f);


                costField.SetValue(self, discounted);
                bool result = orig(self, interactor);

                costField.SetValue(self, original);
                return result;
            }

            return orig(self, interactor);
        }

        private static string GetContextString(On.RoR2.PurchaseInteraction.orig_GetContextString orig, PurchaseInteraction self, Interactor activator)
        {
            int original = (int)costField.GetValue(self);
            CharacterBody body = activator.GetComponent<CharacterBody>();

            if (body && body.HasBuff(fireSaleBuffDef))
            {
                try
                {
                    costField.SetValue(self, Mathf.CeilToInt(original * 0.25f));
                    return orig(self, activator);
                }
                finally
                {
                    costField.SetValue(self, original);
                }
            }

            return orig(self, activator);
        }

        private static void UpdateHologramContent(On.RoR2.PurchaseInteraction.orig_UpdateHologramContent orig, PurchaseInteraction self, GameObject hologramContentObject, Transform viewer)
        {
            int original = (int)costField.GetValue(self);
            CharacterBody viewerBody = (CharacterBody)viewerBodyField.GetValue(self);
            try
            {
                if (viewerBody != null && viewerBody.HasBuff(fireSaleBuffDef))
                {
                    costField.SetValue(self, Mathf.CeilToInt(original * 0.25f));
                }
                orig(self, hologramContentObject, viewer);
            }
            finally
            {
                costField.SetValue(self, original);
            }
        }

        private static void OnInteractionBegin(On.RoR2.PurchaseInteraction.orig_OnInteractionBegin orig, PurchaseInteraction self, Interactor interactor)
        {
            var body = interactor.GetComponent<CharacterBody>();
            if (body != null && body.HasBuff(fireSaleBuffDef))
            {
                int original = (int)costField.GetValue(self);
                int discounted = Mathf.CeilToInt(original * 0.25f);


                costField.SetValue(self, discounted);

                orig(self, interactor);


                costField.SetValue(self, original);
                return;
            }

            orig(self, interactor);
        }
    }
}