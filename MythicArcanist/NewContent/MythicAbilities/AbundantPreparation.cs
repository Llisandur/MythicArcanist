using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using System.Collections.Generic;
using TabletopTweaks.Core.Utilities;
using MythicArcanist.NewComponents;
using static MythicArcanist.Main;

namespace MythicArcanist.NewContent.MythicAbilities
{
    static class AbundantPreparation
    {
        public static void AddAbundantPreparation()
        {
            var ArcanistClass = BlueprintTools.GetBlueprint<BlueprintCharacterClass>("52dbfd8505e22f84fad8d702611f60b7");
            var icon = AssetLoader.LoadInternal(ThisModContext, folder: "MythicAbilities", file: "Icon_AbundantPreparation.png");

            var AbundantPreparationFeature = Helpers.CreateBlueprint<BlueprintFeature>(ThisModContext, "AbundantPreparationFeature", bp =>
            {
                bp.SetName(ThisModContext, "Abundant Preparation");
                bp.SetDescription(ThisModContext, "You've learned a way to increase the number of {g|Encyclopedia:Spell}spells{/g} you can prepare per day.\n" +
                    "Benefit: You can prepare four more spells per day of 1st, 2nd, and 3rd levels each. This ability only affects arcanist spellbooks.");
                bp.m_Icon = icon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.ReapplyOnLevelUp = false;
                bp.Groups = new FeatureGroup[] { FeatureGroup.MythicAbility };
                bp.AddComponent<AddSpellSlotsMA>(c =>
                {
                    c.Amount = 4;
                    //c.MinLevel = 1;
                    //c.MaxLevel = 3;
                    c.Levels = new int[] { 1, 2, 3 };
                });
                bp.AddPrerequisites(Helpers.Create<PrerequisiteClassLevel>(c =>
                {
                    c.m_CharacterClass = ArcanistClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                }));
                bp.IsPrerequisiteFor = new List<BlueprintFeatureReference>()
                {
                    //BlueprintTools.GetModBlueprint<BlueprintFeature>(ModContext, "AbundantPreparationImprovedFeature").ToReference<BlueprintFeatureReference>(),
                    //BlueprintTools.GetModBlueprint<BlueprintFeature>(ModContext, "AbundantPreparationGreaterFeature").ToReference<BlueprintFeatureReference>()
                };
            });

            if (ThisModContext.Homebrew.MythicAbilities.IsDisabled("AbundantPreparation")) { return; }
            FeatTools.AddAsMythicAbility(AbundantPreparationFeature);
        }
    }
}
