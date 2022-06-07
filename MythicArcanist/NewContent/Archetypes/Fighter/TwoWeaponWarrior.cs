using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Items.Armors;
using Kingmaker.Blueprints.Items.Equipment;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.Enums.Damage;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.Utility;
using Kingmaker.View.Animation;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TabletopTweaks.Core.Utilities;
using MythicArcanist.NewComponents;
using static MythicArcanist.Main;

namespace MythicArcanist.NewContent.Archetypes.Fighter
{
    internal class TwoWeaponWarrior
    {
        public static void Add()
        {
            #region Reference Blueprints
            var FighterClass = BlueprintTools.GetBlueprint<BlueprintCharacterClass>("48ac8db94d5de7645906c7d0ad3bcfbd");
            var FighterProgression = BlueprintTools.GetBlueprint<BlueprintProgression>("b50e94b57be32f74892f381ae2a8905a");

            var ArmorTraining = BlueprintTools.GetBlueprint<BlueprintFeature>("3c380607706f209499d951b29d3c44f3");
            var ArmorMastery = BlueprintTools.GetBlueprint<BlueprintFeature>("ae177f17cfb45264291d4d7c2cb64671");
            var WeaponTrainingSelection = BlueprintTools.GetBlueprint<BlueprintFeature>("b8cecf4e5e464ad41b79d5b42b76b399");
            var WeaponTrainingRankUpSelection = BlueprintTools.GetBlueprint<BlueprintFeature>("5f3cc7b9a46b880448275763fe70c0b0");

            var TwoWeaponFightingMythicFeat = BlueprintTools.GetBlueprintReference<BlueprintFeatureReference>("c6afbb8c1a36a704a8041f35498f41a4");

            var ColdIronLongsword = BlueprintTools.GetBlueprint<BlueprintItemWeapon>("533e10c8b4c6a4940a3767d096f4f05d");
            var ColdIronLightHammer = BlueprintTools.GetBlueprint<BlueprintItemWeapon>("0d63566868570f04b9dd69398b7ae239");
            var StandardLongbow = BlueprintTools.GetBlueprint<BlueprintItemWeapon>("201f6150321e09048bd59e9b7f558cb0");
            var ScaleMailStandard = BlueprintTools.GetBlueprint<BlueprintItemArmor>("d7963e1fcf260c148877afd3252dbc91");
            var PotionOfCureLightWounds = BlueprintTools.GetBlueprint<BlueprintItemEquipmentUsable>("d52566ae8cbe8dc4dae977ef51c27d91");

            #endregion
            #region New Abilities
            #region Defensive Flurry
            string DefensiveFlurryName = "Defensive Flurry";
            string DefensiveFlurryDesc = "At 3rd level, when a two-weapon warrior makes a full attack with both weapons, he gains a +1 dodge bonus to AC against " +
                    "melee attacks until the beginning of his next turn. This bonus increases by +1 every four levels after 3rd.";
            var DefensiveFlurryBuff = Helpers.CreateBlueprint<BlueprintBuff>(ThisModContext, "TwoWeaponWarriorDefensiveFlurryBuff", bp =>
            {
                bp.AddComponent<ACBonusAgainstAttacks>(c =>
                {
                    c.AgainstMeleeOnly = true;
                    c.Value = new ContextValue()
                    {
                        ValueType = ContextValueType.Rank,
                        Value = 1,
                        ValueRank = AbilityRankType.Default,
                        ValueShared = AbilitySharedValue.Damage,
                        Property = UnitProperty.None
                    };
                    c.ArmorClassBonus = 0;
                    c.Descriptor = ModifierDescriptor.None;
                });
                bp.AddComponent<ContextRankConfig>(c =>
                {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.DelayedStartPlusDivStep;
                    c.m_StartLevel = 3;
                    c.m_StepLevel = 4;
                    c.m_Class = new BlueprintCharacterClassReference[]
                    {
                        BlueprintTools.GetBlueprintReference<BlueprintCharacterClassReference>("48ac8db94d5de7645906c7d0ad3bcfbd") //FighterClass
                    };
                });
                bp.SetName(ThisModContext, DefensiveFlurryName);
                bp.SetDescription(ThisModContext, DefensiveFlurryDesc);
                bp.m_DescriptionShort = Helpers.CreateString(ThisModContext, $"{bp.name}.DescriptionShort", "");
                bp.IsClassFeature = true;
                //bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.Stacking = StackingType.Replace;
                bp.Ranks = 1;
                bp.TickEachSecond = false;
                bp.Frequency = DurationRate.Rounds;
            });
            var DefensiveFlurry = Helpers.CreateBlueprint<BlueprintFeature>(ThisModContext, "TwoWeaponWarriorDefensiveFlurry", bp =>
            {
                bp.SetName(ThisModContext, DefensiveFlurryName);
                bp.SetDescription(ThisModContext, DefensiveFlurryDesc);
                bp.m_Icon = BlueprintTools.GetBlueprint<BlueprintFeature>("be50f4e97fff8a24ba92561f1694a945").Icon; //SpellStrikeFeature
                bp.IsClassFeature = true;
                bp.Ranks = 5;
                bp.AddComponent<TWWDefensiveFlurry>(c =>
                {
                    c.Action = Helpers.CreateActionList(
                        Helpers.Create<ContextActionApplyBuff>(c =>
                        {
                            c.m_Buff = DefensiveFlurryBuff.ToReference<BlueprintBuffReference>();
                            c.DurationValue = new ContextDurationValue()
                            {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 1
                            };
                        }));
                });
            });
            #endregion
            var TwinBlades = Helpers.CreateBlueprint<BlueprintFeature>(ThisModContext, "TwoWeaponWarriorTwinBlades", bp =>
            {
                bp.SetName(ThisModContext, "Twin Blades");
                bp.SetDescription(ThisModContext, "At 5th level, a two-weapon warrior gains a +1 bonus on attack and damage rolls when making a full attack with two weapons " +
                    "or a double weapon. This bonus increases by +1 for every four levels after 5th.");
                bp.m_Icon = BlueprintTools.GetBlueprint<BlueprintAbility>("9d5d2d3ffdd73c648af3eb3e585b1113").Icon; //DivineFavor
                bp.Ranks = 5;
                bp.IsClassFeature = true;
                bp.AddComponent(new TWWTwinBlades());
            });
            #region Doublestrike
            string DoublestrikeName = "Doublestrike";
            string DoublestrikeDescription = "At 9th level, a two-weapon warrior may, as a standard action, make one attack with both his primary and secondary weapons. " +
                    "The penalties for attacking with two weapons apply normally.";
            var DoublestrikeIcon = BlueprintTools.GetBlueprint<BlueprintFeature>("8ec618121de114845981933a3d5c4b02").Icon; //ImpromptuSneakAttack
            var DoublestrikeAbility = Helpers.CreateBlueprint<BlueprintAbility>(ThisModContext, "TwoWeaponWarriorDoublestrikeAbility", bp =>
            {
                bp.SetName(ThisModContext, DoublestrikeName);
                bp.SetDescription(ThisModContext, DoublestrikeDescription);
                bp.LocalizedDuration = Helpers.CreateString(ThisModContext, $"{bp.name}.Duration", "");
                bp.LocalizedSavingThrow = Helpers.CreateString(ThisModContext, $"{bp.name}.SavingThrow", "");
                bp.m_Icon = DoublestrikeIcon;
                bp.Type = AbilityType.Physical;
                bp.Range = AbilityRange.Weapon;
                bp.CanTargetEnemies = true;
                bp.NeedEquipWeapons = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Special;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AddComponent(new TWWDoublestrike());
            });
            var Doublestrike = Helpers.CreateBlueprint<BlueprintFeature>(ThisModContext, "TwoWeaponWarriorDoublestrike", bp =>
            {
                bp.SetName(ThisModContext, DoublestrikeName);
                bp.SetDescription(ThisModContext, DoublestrikeDescription);
                bp.m_Icon = DoublestrikeIcon;
                bp.IsClassFeature = true;
                bp.AddComponent<AddFacts>(c =>
                {
                    c.m_Facts = new BlueprintUnitFactReference[]
                    {
                        DoublestrikeAbility.ToReference<BlueprintUnitFactReference>()
                    };
                });
            });
            #endregion
            var ImprovedBalance = Helpers.CreateBlueprint<BlueprintFeature>(ThisModContext, "TwoWeaponWarriorImprovedBalance", bp =>
            {
                bp.SetName(ThisModContext, "Improved Balance");
                bp.SetDescription(ThisModContext, "At 11th level, the attack penalties for fighting with two weapons are reduced by –1 for a two-weapon warrior. Alternatively, " +
                    "he may use a one-handed weapon in his off-hand, treating it as if it were a light weapon with the normal light weapon penalties.");
                bp.m_Icon = BlueprintTools.GetBlueprint<BlueprintAbility>("779179912e6c6fe458fa4cfb90d96e10").Icon; //LeadBlades
                bp.IsClassFeature = true;
                bp.AddComponent<TWWBalance>(c =>
                {
                    c.OneHandedOffhandBonus = true;
                    c.m_MythicBlueprint = TwoWeaponFightingMythicFeat;
                });
            });
            var EqualOpportunity = Helpers.CreateBlueprint<BlueprintFeature>(ThisModContext, "TwoWeaponWarriorEqualOpportunity", bp =>
            {
                bp.SetName(ThisModContext, "Equal Opportunity");
                bp.SetDescription(ThisModContext, "At 13th level, when a two-weapon warrior makes an attack of opportunity, he may attack once with both his primary and secondary " +
                    "weapons. The penalties for attacking with two weapons apply normally.");
                bp.m_Icon = BlueprintTools.GetBlueprint<BlueprintAbility>("831e942864e924846a30d2e0678e438b").Icon; //BlessWeapon
                bp.IsClassFeature = true;
            });
            var PerfectBalance = Helpers.CreateBlueprint<BlueprintFeature>(ThisModContext, "TwoWeaponWarriorPerfectBalance", bp =>
            {
                bp.SetName(ThisModContext, "Perfect Balance");
                bp.SetDescription(ThisModContext, "At 15th level, the penalties for fighting with two weapons are reduced by an additional –1 for a two-weapon warrior. This benefit " +
                    "stacks with improved balance. If he is using a one-handed weapon in his off hand, treating it as a light weapon, he uses the normal light weapon penalties.");
                bp.m_Icon = BlueprintTools.GetBlueprint<BlueprintAbility>("779179912e6c6fe458fa4cfb90d96e10").Icon; //LeadBlades
                bp.IsClassFeature = true;
                bp.AddComponent<TWWBalance>(c =>
                {
                    c.OneHandedOffhandBonus = false;
                    c.m_MythicBlueprint = TwoWeaponFightingMythicFeat;
                });
            });
            var DeftDoublestrike = Helpers.CreateBlueprint<BlueprintFeature>(ThisModContext, "TwoWeaponWarriorDeftDoublestrike", bp =>
            {
                bp.SetName(ThisModContext, "Deft Doublestrike");
                bp.SetDescription(ThisModContext, "At 17th level, when a two-weapon warrior hits an opponent with both weapons, he can make a disarm or sunder attempt (or trip, " +
                    "if one or both weapons can be used to trip) against that opponent as an immediate action that does not provoke attacks of opportunity.");
                bp.m_Icon = BlueprintTools.GetBlueprint<BlueprintFeature>("72dcf1fb106d5054a81fd804fdc168d3").Icon; //MasterStrike
                bp.IsClassFeature = true;
            });
            var DeadlyDefense = Helpers.CreateBlueprint<BlueprintFeature>(ThisModContext, "TwoWeaponWarriorDeadlyDefense", bp =>
            {
                bp.SetName(ThisModContext, "Deadly Defense");
                bp.SetDescription(ThisModContext, "At 19th level, when a two-weapon warrior makes a full attack with both weapons, every creature that hits him with a melee attack " +
                    "before the beginning of his next turn provokes an attack of opportunity from the warrior.");
                bp.m_Icon = BlueprintTools.GetBlueprint<BlueprintAbility>("464a7193519429f48b4d190acb753cf0").Icon; //Grace
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
                    Helpers.CreateLevelEntry(7, DefensiveFlurry),
                    Helpers.CreateLevelEntry(9, Doublestrike, TwinBlades),
                    Helpers.CreateLevelEntry(11, ImprovedBalance, DefensiveFlurry),
                    Helpers.CreateLevelEntry(13, EqualOpportunity, TwinBlades),
                    Helpers.CreateLevelEntry(15, PerfectBalance, DefensiveFlurry),
                    Helpers.CreateLevelEntry(17, DeftDoublestrike, TwinBlades),
                    Helpers.CreateLevelEntry(19, DeadlyDefense, DefensiveFlurry)
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
                .AppendToArray(Helpers.CreateUIGroup(DefensiveFlurry));
            FighterProgression.UIGroups = FighterProgression.UIGroups
                .AppendToArray(Helpers.CreateUIGroup(TwinBlades));
            FighterProgression.UIGroups = FighterProgression.UIGroups
                .AppendToArray(Helpers.CreateUIGroup(Doublestrike, DeftDoublestrike));
            FighterProgression.UIGroups = FighterProgression.UIGroups
                .AppendToArray(Helpers.CreateUIGroup(ImprovedBalance, PerfectBalance));

            if (ThisModContext.AddedContent.Archetypes.IsDisabled("TwoWeaponWarrior")) { return; }
            FighterClass.m_Archetypes = FighterClass.m_Archetypes.AppendToArray(Archetype.ToReference<BlueprintArchetypeReference>());
        }
    }
}
