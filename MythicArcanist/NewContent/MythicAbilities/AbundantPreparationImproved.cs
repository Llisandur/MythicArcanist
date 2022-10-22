using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using System.Collections.Generic;
using TabletopTweaks.Core.Utilities;
using MythicArcanist.NewComponents;
using static MythicArcanist.Main;

namespace MythicArcanist.NewContent.MythicAbilities
{
    static class AbundantPreparationImproved
    {
        public static void Add()
        {
            var ArcanistClass = BlueprintTools.GetBlueprint<BlueprintCharacterClass>("52dbfd8505e22f84fad8d702611f60b7");
            var icon = AssetLoader.LoadInternal(ThisModContext, folder: "MythicAbilities", file: "Icon_AbundantPreparation.png");
            var AbundantPreparation = BlueprintTools.GetModBlueprint<BlueprintFeature>(ThisModContext, "AbundantPreparationFeature");

            var AbundantPreparationImprovedFeature = Helpers.CreateBlueprint<BlueprintFeature>(ThisModContext, "AbundantPreparationImprovedFeature", bp =>
            {
                bp.SetName(ThisModContext, "Improved Abundant Preparation");
                bp.SetDescription(ThisModContext, "You've studied a way to increase the number of {g|Encyclopedia:Spell}spells{/g} you can prepare per day.\n" +
                    "Benefit: You can prepare four more spells per day of 4th, 5th, and 6th levels each. This ability only affects arcanist spellbooks.");
                bp.m_Icon = icon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.ReapplyOnLevelUp = false;
                bp.Groups = new FeatureGroup[] { FeatureGroup.MythicAbility };
                bp.AddComponent<AddSpellSlots>(c =>
                {
                    c.Amount = 4;
                    //c.MinLevel = 1;
                    //c.MaxLevel = 3;
                    c.Levels = new int[] { 4, 5, 6 };
                });
                bp.AddPrerequisites(Helpers.Create<PrerequisiteClassLevel>(c =>
                {
                    c.m_CharacterClass = ArcanistClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                }));
                bp.IsPrerequisiteFor = new List<BlueprintFeatureReference>()
                {
                    //BlueprintTools.GetModBlueprint<BlueprintFeature>(ModContext, "AbundantPreparationGreaterFeature").ToReference<BlueprintFeatureReference>()
                };
                bp.AddPrerequisiteFeature(AbundantPreparation);
            });

            if (ThisModContext.Homebrew.MythicAbilities.IsDisabled("AbundantPreparationImproved")) { return; }
            FeatTools.AddAsMythicAbility(AbundantPreparationImprovedFeature);
        }
    }
}
