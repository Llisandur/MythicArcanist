using System;
using System.Collections.Generic;
using System.Linq;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using static Kingmaker.Blueprints.Classes.Prerequisites.Prerequisite;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Blueprints.Items.Armors;
using Kingmaker.Blueprints.Items.Equipment;
using Kingmaker.Blueprints.Root;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.ResourceLinks;
using Kingmaker.RuleSystem;
using TabletopTweaks.Core.Utilities;
using static MythicArcanist.Main;
using MythicArcanist.Utilities;

namespace MythicArcanist.NewContent.Archetypes.Fighter
{
    internal class TwoWeaponWarrior
    {
        public static void AddTwoWeaponWarriorArchetype()
        {
            #region Reference Blueprints
            var FighterClass = BlueprintTools.GetBlueprint<BlueprintCharacterClass>("48ac8db94d5de7645906c7d0ad3bcfbd");
            var FighterProgression = BlueprintTools.GetBlueprint<BlueprintProgression>("b50e94b57be32f74892f381ae2a8905a");

            var ArmorTraining = BlueprintTools.GetBlueprint<BlueprintFeature>("3c380607706f209499d951b29d3c44f3");
            var ArmorMastery = BlueprintTools.GetBlueprint<BlueprintFeature>("ae177f17cfb45264291d4d7c2cb64671");
            var WeaponTrainingSelection = BlueprintTools.GetBlueprint<BlueprintFeature>("b8cecf4e5e464ad41b79d5b42b76b399");
            var WeaponTrainingRankUpSelection = BlueprintTools.GetBlueprint<BlueprintFeature>("5f3cc7b9a46b880448275763fe70c0b0");

            var ColdIronLongsword = BlueprintTools.GetBlueprint<BlueprintItemWeapon>("533e10c8b4c6a4940a3767d096f4f05d");
            var ColdIronLightHammer = BlueprintTools.GetBlueprint<BlueprintItemWeapon>("0d63566868570f04b9dd69398b7ae239");
            var StandardLongbow = BlueprintTools.GetBlueprint<BlueprintItemWeapon>("201f6150321e09048bd59e9b7f558cb0");
            var ScaleMailStandard = BlueprintTools.GetBlueprint<BlueprintItemArmor>("d7963e1fcf260c148877afd3252dbc91");
            var PotionOfCureLightWounds = BlueprintTools.GetBlueprint<BlueprintItemEquipmentUsable>("d52566ae8cbe8dc4dae977ef51c27d91");

            #endregion
            #region New Abilities
            var DefensiveFlurry = Helpers.CreateBlueprint<BlueprintFeature>(ThisModContext, "TwoWeaponWarriorDefensiveFlurry", bp =>
            {
                bp.SetName(ThisModContext, "Defensive Flurry");
                bp.SetDescription(ThisModContext, "At 3rd level, when a two-weapon warrior makes a full attack with both weapons, he gains a +1 dodge bonus to AC against " +
                    "melee attacks until the beginning of his next turn. This bonus increases by +1 every four levels after 3rd.");
                bp.IsClassFeature = true;
            });
            var TwinBlades = Helpers.CreateBlueprint<BlueprintFeature>(ThisModContext, "TwoWeaponWarriorTwinBlades", bp =>
            {
                bp.SetName(ThisModContext, "Twin Blades");
                bp.SetDescription(ThisModContext, "At 5th level, a two-weapon warrior gains a +1 bonus on attack and damage rolls when making a full attack with two weapons " +
                    "or a double weapon. This bonus increases by +1 for every four levels after 5th.");
                //bp.m_AllowNonContextActions = false;
                //bp.m_Icon = ;
                //bp.HideInUI = false;
                //bp.HideInCharacterSheetAndLevelUp = false;
                //bp.HideNotAvailibleInUI = false;
                //bp.Groups = new FeatureGroup[0];
                bp.Ranks = 4;
                //bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
                //bp.IsPrerequisiteFor = new List<BlueprintFeatureReference> { };
            });
            var Doublestrike = Helpers.CreateBlueprint<BlueprintFeature>(ThisModContext, "TwoWeaponWarriorDoublestrike", bp =>
            {
                bp.SetName(ThisModContext, "Doublestrike");
                bp.SetDescription(ThisModContext, "At 9th level, a two-weapon warrior may, as a standard action, make one attack with both his primary and secondary weapons. " +
                    "The penalties for attacking with two weapons apply normally.");
                bp.IsClassFeature = true;
            });
            var ImprovedBalance = Helpers.CreateBlueprint<BlueprintFeature>(ThisModContext, "TwoWeaponWarriorImprovedBalance", bp =>
            {
                bp.SetName(ThisModContext, "Improved Balance");
                bp.SetDescription(ThisModContext, "At 11th level, the attack penalties for fighting with two weapons are reduced by –1 for a two-weapon warrior. Alternatively, " +
                    "he may use a one-handed weapon in his off-hand, treating it as if it were a light weapon with the normal light weapon penalties.");
                bp.IsClassFeature = true;
            });
            var EqualOpportunity = Helpers.CreateBlueprint<BlueprintFeature>(ThisModContext, "TwoWeaponWarriorEqualOpportunity", bp =>
            {
                bp.SetName(ThisModContext, "Equal Opportunity");
                bp.SetDescription(ThisModContext, "At 13th level, when a two-weapon warrior makes an attack of opportunity, he may attack once with both his primary and secondary " +
                    "weapons. The penalties for attacking with two weapons apply normally.");
                bp.IsClassFeature = true;
            });
            var PerfectBalance = Helpers.CreateBlueprint<BlueprintFeature>(ThisModContext, "TwoWeaponWarriorPerfectBalance", bp =>
            {
                bp.SetName(ThisModContext, "Perfect Balance");
                bp.SetDescription(ThisModContext, "At 15th level, the penalties for fighting with two weapons are reduced by an additional –1 for a two-weapon warrior. This benefit " +
                    "stacks with improved balance. If he is using a one-handed weapon in his off hand, treating it as a light weapon, he uses the normal light weapon penalties.");
                bp.IsClassFeature = true;
            });
            var DeftDoublestrike = Helpers.CreateBlueprint<BlueprintFeature>(ThisModContext, "TwoWeaponWarriorDeftDoublestrike", bp =>
            {
                bp.SetName(ThisModContext, "Deft Doublestrike");
                bp.SetDescription(ThisModContext, "At 17th level, when a two-weapon warrior hits an opponent with both weapons, he can make a disarm or sunder attempt (or trip, " +
                    "if one or both weapons can be used to trip) against that opponent as an immediate action that does not provoke attacks of opportunity.");
                bp.IsClassFeature = true;
            });
            var DeadlyDefense = Helpers.CreateBlueprint<BlueprintFeature>(ThisModContext, "TwoWeaponWarriorDeadlyDefense", bp =>
            {
                bp.SetName(ThisModContext, "Deadly Defense");
                bp.SetDescription(ThisModContext, "At 19th level, when a two-weapon warrior makes a full attack with both weapons, every creature that hits him with a melee attack " +
                    "before the beginning of his next turn provokes an attack of opportunity from the warrior.");
                bp.IsClassFeature = true;
            });
            #endregion

            var Archetype = Helpers.CreateBlueprint<BlueprintArchetype>(ThisModContext, "TwoWeaponWarrior", bp =>
            {
                bp.LocalizedName = Helpers.CreateString(ThisModContext, $"{bp.name}.Name", "Two-Weapon Warrior");
                bp.LocalizedDescription = Helpers.CreateString(ThisModContext, $"{bp.name}.Description", "Trained under great masters who preached the simple truth" +
                    "that two are better than one when it comes to weapons, the two-weapon warrior is a terror when his hands are full. From paired daggers to exotic" +
                    "double weapons, all combinations come equally alive in his skilled hands.");
                //bp.LocalizedDescriptionShort = bp.LocalizedDescription;
                bp.RemoveSpellbook = false;
                bp.BuildChanging = false;
                bp.AddFeatures = new LevelEntry[]
                {
                    Helpers.CreateLevelEntry(3, DefensiveFlurry),
                    Helpers.CreateLevelEntry(5, TwinBlades),
                    Helpers.CreateLevelEntry(9, Doublestrike, TwinBlades),
                    Helpers.CreateLevelEntry(11, ImprovedBalance),
                    Helpers.CreateLevelEntry(13, EqualOpportunity, TwinBlades),
                    Helpers.CreateLevelEntry(15, PerfectBalance),
                    Helpers.CreateLevelEntry(17, DeftDoublestrike, TwinBlades),
                    Helpers.CreateLevelEntry(19, DeadlyDefense)
                };
                bp.RemoveFeatures = new LevelEntry[]
                {
                    Helpers.CreateLevelEntry(3, ArmorTraining),
                    Helpers.CreateLevelEntry(7, ArmorTraining),
                    Helpers.CreateLevelEntry(11, ArmorTraining),
                    Helpers.CreateLevelEntry(15, ArmorTraining),
                    Helpers.CreateLevelEntry(19, ArmorMastery),
                    Helpers.CreateLevelEntry(5, WeaponTrainingSelection),
                    Helpers.CreateLevelEntry(9, WeaponTrainingSelection, WeaponTrainingRankUpSelection),
                    Helpers.CreateLevelEntry(13, WeaponTrainingSelection, WeaponTrainingRankUpSelection),
                    Helpers.CreateLevelEntry(17, WeaponTrainingSelection, WeaponTrainingRankUpSelection)
                };
                bp.ReplaceStartingEquipment = true;
                bp.StartingGold = 411;
                bp.m_StartingItems = new BlueprintItemReference[]
                {
                    ColdIronLongsword.ToReference<BlueprintItemReference>(),
                    ColdIronLightHammer.ToReference<BlueprintItemReference>(),
                    StandardLongbow.ToReference<BlueprintItemReference>(),
                    ScaleMailStandard.ToReference<BlueprintItemReference>(),
                    PotionOfCureLightWounds.ToReference<BlueprintItemReference>()
                };
                bp.ReplaceClassSkills = false;
                bp.ClassSkills = new StatType[0];
                bp.ChangeCasterType = false;
                bp.IsDivineCaster = false;
                bp.IsArcaneCaster = false;
                bp.AddSkillPoints = 0;
                bp.OverrideAttributeRecommendations = false;
                bp.RecommendedAttributes = new StatType[]
                {
                    StatType.Dexterity,
                    StatType.Constitution
                };
                bp.NotRecommendedAttributes = new StatType[]
                {
                    StatType.Charisma
                };
                bp.m_SignatureAbilities = new BlueprintFeatureReference[0];
                bp.m_BaseAttackBonus = null;
                bp.m_FortitudeSave = null;
                bp.m_ReflexSave = null;
                bp.m_WillSave = null;
                bp.m_Difficulty = 1;
            });

            FighterProgression.UIGroups = FighterProgression.UIGroups
                .AppendToArray(Helpers.CreateUIGroup(DefensiveFlurry, DeadlyDefense));
            FighterProgression.UIGroups = FighterProgression.UIGroups
                .AppendToArray(Helpers.CreateUIGroup(Doublestrike, DeftDoublestrike));
            FighterProgression.UIGroups = FighterProgression.UIGroups
                .AppendToArray(Helpers.CreateUIGroup(ImprovedBalance, PerfectBalance));

            if (ThisModContext.AddedContent.Archetypes.IsDisabled("TwoWeaponWarrior")) { return; }
            FighterClass.m_Archetypes = FighterClass.m_Archetypes.AppendToArray(Archetype.ToReference<BlueprintArchetypeReference>());
        }
    }
}
