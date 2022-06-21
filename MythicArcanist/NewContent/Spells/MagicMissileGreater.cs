using System.Linq;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Items;
using Kingmaker.Blueprints.Items.Equipment;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.Designers.Mechanics.Recommendations;
using Kingmaker.ElementsSystem;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.Utility;
using TabletopTweaks.Core.Utilities;
using static MythicArcanist.Main;
using MythicArcanist.Utilities;

namespace MythicArcanist.NewContent.Spells
{
    static class MagicMissileGreater
    {
        public static void Add()
        {
            BlueprintAbility SpellCopy = BlueprintTools.GetBlueprint<BlueprintAbility>("4ac47ddb9fa1eaf43a1b6809980cfbd2"); //MagicMissile

            string SpellName = "MagicMissileGreater";
            string SpellDisplay = "Magic Missile, Greater";
            string SpellDesc = "This spell functions like magic missile, except each missile deals 1d6 points of force damage + your key ability modifier. " +
                "If it strikes an effect that normally blocks magic missile, you may attempt to penetrate the effect as if against a spell resistance equal to " +
                "5 + the target's caster level or the target's Hit Dice if the target has no caster level.";
            var icon = AssetLoader.LoadInternal(ThisModContext, folder: "Spells", file: $"Icon_{SpellName}.png");
            var ScrollIcon = BlueprintTools.GetBlueprint<BlueprintItemEquipmentUsable>("63caf94a780472b448f50d0bc183c38f").Icon; //ScrollOfMagicMissile.Icon

            var Spell = SpellCopy.CreateCopy(ThisModContext, SpellName, bp =>
            {
                bp.m_Icon = icon;
                bp.SetNameDescription(ThisModContext, SpellDisplay, SpellDesc);
                bp.RemoveComponents<SpellListComponent>();
                bp.RemoveComponents<RecommendationNoFeatFromGroup>();

                bp.GetComponent<AbilityEffectRunAction>()
                    .Actions.Actions
                    .OfType<ContextActionDealDamage>().FirstOrDefault()
                    .Value.DiceType = DiceType.D6;
                bp.GetComponent<AbilityEffectRunAction>()
                    .Actions.Actions
                    .OfType<ContextActionDealDamage>().FirstOrDefault()
                    .Value.BonusValue.Value = 0;
                bp.GetComponent<AbilityEffectRunAction>()
                    .Actions.Actions
                    .OfType<ContextActionDealDamage>().FirstOrDefault()
                    .Value.BonusValue.ValueType = ContextValueType.AbilityParameter;
                bp.GetComponent<AbilityEffectRunAction>()
                    .Actions.Actions
                    .OfType<ContextActionDealDamage>().FirstOrDefault()
                    .Value.BonusValue.m_AbilityParameter = AbilityParameterType.CasterStatBonus;
            });


            if (ThisModContext.ThirdParty.Spells.IsDisabled("MagicMissileGreater")) { return; }
            Spell.AddToSpellList(SpellTools.SpellList.BloodragerSpellList, 4);
            Spell.AddToSpellList(SpellTools.SpellList.MagusSpellList, 4);
            Spell.AddToSpellList(SpellTools.SpellList.WizardSpellList, 4);
            var Scroll = Utilities.ItemTools.CreateScroll(ThisModContext, Spell, ScrollIcon);
            Utilities.ItemTools.AddToVendor(ThisModContext, Scroll, 2, BlueprintSharedVendorTables.Scrolls_DefendersHeartVendorTable);
            Utilities.ItemTools.AddToVendor(ThisModContext, Scroll, 4, BlueprintSharedVendorTables.WarCamp_ScrollVendorClericTable);
            Utilities.ItemTools.AddToVendor(ThisModContext, Scroll, 4, BlueprintSharedVendorTables.Scroll_Chapter3VendorTable);
            Utilities.ItemTools.AddToVendor(ThisModContext, Scroll, 7, BlueprintSharedVendorTables.Scroll_Chapter5VendorTable);
        }
    }
}
