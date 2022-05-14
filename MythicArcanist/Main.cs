using HarmonyLib;
using TabletopTweaks.Core.Utilities;
using MythicArcanist.ModLogic;
using UnityModManagerNet;

namespace MythicArcanist
{
    static class Main
    {
        //public static bool Enabled;
        public static ModContext ThisModContext;
        static bool Load(UnityModManager.ModEntry modEntry)
        {
            var harmony = new Harmony(modEntry.Info.Id);
            ThisModContext = new ModContext(modEntry);
            //ModContext.LoadAllSettings();
            ThisModContext.ModEntry.OnSaveGUI = OnSaveGUI;
            ThisModContext.ModEntry.OnGUI = UMMSettingsUI.OnGUI;
            harmony.PatchAll();
            PostPatchInitializer.Initialize(ThisModContext);
            return true;
        }

        static void OnSaveGUI(UnityModManager.ModEntry modEntry)
        {
            ThisModContext.SaveAllSettings();
        }
    }
}
