using Kingmaker.Enums;
using Kingmaker.Items;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;

namespace MythicArcanist.NewComponents
{
	/*Twin Blades (Ex): At 5th level, a two-weapon warrior gains a +1 bonus on attack and damage rolls when making a full attack with two weapons or a double weapon. 
	 This bonus increases by +1 for every four levels after 5th.*/
	public class TWWTwinBlades : UnitFactComponentDelegate, ISubscriber, IInitiatorRulebookSubscriber,
		IInitiatorRulebookHandler<RuleCalculateAttackBonusWithoutTarget>, IRulebookHandler<RuleCalculateAttackBonusWithoutTarget>,
		IInitiatorRulebookHandler<RuleCalculateWeaponStats>, IRulebookHandler<RuleCalculateWeaponStats>
	{
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
			int rank = base.Fact.GetRank();
			int bonus = rank;
			if (ruleAttackWithWeapon.IsFullAttack)
			{
				evt.AddModifier(bonus, base.Fact, ModifierDescriptor.UntypedStackable);
			}
		}
		public void OnEventDidTrigger(RuleCalculateAttackBonusWithoutTarget evt)
		{
		}
		public void OnEventAboutToTrigger(RuleCalculateWeaponStats evt)
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
			int rank = base.Fact.GetRank();
			int bonus = rank;
			if (ruleAttackWithWeapon.IsFullAttack)
			{
				evt.AddDamageModifier(bonus, base.Fact, ModifierDescriptor.UntypedStackable);
			}
		}

		public void OnEventDidTrigger(RuleCalculateWeaponStats evt)
		{
		}
	}
}
