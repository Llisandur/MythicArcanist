using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using System;
using System.Linq;
using UnityEngine;
using TabletopTweaks.Core.NewComponents.AbilitySpecific;
using TabletopTweaks.Core.Utilities;
using static MythicArcanist.Main;

namespace MythicArcanist.NewContent.MythicAbilities
{
    static class AbundantPreparation
    {
        public static void AddAbundantPreparation()
        {
            var ArcanistClass = BlueprintTools.GetBlueprint<BlueprintCharacterClass>("52dbfd8505e22f84fad8d702611f60b7");
            //var icon = AssetLoader.LoadInternal(MAContext, folder: "MYthicAbilities", file: "Icon_AbundantPreparation.png");

            var AbundantPreparationFeature = Helpers.CreateBlueprint<BlueprintFeature>(MAContext, "AbundantPreparationFeature", bp =>
            {
                bp.SetName(MAContext, "Abundant Preparation");
                bp.SetDescription(MAContext, "You've learned a way to increase the number of {g|Encyclopedia:Spell}spells{/g} you can prepare per day.\n" +
                    "Benefit: You can prepare four more spells per day of 1st, 2nd, and 3rd ranks each. This ability does only affects arcanist spellbooks.");
                //bp.m_Icon = icon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.ReapplyOnLevelUp = true;
                bp.Groups = new FeatureGroup[] { FeatureGroup.MythicAbility };
                //bp.AddComponent<Helpers.Create<BlueprintSpellbookReference()>>(c => {});
                bp.AddPrerequisites(Helpers.Create<PrerequisiteClassLevel>(c =>
                {
                    c.m_CharacterClass = ArcanistClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                }));
            });

            if (MAContext.Homebrew.MythicAbilities.IsDisabled("AbundantPreparation")) { return; }
            FeatTools.AddAsMythicAbility(AbundantPreparationFeature);
        }
        //[HarmonyPatch(typeof(Spellbook), "GetSpellSlotsCount")]
        //public static class BlueprintSpellsTable_GetCount_Patch
        //{
        //    private static void Postfix(ref int __result, Spellbook __instance, int spellLevel)
        //    {
        //        if (__result > 0 && __instance.Blueprint.IsArcanist)
        //        {
        //            if (spellLevel > 0 && spellLevel <= 3)
        //            {
        //                var spellsKnown = __instance.m_KnownSpells[spellLevel].Count;
        //                __result = Math.Min(Mathf.RoundToInt(__result + 4), spellsKnown);
        //            }
        //        }
        //    }
        //}
    }
}
