using RoR2;
using RichtofenSurvivor;

public class PowerUpAssetLoadUp
{
    private static ItemTierDef powerUpTier;
    private static ItemDef doublePointsDef;
    private static BuffDef doublePointsBuffDef;
    private static ItemDef instantKillDef;
    private static BuffDef instantKillBuffDef;
    private static ItemDef fireSaleDef;
    private static BuffDef fireSaleBuffDef;
    private static ItemDef nukeItemDef;
    private static ItemDef maxAmmoItemDef;

    public static void loadAssetstoBundle()
    {
        powerUpTier = RichtofenSurvivorContent._myBundle.LoadAsset<ItemTierDef>("PowerUpTier");
        doublePointsBuffDef = RichtofenSurvivorContent._myBundle.LoadAsset<BuffDef>("DoublePointBuff");
        doublePointsDef = RichtofenSurvivorContent._myBundle.LoadAsset<ItemDef>("DoublePointsItem");
        instantKillBuffDef = RichtofenSurvivorContent._myBundle.LoadAsset<BuffDef>("InstantKillBuff");
        instantKillDef = RichtofenSurvivorContent._myBundle.LoadAsset<ItemDef>("InstantKillItem");
        fireSaleDef = RichtofenSurvivorContent._myBundle.LoadAsset<ItemDef>("FireSaleItem");
        fireSaleBuffDef = RichtofenSurvivorContent._myBundle.LoadAsset<BuffDef>("FireSaleBuff");
        nukeItemDef = RichtofenSurvivorContent._myBundle.LoadAsset<ItemDef>("NukeItem");
        maxAmmoItemDef = RichtofenSurvivorContent._myBundle.LoadAsset<ItemDef>("MaxAmmoItem");

        RichtofenSurvivorContent.RichtofenSurvivorContentPack.itemDefs.Add(new ItemDef[] { 
            nukeItemDef,
            fireSaleDef,
            doublePointsDef,
            instantKillDef,
            maxAmmoItemDef
        });

        RichtofenSurvivorContent.RichtofenSurvivorContentPack.buffDefs.Add(new BuffDef[] { 
            fireSaleBuffDef,
            doublePointsBuffDef,
            instantKillBuffDef
        });

        RichtofenSurvivorContent.RichtofenSurvivorContentPack.itemTierDefs.Add(new ItemTierDef[] { powerUpTier });
    }

    public static void loadPowerUpBehaviour()
    {
        PowerUpBehaviour.RegisterMainHooks();
    }
}
