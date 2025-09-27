using RoR2.ContentManagement;
using UnityEngine;
using RoR2;
using RoR2.Skills;
using System.Collections;
using RoR2.ExpansionManagement;
using R2API;
using UnityEditor;
using UnityEngine.AddressableAssets;
namespace RichtofenSurvivor
{
    public class RichtofenSurvivorContent : IContentPackProvider
    {
        public string identifier => RichtofenSurvivorMain.GUID;

        public static ReadOnlyContentPack readOnlyContentPack => new ReadOnlyContentPack(RichtofenSurvivorContentPack);
        internal static ContentPack RichtofenSurvivorContentPack { get; } = new ContentPack();

        private static SurvivorDef _mySurvivor;
        public static AssetBundle _myBundle;

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
            ItemDef _myItem = _myBundle.LoadAsset<ItemDef>("PistolItemDef");
            var expansionDef = _myBundle.LoadAsset<ExpansionDef>("RichtofenExpansion");
            //Write code here to initialize your mod post assetbundle load
            GameObject _myPrefab = _myBundle.LoadAsset<GameObject>("RichtofenBody");
            _myPrefab.AddComponent<WeaponInventory>();

            RichtofenPrimarySkillDef.RichtofenPrimarySkills();
            RichtofenUtilitySkillDef.RichtofenUtilitySkills();
            var crosshair = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/UI/CrosshairSimple.prefab").WaitForCompletion();
            var pod = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/SurvivorPod/PodPrefab.prefab").WaitForCompletion();
            var letssee = (CharacterBody)_mySurvivor.bodyPrefab.GetComponent("CharacterBody");
            var letssee2 = _mySurvivor.bodyPrefab.GetComponent<CharacterBody>();

            
            


            RichtofenSurvivorMain.LogInfo("testing rich: " + letssee + ", :" + letssee2);

            RoR2.UI.CrosshairUtils.RequestOverrideForBody(letssee, crosshair, RoR2.UI.CrosshairUtils.OverridePriority.Sprint);
            //letssee._defaultCrosshairPrefab = crosshair;
            //_mySurvivor.bodyPrefab.GetComponent<CharacterBody>().preferredPodPrefab = pod;
            //GameObject body = PrefabUtility.LoadPrefabContents("Assets/RichtofenSurvivor/Assets/Survivor/Richtofen/RichtofenBody.prefab");
            //CharacterBody rBody = body.GetComponent<CharacterBody>();
            //CharacterBody duplicate = GameObject.Instantiate(rBody);
            //GameObject.Destroy(rBody);
            //body.AddComponent(duplicate);

            //var _richtofenPrefab = SurvivorDefModify.ModifySurvivorDef();
            //_mySurvivor.bodyPrefab = _richtofenPrefab;

            RichtofenSurvivorContentPack.bodyPrefabs.Add(new GameObject[] { _myPrefab });
            RichtofenSurvivorContentPack.survivorDefs.Add(new SurvivorDef[] { _mySurvivor });
            RichtofenSurvivorContentPack.expansionDefs.Add(new ExpansionDef[] { expansionDef });
            RichtofenSurvivorContentPack.itemDefs.Add(new ItemDef[] { _myItem });

        }
        public IEnumerator GenerateContentPackAsync(GetContentPackAsyncArgs args)
        {
            ContentPack.Copy(RichtofenSurvivorContentPack, args.output);
            args.ReportProgress(1f);
            yield break;
        }
        public IEnumerator FinalizeAsync(FinalizeAsyncArgs args)
        {
            RoR2Application.onLoad += () =>
            {
                WeaponInventory.RegisterInventoryHooks();
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
