using RoR2.ContentManagement;
using UnityEngine;
using RoR2;
using System.Collections;
namespace RichtofenSurvivor
{
    public class RichtofenSurvivorContent : IContentPackProvider
    {
        public string identifier => RichtofenSurvivorMain.GUID;

        public static ReadOnlyContentPack readOnlyContentPack => new ReadOnlyContentPack(RichtofenSurvivorContentPack);
        public static ContentPack RichtofenSurvivorContentPack { get; } = new ContentPack();

        private static ItemTierDef powerUpTier;
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
