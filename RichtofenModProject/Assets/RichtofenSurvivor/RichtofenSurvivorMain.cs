using BepInEx;
using System.IO;
using UnityEngine;
namespace RichtofenSurvivor
{
    #region Dependencies
    [BepInDependency("com.bepis.r2api.teams")]
    [BepInDependency("com.bepis.r2api.rules")]
    [BepInDependency("com.bepis.r2api.extendedStringSerializer")]
    [BepInDependency("com.bepis.r2api.stages")]
    [BepInDependency("com.bepis.r2api.addressables")]
    [BepInDependency("com.bepis.r2api.animations")]
    [BepInDependency("com.bepis.r2api.character_body")]
    [BepInDependency("com.bepis.r2api")]
    [BepInDependency("com.bepis.r2api.skills")]
    [BepInDependency("com.bepis.r2api.proctype")]
    [BepInDependency("com.bepis.r2api.colors")]
    #endregion
    [BepInPlugin(GUID, MODNAME, VERSION)]
    public class RichtofenSurvivorMain : BaseUnityPlugin
    {
        public const string GUID = "com.Kazzy_Weirdo & Natani.RichtofenSurvivor";
        public const string MODNAME = "Richtofen Survivor";
        public const string VERSION = "0.0.1";

        public static PluginInfo pluginInfo { get; private set; }
        public static RichtofenSurvivorMain instance { get; private set; }
        internal static AssetBundle assetBundle { get; private set; }
        internal static string assetBundleDir => Path.Combine(Path.GetDirectoryName(pluginInfo.Location), "RichtofenSurvivorAssets");

        private void Awake()
        {
            instance = this;
            pluginInfo = Info;
            new RichtofenSurvivorContent();
        }
        internal static void LogFatal(object data)
        {
            instance.Logger.LogFatal(data);
        }
        internal static void LogError(object data)
        {
            instance.Logger.LogError(data);
        }
        internal static void LogWarning(object data)
        {
            instance.Logger.LogWarning(data);
        }
        internal static void LogMessage(object data)
        {
            instance.Logger.LogMessage(data);
        }
        internal static void LogInfo(object data)
        {
            instance.Logger.LogInfo(data);
        }
        internal static void LogDebug(object data)
        {
            instance.Logger.LogDebug(data);
        }
    }
}
