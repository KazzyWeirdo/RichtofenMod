using RoR2;
using RichtofenSurvivor;
using InstantKill;
using DoublePoints;

public class PowerUpAssetLoadUp
{
    private static ItemTierDef powerUpTier;
    private static ItemDef doublePointsDef;
    private static BuffDef doublePointsBuffDef;
    private static ItemDef instantKillDef;
    private static BuffDef instantKillBuffDef;

    public static void loadAssetstoBundle()
    {
        powerUpTier = RichtofenSurvivorContent._myBundle.LoadAsset<ItemTierDef>("PowerUpTier");
        doublePointsBuffDef = RichtofenSurvivorContent._myBundle.LoadAsset<BuffDef>("DoublePointBuff");
        doublePointsDef = RichtofenSurvivorContent._myBundle.LoadAsset<ItemDef>("DoublePointsItem");
        instantKillBuffDef = RichtofenSurvivorContent._myBundle.LoadAsset<BuffDef>("InstantKillBuff");
        instantKillDef = RichtofenSurvivorContent._myBundle.LoadAsset<ItemDef>("InstantKillItem");

        RichtofenSurvivorContent.RichtofenSurvivorContentPack.itemDefs.Add(new ItemDef[] { doublePointsDef });
        RichtofenSurvivorContent.RichtofenSurvivorContentPack.buffDefs.Add(new BuffDef[] { doublePointsBuffDef });
        RichtofenSurvivorContent.RichtofenSurvivorContentPack.itemDefs.Add(new ItemDef[] { instantKillDef });
        RichtofenSurvivorContent.RichtofenSurvivorContentPack.buffDefs.Add(new BuffDef[] { instantKillBuffDef });
        RichtofenSurvivorContent.RichtofenSurvivorContentPack.itemTierDefs.Add(new ItemTierDef[] { powerUpTier });
    }

    public static void loadPowerUpBehaviour()
    {
        PowerUpBehaviour.RegisterMainHooks();
    }
}
