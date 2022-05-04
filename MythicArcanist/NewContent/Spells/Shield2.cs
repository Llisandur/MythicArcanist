using System.Linq;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.Designers.Mechanics.Recommendations;
using Kingmaker.Utility;
using TabletopTweaks.Core.Utilities;
using static MythicArcanist.Main;

namespace MythicArcanist.NewContent.Spells
{
    static class MageShield2
    {
        public static void AddMageShield2()
        {
            BlueprintAbility SpellCopy = BlueprintTools.GetBlueprint<BlueprintAbility>("ef768022b0785eb43a18969903c537c4"); //MageShield
            BlueprintBuff SpellCopyBuff = BlueprintTools.GetBlueprint<BlueprintBuff>("9c0fa9b438ada3f43864be8dd8b3e741"); //MageShieldBuff

            string spellName = "MageShield2";
            string spellDisplay = "Shield II";
            string spellDesc = "This spell functions like shield, but its duration is longer and it can be employed like a tower shield for cover." +
                    "[LONGSTART]\n\nShield creates an invisible shield of force that hovers in front of you. It negates magic missile " +
                    "{g|Encyclopedia:Attacks}attacks{/g} directed at you. The disk als oprovides a +4 shield {g|Encyclopedia:Bonus}bonus{/g} to " +
                    "{g|Encyclopedia:Armor_Class}AC{/g}. This bonus applies against {g|Encyclopedia:Incorporeal_Touch_Attack}incorporeal{/g} " +
                    "{g|Encyclopedia:TouchAttack}touch attacks{/g}, since it is a force effect. The shield has no armor {g|Encyclopedia:Check}check{/g} " +
                    "{g|Encyclopedia:Penalty}penalty{/g} or {g|Encyclopedia:Spell_Fail_Chance}arcane spell failure{/g}.[LONGEND]";
            var icon = AssetLoader.LoadInternal(MAContext, folder: "Spells", file: $"Icon_{spellName}.png");

            var buff = SpellCopyBuff.CreateCopy(MAContext, $"{spellName}Buff", bp =>
            {
                bp.SetNameDescription(MAContext, spellDisplay, spellDesc);
                bp.m_Icon = icon;
            });
            
            var spell = SpellCopy.CreateCopy(MAContext, spellName, bp =>
            {
                bp.SetNameDescription(MAContext, spellDisplay, spellDesc);
                bp.m_Icon = icon;
                bp.RemoveComponents<SpellListComponent>();
                bp.RemoveComponents<RecommendationNoFeatFromGroup>();
                bp.AddComponent<SpellDescriptorComponent>(c =>
                {
                    c.Descriptor = SpellDescriptor.Force;
                });
                bp.GetComponent<AbilityEffectRunAction>()
                    .Actions.Actions
                    .OfType<ContextActionApplyBuff>().FirstOrDefault()
                    .m_Buff = buff.ToReference<BlueprintBuffReference>();
                bp.GetComponent<AbilityEffectRunAction>()
                    .Actions.Actions
                    .OfType<ContextActionApplyBuff>().FirstOrDefault()
                    .DurationValue.Rate = DurationRate.Hours;
                bp.LocalizedDuration = Helpers.CreateString(MAContext, $"{spellName}.Duration", "1 hour/level");
            });

            if (MAContext.Homebrew.Spells.IsDisabled("MageShield2")) { return; }
            spell.AddToSpellList(SpellTools.SpellList.BloodragerSpellList, 3);
            spell.AddToSpellList(SpellTools.SpellList.WizardSpellList, 4);
            spell.AddToSpellList(SpellTools.SpellList.LichWizardSpelllist, 4);
            //spell.AddToSpellList(SpellTools.SpellList.SummonerSpellList, 3);
        }
    }
}
