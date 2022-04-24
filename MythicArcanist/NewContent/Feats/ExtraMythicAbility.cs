using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Utility;
using TabletopTweaks.Core.Utilities;
using static Kingmaker.Blueprints.Classes.Prerequisites.Prerequisite;
using static MythicArcanist.Main;

namespace MythicArcanist.NewContent.Feats
{
    static class ExtraMythicAbility
    {
        public static void AddExtraMythicAbility()
        {
            var MythicAbilitySelection = BlueprintTools.GetBlueprint<BlueprintFeatureSelection>("ba0e5a900b775be4a99702f1ed08914d");

            var ExtraMythicAbility = FeatTools.CreateExtraSelectionFeat(MAContext, "ExtraMythicAbility", MythicAbilitySelection, bp => {
                bp.SetName(MAContext, "Extra Mythic Ability");
                bp.SetDescription(MAContext, "You gain one additional mythic ability. You must meet the prerequisites for this mythic ability." +
                    "\nYou can take this feat multiple times. Each time you do, you gain another mythic ability.");
                bp.AddPrerequisiteFeature(MythicAbilitySelection, GroupType.Any);
                bp.GetComponents<PrerequisiteFeature>().ForEach(p => p.Group = GroupType.Any);
            });
            if (MAContext.Homebrew.Feats.IsDisabled("ExtraMythicAbility")) { return; }
            FeatTools.AddAsFeat(ExtraMythicAbility);
        }
    }
}
