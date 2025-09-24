using RoR2.ContentManagement;
using UnityEngine;
using RoR2;
using System.Collections;
using DoublePoints;

namespace RichtofenSurvivor
{
    public class RichtofenSurvivorContent : IContentPackProvider
    {
        public string identifier => RichtofenSurvivorMain.GUID;

        public static ReadOnlyContentPack readOnlyContentPack => new ReadOnlyContentPack(RichtofenSurvivorContentPack);
        public static ContentPack RichtofenSurvivorContentPack { get; } = new ContentPack();

        private static ItemTierDef powerUpTier;
        private static ItemDef doublePointsDef;
        private static BuffDef doublePointsBuffDef;
        private static ItemDef instantKillDef;
        private static BuffDef instantKillBuffDef;
        private static AssetBundle _myBundle;

        public IEnumerator LoadStaticContentAsync(LoadStaticContentAsyncArgs args)
        {
            var asyncOperation = AssetBundle.LoadFromFileAsync(RichtofenSurvivorMain.assetBundleDir);
            while(!asyncOperation.isDone)
            {
                args.ReportProgress(asyncOperation.progress);
                yield return null;
            }

            //Write code here to initialize your mod post assetbundle load
            _myBundle = asyncOperation.assetBundle;
            powerUpTier = _myBundle.LoadAsset<ItemTierDef>("PowerUpTier");
            doublePointsBuffDef = _myBundle.LoadAsset<BuffDef>("DoublePointBuff");
            doublePointsDef = _myBundle.LoadAsset<ItemDef>("DoublePointsItem");
            instantKillBuffDef = _myBundle.LoadAsset<BuffDef>("InstantKillBuff");
            instantKillDef = _myBundle.LoadAsset<ItemDef>("InstantKillItem");

            RichtofenSurvivorContentPack.itemDefs.Add(new ItemDef[] { doublePointsDef });
            RichtofenSurvivorContentPack.buffDefs.Add(new BuffDef[] { doublePointsBuffDef });
            RichtofenSurvivorContentPack.itemDefs.Add(new ItemDef[] { instantKillDef });
            RichtofenSurvivorContentPack.buffDefs.Add(new BuffDef[] { instantKillBuffDef });
            RichtofenSurvivorContentPack.itemTierDefs.Add(new ItemTierDef[] { powerUpTier });
        }
        public IEnumerator GenerateContentPackAsync(GetContentPackAsyncArgs args)
        {
             ContentPack.Copy(RichtofenSurvivorContentPack, args.output);
            args.ReportProgress(1f);
            yield break;
        }
        public IEnumerator FinalizeAsync(FinalizeAsyncArgs args)
        {
            // Wait until content packs are loaded before registering hooks
            RoR2Application.onLoad += () =>
            {
                DoublePointsBehavior.RegisterHooks();
                InstantKillBehaviour.RegisterHooks();
            };
            args.ReportProgress(1f);
            yield break;
        }
        private void AddSelf(ContentManager.AddContentPackProviderDelegate addContentPackProvider)
        {
            addContentPackProvider(this);
        }
        internal RichtofenSurvivorContent()
        {
            ContentManager.collectContentPackProviders += AddSelf;
        }
    }
}
