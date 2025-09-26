using RichtofenSurvivor;
using RoR2;

public class FireSaleBehaviour
{
    private static ItemDef fireSaleItemDef;
    private static BuffDef fireSaleBuffDef;

    public static void RegisterHooks()
    {
        fireSaleItemDef = RichtofenSurvivorContent.readOnlyContentPack.itemDefs.Find("FireSaleItem");
        fireSaleBuffDef = RichtofenSurvivorContent.readOnlyContentPack.buffDefs.Find("FireSaleBuff");

        On.RoR2.CharacterBody.OnInventoryChanged += OnInventoryChangedHook;
    }

    private static void OnInventoryChangedHook(On.RoR2.CharacterBody.orig_OnInventoryChanged orig, CharacterBody self)
    {
        var count = self.inventory.GetItemCount(fireSaleItemDef);

        if (count > 0)
        {
            self.AddTimedBuff(fireSaleBuffDef, 20);
            self.inventory.RemoveItem(fireSaleItemDef);
        }
        orig(self);
    }
}
