using System.Linq;
using Kingmaker.Enums;
using Kingmaker.Items;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic.Parts;
using Kingmaker.UnitLogic;
using TabletopTweaks.Core.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using UnityEngine;
using UnityModManagerNet;

namespace MythicArcanist.NewComponents
{
    //Inspired by code for Prodigious Two-Weapon Fighting from phoenix99:Tome of the Phoenix.
    /*Improved Balance (Ex): At 11th level, the attack penalties for fighting with two weapons are reduced by –1 for a two-weapon warrior. 
		Alternatively, he may use a one-handed weapon in his off-hand, treating it as if it were a light weapon with the normal light weapon penalties. */
    /* Perfect Balance (Ex): At 15th level, the penalties for fighting with two weapons are reduced by an additional –1 for a two-weapon warrior. 
		This benefit stacks with improved balance. If he is using a one-handed weapon in his off hand, treating it as a light weapon, he uses the normal light weapon penalties.*/
    public class TWWBalance : UnitFactComponentDelegate, ISubscriber, IInitiatorRulebookSubscriber, 
		IInitiatorRulebookHandler<RuleCalculateAttackBonusWithoutTarget>, IRulebookHandler<RuleCalculateAttackBonusWithoutTarget>
	{
		public bool OneHandedOffhandBonus;
		[SerializeField]
		public BlueprintFeatureReference m_MythicBlueprint;
		public void OnEventAboutToTrigger(RuleCalculateAttackBonusWithoutTarget evt)
		{
			ItemEntityWeapon maybeWeapon = evt.Initiator.Body.PrimaryHand.MaybeWeapon;
			ItemEntityWeapon maybeWeapon2 = evt.Initiator.Body.SecondaryHand.MaybeWeapon;
			RuleAttackWithWeapon ruleAttackWithWeapon = evt.Reason.Rule as RuleAttackWithWeapon;
			bool flag = ruleAttackWithWeapon != null && !ruleAttackWithWeapon.IsFullAttack;
			if (evt.Weapon == null || maybeWeapon == null || maybeWeapon2 == null || maybeWeapon.Blueprint.IsNatural ||
				maybeWeapon2.Blueprint.IsNatural || maybeWeapon == evt.Initiator.Body.EmptyHandWeapon || maybeWeapon2 == evt.Initiator.Body.EmptyHandWeapon ||
				(maybeWeapon != evt.Weapon && maybeWeapon2 != evt.Weapon) || flag)
			{
				return;
			}
			int bonus = 1;
			UnitPartWeaponTraining unitPartWeaponTraining = base.Owner.Get<UnitPartWeaponTraining>();
			bool flag2 = base.Owner.State.Features.EffortlessDualWielding && unitPartWeaponTraining != null && unitPartWeaponTraining.IsSuitableWeapon(maybeWeapon2);
			bool flag3 = base.Owner.HasFact(TFBProdigiousTwoWeaponFighting) && TFBProdigiousTwoWeaponFighting != null;
			bool flag4 = base.Owner.HasFact(DCProdigiousTwoWeaponFighting) && DCProdigiousTwoWeaponFighting != null;
			if (!maybeWeapon2.Blueprint.IsLight && !maybeWeapon.Blueprint.Double && !maybeWeapon2.IsShield && !flag2)
			{
				if (OneHandedOffhandBonus && !(flag3 || flag4))
                {
					int offHandBonus = 2;
					evt.AddModifier(offHandBonus, base.Fact, ModifierDescriptor.UntypedStackable);
				}
				return;
			}
			if (!evt.Initiator.HasFact(this.MythicBlueprint))
            {
				evt.AddModifier(bonus, base.Fact, ModifierDescriptor.UntypedStackable);
            }
		}
	
		public void OnEventDidTrigger(RuleCalculateAttackBonusWithoutTarget evt)
        {
        }
		public BlueprintFeature MythicBlueprint
		{
			get
			{
				BlueprintFeatureReference mythicBlueprint = this.m_MythicBlueprint;
				if (mythicBlueprint == null)
				{
					return null;
				}
				return mythicBlueprint.Get();
			}
		}
		public BlueprintFeature TFBProdigiousTwoWeaponFighting
		{
			get
			{
				BlueprintFeatureReference blueprint = null;
				if (UnityModManager.modEntries.Where(mod => mod.Info.Id.Equals("TomeOfTheFirebird") && mod.Enabled && !mod.ErrorOnLoading).Any())
				{
					blueprint = BlueprintTools.GetBlueprintReference<BlueprintFeatureReference>("40f3f714d17347fa9f078d751b777f05"); //TomeOfTheFirebird:ProdigiousTWF
				}
				if (blueprint == null)
				{
					return null;
				}
				return blueprint.Get();
			}
		}
		public BlueprintFeature DCProdigiousTwoWeaponFighting
		{
			get
			{
				BlueprintFeatureReference blueprint = null;
				if (UnityModManager.modEntries.Where(mod => mod.Info.Id.Equals("DarkCodex") && mod.Enabled && !mod.ErrorOnLoading).Any())
				{
                    blueprint = BlueprintTools.GetBlueprintReference<BlueprintFeatureReference>("5725b7af6cd64bb8b53d6535f9de0161"); //DarkCodex:ProdigiousTwoWeaponFighting
				}
				if (blueprint == null)
				{
					return null;
				}
				return blueprint.Get();
			}
		}
	}
}
