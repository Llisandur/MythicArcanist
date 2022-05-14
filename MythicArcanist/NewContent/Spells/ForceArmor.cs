using System.Linq;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Items;
using Kingmaker.Blueprints.Items.Equipment;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.Utility;
using TabletopTweaks.Core.Utilities;
using static MythicArcanist.Main;
using MythicArcanist.Utilities;

namespace MythicArcanist.NewContent.Spells
{
    static class ForceArmor
    {
        public static void AddForceArmor()
        {
            BlueprintAbility SpellCopy = BlueprintTools.GetBlueprint<BlueprintAbility>("183d5bb91dea3a1489a6db6c9cb64445"); //ShieldOfFaith
            BlueprintBuff SpellCopyBuff = BlueprintTools.GetBlueprint<BlueprintBuff>("5274ddc289f4a7447b7ace68ad8bebb0"); //ShieldOfFaithBuff

            string SpellName = "ForceArmor";
            string SpellDisplay = "Force Armor";
            string SpellDesc = "You wrap your body in force, gaining a +2 deflection {g|Encyclopedia:Bonus}bonus{/g} " +
                "to {g|Encyclopedia:Armor_Class}AC{/g}. At caster level 6th and every 6 caster levels thereafter, " +
                "this deflection bonus increases by +1 (to a maximum of +5 at 18th level).";
            //var icon = AssetLoader.LoadInternal(ModContext, folder: "Spells", file: $"Icon_{spellName}.png");
            var ScrollIcon = BlueprintTools.GetBlueprint<BlueprintItemEquipmentUsable>("44e6b7488b8912842a4793754a32c7ec").Icon; //ScollOfShieldOfFaith.Icon

            var Buff = SpellCopyBuff.CreateCopy(ThisModContext, $"{SpellName}Buff", bp =>
            {
                bp.SetNameDescription(ThisModContext, SpellDisplay, SpellDesc);
            });

            var Spell = SpellCopy.CreateCopy(ThisModContext, SpellName, bp =>
            {
                bp.SetNameDescription(ThisModContext, SpellDisplay, SpellDesc);
                bp.RemoveComponents<SpellListComponent>();
                bp.AddComponent<SpellDescriptorComponent>(c =>
                {
                    c.Descriptor = SpellDescriptor.Force;
                });
                bp.GetComponent<AbilityEffectRunAction>()
                    .Actions.Actions
                    .OfType<ContextActionApplyBuff>().FirstOrDefault()
                    .m_Buff = Buff.ToReference<BlueprintBuffReference>();
                bp.GetComponent<AbilityEffectRunAction>()
                    .Actions.Actions
                    .OfType<ContextActionApplyBuff>().FirstOrDefault()
                    .DurationValue.Rate = DurationRate.TenMinutes;
                bp.LocalizedDuration = Helpers.CreateString(ThisModContext, $"{SpellName}.Duration", "10 minutes/level");
                bp.GetComponent<AbilitySpawnFx>().Anchor = AbilitySpawnFxAnchor.Caster;
                bp.Range = AbilityRange.Personal;
                bp.AvailableMetamagic = Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten | Metamagic.CompletelyNormal;
            });


            if (ThisModContext.ThirdParty.Spells.IsDisabled("ForceArmor")) { return; }
            Spell.AddToSpellList(SpellTools.SpellList.AlchemistSpellList, 2);
            Spell.AddToSpellList(SpellTools.SpellList.BardSpellList, 2);
            Spell.AddToSpellList(SpellTools.SpellList.WizardSpellList, 2);
            var Scroll = Utilities.ItemTools.CreateScroll(ThisModContext, Spell, ScrollIcon);
            Utilities.ItemTools.AddToVendor(ThisModContext , Scroll, 2, BlueprintSharedVendorTables.Scrolls_DefendersHeartVendorTable);
            Utilities.ItemTools.AddToVendor(ThisModContext, Scroll, 4, BlueprintSharedVendorTables.WarCamp_ScrollVendorClericTable);
            Utilities.ItemTools.AddToVendor(ThisModContext, Scroll, 4, BlueprintSharedVendorTables.Scroll_Chapter3VendorTable);
            Utilities.ItemTools.AddToVendor(ThisModContext, Scroll, 7, BlueprintSharedVendorTables.Scroll_Chapter5VendorTable);
        }
    }
}
