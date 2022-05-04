using System.Linq;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.Designers.Mechanics.Recommendations;
using Kingmaker.Utility;
using TabletopTweaks.Core.Utilities;
using static MythicArcanist.Main;

namespace MythicArcanist.NewContent.Spells
{
    static class ForceArmor
    {
        public static void AddForceArmor()
        {
            BlueprintAbility SpellCopy = BlueprintTools.GetBlueprint<BlueprintAbility>("183d5bb91dea3a1489a6db6c9cb64445"); //ShieldOfFaith
            BlueprintBuff SpellCopyBuff = BlueprintTools.GetBlueprint<BlueprintBuff>("5274ddc289f4a7447b7ace68ad8bebb0"); //ShieldOfFaithBuff

            string spellName = "ForceArmor";
            string spellDisplay = "Force Armor";
            string spellDesc = "You wrap your body in force, gaining a +2 deflection {g|Encyclopedia:Bonus}bonus{/g} " +
                "to {g|Encyclopedia:Armor_Class}AC{/g}. At caster level 6th and every 6 caster levels thereafter, " +
                "this deflection bonus increases by +1 (to a maximum of +5 at 18th level).";
            //var icon = AssetLoader.LoadInternal(MAContext, folder: "Spells", file: $"Icon_{spellName}.png");

            var buff = SpellCopyBuff.CreateCopy(MAContext, $"{spellName}Buff", bp =>
            {
                bp.SetNameDescription(MAContext, spellDisplay, spellDesc);
            });

            var spell = SpellCopy.CreateCopy(MAContext, spellName, bp =>
            {
                bp.SetNameDescription(MAContext, spellDisplay, spellDesc);
                bp.RemoveComponents<SpellListComponent>();
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
                    .DurationValue.Rate = DurationRate.TenMinutes;
                bp.LocalizedDuration = Helpers.CreateString(MAContext, $"{spellName}.Duration", "10 minutes/level");
                bp.GetComponent<AbilitySpawnFx>().Anchor = AbilitySpawnFxAnchor.Caster;
                bp.Range = AbilityRange.Personal;
                bp.AvailableMetamagic = Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten | Metamagic.CompletelyNormal;
            });

            if (MAContext.Homebrew.Spells.IsDisabled("ForceArmor")) { return; }
            spell.AddToSpellList(SpellTools.SpellList.AlchemistSpellList, 2);
            spell.AddToSpellList(SpellTools.SpellList.BardSpellList, 2);
            spell.AddToSpellList(SpellTools.SpellList.WizardSpellList, 2);
        }
    }
}
