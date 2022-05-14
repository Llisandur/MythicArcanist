using TabletopTweaks.Core.ModLogic;
using MythicArcanist.Config;
using static UnityModManagerNet.UnityModManager;

namespace MythicArcanist.ModLogic
{
    internal class ModContext : ModContextBase
    {
        public AddedContent AddedContent;
        public Homebrew Homebrew;
        public ThirdParty ThirdParty;

        public ModContext(ModEntry ModEntry) : base(ModEntry)
        {
#if DEBUG
            Debug = true;
#endif
            LoadAllSettings();
        }
        public override void LoadAllSettings()
        {
            LoadSettings("AddedContent.json", "MythicArcanist.Config", ref AddedContent);
            LoadSettings("Homebrew.json", "MythicArcanist.Config", ref Homebrew);
            LoadSettings("ThirdParty.json", "MythicArcanist.Config", ref ThirdParty);
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
            SaveSettings("AddedContent.json", AddedContent);
            SaveSettings("Homebrew.json", Homebrew);
            SaveSettings("ThirdParty.json", ThirdParty);
        }
    }
}
