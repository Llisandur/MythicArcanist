using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using System.Collections.Generic;
using TabletopTweaks.Core.Utilities;
using MythicArcanist.NewComponents;
using static MythicArcanist.Main;

namespace MythicArcanist.NewContent.MythicAbilities
{
    static class AbundantPreparationGreater
    {
        public static void Add()
        {
            var ArcanistClass = BlueprintTools.GetBlueprint<BlueprintCharacterClass>("52dbfd8505e22f84fad8d702611f60b7");
            var icon = AssetLoader.LoadInternal(ThisModContext, folder: "MythicAbilities", file: "Icon_AbundantPreparation.png");
            var AbundantPreparation = BlueprintTools.GetModBlueprint<BlueprintFeature>(ThisModContext, "AbundantPreparationFeature");
            var AbundantPreparationImproved = BlueprintTools.GetModBlueprint<BlueprintFeature>(ThisModContext, "AbundantPreparationImprovedFeature");

            var AbundantPreparationGreaterFeature = Helpers.CreateBlueprint<BlueprintFeature>(ThisModContext, "AbundantPreparationGreaterFeature", bp =>
            {
                bp.SetName(ThisModContext, "Greater Abundant Preparation");
                bp.SetDescription(ThisModContext, "You've mastered a way to increase the number of {g|Encyclopedia:Spell}spells{/g} you can prepare per day.\n" +
                    "Benefit: You can prepare four more spells per day of 7th, 8th, and 9th levels each. This ability only affects arcanist spellbooks.");
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
                    c.Levels = new int[] { 7, 8, 9 };
                });
                bp.AddPrerequisites(Helpers.Create<PrerequisiteClassLevel>(c =>
                {
                    c.m_CharacterClass = ArcanistClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                }));
                bp.AddPrerequisiteFeature(AbundantPreparation);
                bp.AddPrerequisiteFeature(AbundantPreparationImproved);
            });

            if (ThisModContext.Homebrew.MythicAbilities.IsDisabled("AbundantPreparationGreater")) { return; }
            FeatTools.AddAsMythicAbility(AbundantPreparationGreaterFeature);
        }
    }
}
