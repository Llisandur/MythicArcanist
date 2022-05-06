using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Items;
using Kingmaker.Blueprints.Items.Equipment;
using Kingmaker.Blueprints.Items.Components;
using Kingmaker.Craft;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.Utility;
using TabletopTweaks.Core.ModLogic;
using TabletopTweaks.Core.Utilities;

using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Items;
using Kingmaker.Blueprints.Loot;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.Designers.Mechanics.Recommendations;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums.Damage;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.Utility;
using System.Linq;
using TabletopTweaks.Core.MechanicsChanges;
using TabletopTweaks.Core.NewComponents;
using TabletopTweaks.Core.Utilities;

namespace MythicArcanist.Utilities
{
    public static class ItemTools
    {
        public static BlueprintItemEquipmentUsable CreateScroll(ModContextBase modContext, BlueprintAbility Spell, Sprite Icon, Action<BlueprintItemEquipmentUsable> init = null)
        {
            var Scroll = Helpers.CreateBlueprint<BlueprintItemEquipmentUsable>(modContext, $"ScrollOf{Spell.name}", bp =>
            {
                var CasterLevel = 1;
                var SpellLevel = Spell.GetComponent<SpellListComponent>().SpellLevel;

                if (Spell.GetComponents<SpellListComponent>().Any(c => c.m_SpellList.Get() == (
                    SpellTools.SpellList.WizardSpellList |
                    SpellTools.SpellList.ClericSpellList |
                    SpellTools.SpellList.DruidSpellList |
                    SpellTools.SpellList.ShamanSpelllist)))
                {
                    CasterLevel = GetCasterLevel9thLevelPrepared(SpellLevel);
                }
                else if (Spell.GetComponents<SpellListComponent>().Any(c => c.m_SpellList.Get() == (
                    SpellTools.SpellList.BardSpellList |
                    SpellTools.SpellList.AlchemistSpellList |
                    SpellTools.SpellList.InquisitorSpellList |
                    SpellTools.SpellList.MagusSpellList |
                    SpellTools.SpellList.HunterSpelllist |
                    SpellTools.SpellList.WarpriestSpelllist)))
                {
                    CasterLevel = GetCasterLevel6thLevelCaster(SpellLevel);
                }
                else if (Spell.GetComponents<SpellListComponent>().Any(c => c.m_SpellList.Get() == (
                    SpellTools.SpellList.PaladinSpellList |
                    SpellTools.SpellList.RangerSpellList |
                    SpellTools.SpellList.BloodragerSpellList)))
                {
                    CasterLevel = GetCasterLevel4thLevelCaster(SpellLevel);
                }

                var Cost = 25 * CasterLevel * SpellLevel;

                bp.m_Overrides = new List<string>();
                bp.AddComponent<CopyScroll>();
                bp.m_DisplayNameText = Helpers.CreateString(modContext, $"{bp.name}.Name", $"Scroll of {Spell.m_DisplayName}");
                bp.m_DescriptionText = Helpers.CreateString(modContext, $"{bp.name}.Description", "");
                bp.m_FlavorText = Helpers.CreateString(modContext, $"{bp.name}.Flavor", "");
                bp.m_NonIdentifiedNameText = Helpers.CreateString(modContext, $"{bp.name}.Unidentified_Name", "Scroll");
                bp.m_NonIdentifiedDescriptionText = Helpers.CreateString(modContext, $"{bp.name}.Unidentified_Description", "");
                bp.m_Icon = Icon;
                bp.m_Cost = Cost;
                bp.m_Weight = 0.2F;
                bp.m_IsNotable = false;
                bp.m_Destructible = true;
                bp.m_ShardItem = BlueprintTools.GetBlueprintReference<BlueprintItemReference>("08133117418642fb9d1d2adba9785f43"); //PaperShardItem
                bp.m_MiscellaneousType = BlueprintItem.MiscellaneousItemType.None;
                bp.m_InventoryPutSound = "ScrollPut";
                bp.m_InventoryTakeSound = "ScrollTake";
                bp.NeedSkinningForCollect = false;
                bp.TrashLootTypes = new TrashLootType[] {TrashLootType.Scrolls, TrashLootType.Scrolls_RE};
                bp.CR = 0;
                bp.m_Ability = Spell.ToReference<BlueprintAbilityReference>();
                bp.m_ActivatableAbility = new BlueprintActivatableAbilityReference();
                bp.SpendCharges = true;
                bp.Charges = 1;
                bp.RestoreChargesOnRest = false;
                bp.CasterLevel = CasterLevel;
                bp.SpellLevel = SpellLevel;
                if (Spell.CanTargetEnemies) { bp.DC = GetDC(SpellLevel); } else { bp.DC = 0; }
                bp.IsNonRemovable = false;
                bp.m_EquipmentEntity = new KingmakerEquipmentEntityReference();
                bp.m_EquipmentEntityAlternatives = new KingmakerEquipmentEntityReference[0];
                bp.m_ForcedRampColorPresetIndex = 0;
                bp.Type = UsableItemType.Scroll;
                bp.m_IdentifyDC = 0;
                bp.m_InventoryEquipSound = "ScrollPut";
                bp.m_BeltItemPrefab = new Kingmaker.ResourceLinks.PrefabLink();
                bp.m_Enchantments = new BlueprintEquipmentEnchantmentReference[0];
            });
            PatchCraftRoot_ScrollsItems(modContext, Scroll);
            return Scroll;
        }
        private static void PatchCraftRoot_ScrollsItems(ModContextBase modContext, BlueprintItemEquipmentUsable Scroll)
        {
            CraftRoot CraftRoot = BlueprintTools.GetBlueprint<CraftRoot>("ecf0bbd052e4d7f44892dfd0601c812e");

            if (!CraftRoot.m_ScrollsItems.Contains(Scroll.ToReference<BlueprintItemEquipmentUsableReference>()))
            {
            CraftRoot.m_ScrollsItems.Add(Scroll.ToReference<BlueprintItemEquipmentUsableReference>());
            }
            modContext.Logger.Log($"Patched: {CraftRoot.ToReference<CraftRoot.Reference>()} - {CraftRoot.name} added {Scroll.ToReference<BlueprintItemEquipmentUsableReference>()} - {Scroll.name}");
        }
        public static void AddToVendor(ModContextBase modContext ,BlueprintItemEquipmentUsable Item, int Count, BlueprintSharedVendorTable VendorTable)
        {
            VendorTable.AddComponent<LootItemsPackFixed>(c =>
            {
                c.m_Item = new LootItem()
                {
                    m_Item = Item.ToReference<BlueprintItemReference>(),
                    m_Loot = new BlueprintUnitLootReference()
                };
                c.m_Count = Count;
            });
            modContext.Logger.Log($"Added: {Item} to {VendorTable}.");
        }
            private static int GetCasterLevel9thLevelPrepared(int SpellLevel)
        {
            switch (SpellLevel)
            {
                case 1:
                    return 1;
                case 2:
                    return 3;
                case 3:
                    return 5;
                case 4:
                    return 7;
                case 5:
                    return 9;
                case 6:
                    return 11;
                case 7:
                    return 13;
                case 8:
                    return 15;
                case 9:
                    return 17;
                default:
                    return 0;
            }
        }
        private static int GetCasterLevel9thLevelSpontaneous(int SpellLevel)
        {
            switch (SpellLevel)
            {
                case 1:
                    return 1;
                case 2:
                    return 4;
                case 3:
                    return 6;
                case 4:
                    return 8;
                case 5:
                    return 10;
                case 6:
                    return 12;
                case 7:
                    return 14;
                case 8:
                    return 16;
                case 9:
                    return 18;
                default:
                    return 0;
            }
        }
        private static int GetCasterLevel6thLevelCaster(int SpellLevel)
        {
            switch (SpellLevel)
            {
                case 1:
                    return 1;
                case 2:
                    return 4;
                case 3:
                    return 7;
                case 4:
                    return 10;
                case 5:
                    return 13;
                case 6:
                    return 16;
                default:
                    return 0;
            }
        }
        private static int GetCasterLevel4thLevelCaster(int SpellLevel)
        {
            switch (SpellLevel)
            {
                case 1:
                    return 1;
                case 2:
                    return 4;
                case 3:
                    return 7;
                case 4:
                    return 10;
                default:
                    return 0;
            }
        }
        private static int GetDC(int SpellLevel)
        {
            switch (SpellLevel)
            {
                case 1:
                    return 10 + SpellLevel + 0;
                case 2:
                    return 10 + SpellLevel + 1;
                case 3:
                    return 10 + SpellLevel + 1;
                case 4:
                    return 10 + SpellLevel + 2;
                case 5:
                    return 10 + SpellLevel + 2;
                case 6:
                    return 10 + SpellLevel + 3;
                case 7:
                    return 10 + SpellLevel + 3;
                case 8:
                    return 10 + SpellLevel + 4;
                case 9:
                    return 10 + SpellLevel + 4;
                default:
                    return 10 + SpellLevel + 0;
            }
        }
    }
}
