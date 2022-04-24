using TabletopTweaks.Core.ModLogic;
using MythicArcanist.Config;
using static UnityModManagerNet.UnityModManager;

namespace MythicArcanist.ModLogic
{
    internal class ModContextMA : ModContextBase
    {
        public Homebrew Homebrew;

        public ModContextMA(ModEntry ModEntry) : base(ModEntry)
        {
#if DEBUG
            Debug = true;
#endif
            LoadAllSettings();
        }
        public override void LoadAllSettings()
        {
            LoadSettings("Homebrew.json", "MythicArcanist.Config", ref Homebrew);
            LoadBlueprints("MythicArcanist.Config", this);
            LoadLocalization("MythicArcanist.Localization");
        }
        public override void AfterBlueprintCachePatches()
        {
            base.AfterBlueprintCachePatches();
            if(Debug)
            {
                //Blueprints.RemoveUnused();
                //SaveSettings(BlueprintsFile, Blueprints);
                //ModLocalizationPack.RemoveUnused();
                //SaveLocalization(ModLocalizationPack);
            }
        }
        public override void SaveAllSettings()
        {
            base.SaveAllSettings();
            SaveSettings("Homebrew.json", Homebrew);
        }
    }
}
