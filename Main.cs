using HarmonyLib;
using TabletopTweaks.Core.Utilities;
using MythicArcanist.ModLogic;
using UnityModManagerNet;

namespace MythicArcanist
{
    static class Main
    {
        public static bool Enabled;
        public static ModContextMA MAContext;
        static bool Load(UnityModManager.ModEntry modEntry)
        {
            var harmony = new Harmony(modEntry.Info.Id);
            MAContext = new ModContextMA(modEntry);
            MAContext.LoadAllSettings();
            MAContext.ModEntry.OnSaveGUI = OnSaveGUI;
            MAContext.ModEntry.OnGUI = UMMSettingsUI.OnGUI;
            harmony.PatchAll();
            PostPatchInitializer.Initialize(MAContext);
            return true;
        }

        static void OnSaveGUI(UnityModManager.ModEntry modEntry)
        {
            MAContext.SaveAllSettings();
        }
    }
}
