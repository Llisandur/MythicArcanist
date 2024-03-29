﻿using System;
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
using Kingmaker.Blueprints.Loot;

namespace MythicArcanist.Utilities
{
    public static class ItemTools
    {
        public static BlueprintItemEquipmentUsable CreateScroll(ModContextBase modContext, BlueprintAbility Spell, Sprite Icon, Action<BlueprintItemEquipmentUsable> init = null)
        {
            var Scroll = Helpers.CreateBlueprint<BlueprintItemEquipmentUsable>(modContext, $"ScrollOf{Spell.name}", bp =>
            {
                int SpellLevel = GetSpellLevel(modContext, Spell);
                int CasterLevel = GetCasterLevel(modContext, Spell, SpellLevel);
                int Cost = 25 * CasterLevel * SpellLevel;

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
        private static int GetClassSpellLevel(BlueprintAbility Spell, BlueprintSpellList ClassSpellList)
        {
            foreach (SpellListComponent SpellList in Spell.GetComponents<SpellListComponent>())
            {
                if (SpellList.m_SpellList.Is(ClassSpellList)) { return SpellList.SpellLevel; }
            }
            return -1;
        }
        private static int GetSpellLevel(ModContextBase modContext, BlueprintAbility Spell)
        {
            int ClassSpellLevel;

            ClassSpellLevel = GetClassSpellLevel(Spell, SpellTools.SpellList.WizardSpellList);
            if (ClassSpellLevel > 0) { modContext.Logger.Log($"Spell level set to Wizard {ClassSpellLevel}"); return ClassSpellLevel; }

            ClassSpellLevel = GetClassSpellLevel(Spell, SpellTools.SpellList.ClericSpellList);
            if (ClassSpellLevel > 0) { modContext.Logger.Log($"Spell level set to Cleric {ClassSpellLevel}"); return ClassSpellLevel; }

            ClassSpellLevel = GetClassSpellLevel(Spell, SpellTools.SpellList.DruidSpellList);
            if (ClassSpellLevel > 0) { modContext.Logger.Log($"Spell level set to Druid {ClassSpellLevel}"); return ClassSpellLevel; }

            ClassSpellLevel = GetClassSpellLevel(Spell, SpellTools.SpellList.WitchSpellList);
            if (ClassSpellLevel > 0) { modContext.Logger.Log($"Spell level set to Witch {ClassSpellLevel}"); return ClassSpellLevel; }

            ClassSpellLevel = GetClassSpellLevel(Spell, SpellTools.SpellList.ShamanSpelllist);
            if (ClassSpellLevel > 0) { modContext.Logger.Log($"Spell level set to Shaman {ClassSpellLevel}"); return ClassSpellLevel; }

            ClassSpellLevel = GetClassSpellLevel(Spell, SpellTools.SpellList.BardSpellList);
            if (ClassSpellLevel > 0) { modContext.Logger.Log($"Spell level set to Bard {ClassSpellLevel}"); return ClassSpellLevel; }

            ClassSpellLevel = GetClassSpellLevel(Spell, SpellTools.SpellList.AlchemistSpellList);
            if (ClassSpellLevel > 0) { modContext.Logger.Log($"Spell level set to Alchemist {ClassSpellLevel}"); return ClassSpellLevel; }

            ClassSpellLevel = GetClassSpellLevel(Spell, SpellTools.SpellList.InquisitorSpellList);
            if (ClassSpellLevel > 0) { modContext.Logger.Log($"Spell level set to Inquisitor {ClassSpellLevel}"); return ClassSpellLevel; }

            ClassSpellLevel = GetClassSpellLevel(Spell, SpellTools.SpellList.MagusSpellList);
            if (ClassSpellLevel > 0) { modContext.Logger.Log($"Spell level set to Magus {ClassSpellLevel}"); return ClassSpellLevel; }

            ClassSpellLevel = GetClassSpellLevel(Spell, SpellTools.SpellList.HunterSpelllist);
            if (ClassSpellLevel > 0) { modContext.Logger.Log($"Spell level set to Hunter {ClassSpellLevel}"); return ClassSpellLevel; }

            ClassSpellLevel = GetClassSpellLevel(Spell, SpellTools.SpellList.WarpriestSpelllist);
            if (ClassSpellLevel > 0) { modContext.Logger.Log($"Spell level set to Warpriest {ClassSpellLevel}"); return ClassSpellLevel; }

            ClassSpellLevel = GetClassSpellLevel(Spell, SpellTools.SpellList.PaladinSpellList);
            if (ClassSpellLevel > 0) { modContext.Logger.Log($"Spell level set to Paladin {ClassSpellLevel}"); return ClassSpellLevel; }

            ClassSpellLevel = GetClassSpellLevel(Spell, SpellTools.SpellList.RangerSpellList);
            if (ClassSpellLevel > 0) { modContext.Logger.Log($"Spell level set to Ranger {ClassSpellLevel}"); return ClassSpellLevel; }

            ClassSpellLevel = GetClassSpellLevel(Spell, SpellTools.SpellList.BloodragerSpellList);
            if (ClassSpellLevel > 0) { modContext.Logger.Log($"Spell level set to Bloodrager {ClassSpellLevel}"); return ClassSpellLevel; }

            modContext.Logger.Log($"Unable to get spell level.");
            return -1;
        }
        private static int GetCasterLevel(ModContextBase modContext, BlueprintAbility Spell, int SpellLevel)
        {
            int CasterLevel = 1;

            if (Spell.GetComponents<SpellListComponent>().Any(c => c.m_SpellList.Get() == (
                SpellTools.SpellList.WizardSpellList |
                SpellTools.SpellList.ClericSpellList |
                SpellTools.SpellList.DruidSpellList |
                SpellTools.SpellList.WitchSpellList |
                SpellTools.SpellList.ShamanSpelllist)))
            {
                CasterLevel = GetCasterLevel9thLevelPrepared(SpellLevel);
                modContext.Logger.Log($"Caster level set to {CasterLevel} using 9th-level caster level list.");
                return CasterLevel;
            }
            if (Spell.GetComponents<SpellListComponent>().Any(c => c.m_SpellList.Get() == (
                SpellTools.SpellList.BardSpellList |
                SpellTools.SpellList.AlchemistSpellList |
                SpellTools.SpellList.InquisitorSpellList |
                SpellTools.SpellList.MagusSpellList |
                SpellTools.SpellList.HunterSpelllist |
                SpellTools.SpellList.WarpriestSpelllist)))
            {
                CasterLevel = GetCasterLevel6thLevelCaster(SpellLevel);
                modContext.Logger.Log($"Caster level set to {CasterLevel} using 6th-level caster level list.");
                return CasterLevel;
            }
            if (Spell.GetComponents<SpellListComponent>().Any(c => c.m_SpellList.Get() == (
                SpellTools.SpellList.PaladinSpellList |
                SpellTools.SpellList.RangerSpellList |
                SpellTools.SpellList.BloodragerSpellList)))
            {
                CasterLevel = GetCasterLevel4thLevelCaster(SpellLevel);
                modContext.Logger.Log($"Caster level set to {CasterLevel} using 4th-level caster level list.");
                return CasterLevel;
            }

            modContext.Logger.Log($"Unable to get caster level.");
            return CasterLevel;
        }
        private static void PatchCraftRoot_ScrollsItems(ModContextBase modContext, BlueprintItemEquipmentUsable Scroll)
        {
            CraftRoot CraftRoot = BlueprintTools.GetBlueprint<CraftRoot>("ecf0bbd052e4d7f44892dfd0601c812e");

            if (!CraftRoot.m_ScrollsItems.Contains(Scroll.ToReference<BlueprintItemEquipmentUsableReference>()))
            {
            CraftRoot.m_ScrollsItems.Add(Scroll.ToReference<BlueprintItemEquipmentUsableReference>());
            }
            modContext.Logger.Log($"Patched:{Scroll.ToReference<BlueprintItemEquipmentUsableReference>()} - {Scroll.name} added to {CraftRoot.ToReference<CraftRoot.Reference>()} - {CraftRoot.name}");
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
            modContext.Logger.Log($"Added: {Count} {Item} to {VendorTable}.");
        }
        private static int GetCasterLevel9thLevelPrepared(int SpellLevel)
        {
            /*
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
            }*/
            return SpellLevel switch
            {
                1 => 1,
                2 => 3,
                3 => 5,
                4 => 7,
                5 => 9,
                6 => 11,
                7 => 13,
                8 => 15,
                9 => 17,
                _ => 0
            };
        }
        private static int GetCasterLevel9thLevelSpontaneous(int SpellLevel)
        {
            /*
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
            }*/
            return SpellLevel switch
            {
                1 => 1,
                2 => 4,
                3 => 6,
                4 => 8,
                5 => 10,
                6 => 12,
                7 => 14,
                8 => 16,
                9 => 18,
                _ => 0
            };
        }
        private static int GetCasterLevel6thLevelCaster(int SpellLevel)
        {
            /*
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
            }*/
            return SpellLevel switch
            {
                1 => 1,
                2 => 4,
                3 => 7,
                4 => 10,
                5 => 13,
                6 => 16,
                _ => 0
            };
        }
        private static int GetCasterLevel4thLevelCaster(int SpellLevel)
        {
            /*
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
            }*/
            return SpellLevel switch
            {
                1 => 1,
                2 => 4,
                3 => 7,
                4 => 10,
                _ => 0
            };
        }
        private static int GetDC(int SpellLevel)
        {
            /*
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
            }*/
            return SpellLevel switch
            {
                1 => 10 + SpellLevel + 0,
                2 => 10 + SpellLevel + 1,
                3 => 10 + SpellLevel + 1,
                4 => 10 + SpellLevel + 2,
                5 => 10 + SpellLevel + 2,
                6 => 10 + SpellLevel + 3,
                7 => 10 + SpellLevel + 3,
                8 => 10 + SpellLevel + 4,
                9 => 10 + SpellLevel + 4,
                _ => 10 + SpellLevel + 0
            };
        }
    }
}
