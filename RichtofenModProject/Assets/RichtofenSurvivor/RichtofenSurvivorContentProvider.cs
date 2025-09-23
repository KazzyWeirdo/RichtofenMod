using RoR2.ContentManagement;
using UnityEngine;
using RoR2;
using System.Collections;
using RoR2.ExpansionManagement;
namespace RichtofenSurvivor
{
    public class RichtofenSurvivorContent : IContentPackProvider
    {
        public string identifier => RichtofenSurvivorMain.GUID;

        public static ReadOnlyContentPack readOnlyContentPack => new ReadOnlyContentPack(RichtofenSurvivorContentPack);
        internal static ContentPack RichtofenSurvivorContentPack { get; } = new ContentPack();

        private static SurvivorDef _mySurvivor;
        private static AssetBundle _myBundle;

        public IEnumerator LoadStaticContentAsync(LoadStaticContentAsyncArgs args)
        {
            var asyncOperation = AssetBundle.LoadFromFileAsync(RichtofenSurvivorMain.assetBundleDir);
            while (!asyncOperation.isDone)
            {
                args.ReportProgress(asyncOperation.progress);
                yield return null;
            }
            _myBundle = asyncOperation.assetBundle;
            _mySurvivor = _myBundle.LoadAsset<SurvivorDef>("RichtofenDef");
            var expansionDef = _myBundle.LoadAsset<ExpansionDef>("RichtofenExpansion");
            //Write code here to initialize your mod post assetbundle load

            RichtofenSurvivorMain.LogInfo("testing rich: " + _mySurvivor.ToString());

            RichtofenSurvivorContentPack.bodyPrefabs.Add(new GameObject[] { _mySurvivor.bodyPrefab });
            
            RichtofenSurvivorContentPack.survivorDefs.Add(new SurvivorDef[] { _mySurvivor });
            RichtofenSurvivorContentPack.expansionDefs.Add(new ExpansionDef[] { expansionDef });

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
