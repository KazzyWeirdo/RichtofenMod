using R2API;
using RichtofenSurvivor.EntityStates;
using RoR2;
using RoR2.ContentManagement;
using RoR2.ExpansionManagement;
using RoR2.Skills;
using System.Collections;
using UnityEditor;
using UnityEngine;
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

            GameObject crosshair = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/UI/CrosshairSimple.prefab").WaitForCompletion();
            //manera moderna de anadir entitystates, al parecer no funciona bien con nuestra manera de usar contentpack
            //ContentAddition.AddEntityState<TestPistolState>(out _);
            //ContentAddition.AddEntityState<TestSniperState>(out _);
            //ContentAddition.AddEntityState<ShootState>(out _);

            _myBundle = asyncOperation.assetBundle;
            _mySurvivor = _myBundle.LoadAsset<SurvivorDef>("RichtofenDef");
            ItemDef _myItem = _myBundle.LoadAsset<ItemDef>("PistolItemDef");
            var expansionDef = _myBundle.LoadAsset<ExpansionDef>("RichtofenExpansion");
            //Write code here to initialize your mod post assetbundle load
            GameObject _myPrefab = _myBundle.LoadAsset<GameObject>("RichtofenBody");
            _myPrefab.AddComponent<WeaponInventory>();
            


            //la ultima vez no habia errores, si falta esto ahora siempre salta un error de que no state index; hay que anadir los entity states de esta forma
            //y antes de inicializar las habilidades
            RichtofenSurvivorContentPack.entityStateTypes.Add(new System.Type[]
            {
                typeof(TestPistolState), typeof(TestSniperState), typeof(ShootState), typeof(SwapState), typeof(TestShotgunState), typeof(TestRifleState), typeof(TestRaygunState)
            });


            
            RichtofenPrimarySkillDef.RichtofenPrimarySkills();
            RichtofenUtilitySkillDef.RichtofenUtilitySkills();


            var pod = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/SurvivorPod/PodPrefab.prefab").WaitForCompletion();
            

            RichtofenSurvivorContentPack.bodyPrefabs.Add(new GameObject[] { _myPrefab });
            RichtofenSurvivorContentPack.survivorDefs.Add(new SurvivorDef[] { _mySurvivor });
            RichtofenSurvivorContentPack.expansionDefs.Add(new ExpansionDef[] { expansionDef });
            ItemDef[] allItems = _myBundle.LoadAllAssets<ItemDef>();

            RichtofenSurvivorContentPack.itemDefs.Add(allItems);
            //RichtofenSurvivorContentPack.itemDefs.Add(new ItemDef[] { _myItem });
            //esto creo que es antiguo, la nueva manera esta especificada en primaryskilldef

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
                SurvivorDefModify.ModifySurvivorDef();
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
