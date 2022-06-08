using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.Items;

namespace MythicArcanist.NewComponents
{
    public class TWWEqualOpportunity : UnitFactComponentDelegate<AttackBonusConditional.RuntimeData>, 
		IInitiatorRulebookHandler<RuleAttackWithWeapon>, IRulebookHandler<RuleAttackWithWeapon>, 
		ISubscriber, IInitiatorRulebookSubscriber
		//IInitiatorRulebookHandler<RuleCalculateAttackBonus>, IRulebookHandler<RuleCalculateAttackBonus>
	{
		public bool ForceFlatFooted { get; set; }

		public void OnEventAboutToTrigger(RuleAttackWithWeapon evt)
		{
		}

		public void OnEventDidTrigger(RuleAttackWithWeapon evt)
		{
			MechanicsContext context = base.Context;
			UnitEntityData maybeCaster = context.MaybeCaster;
			ItemEntityWeapon firstWeapon = maybeCaster.Body.PrimaryHand.MaybeWeapon;
			ItemEntityWeapon secondWeapon = maybeCaster.Body.SecondaryHand.MaybeWeapon;
			UnitEntityData targetUnit = evt.Target;
			//bool didAttack = false;
			RuleAttackWithWeapon rule = new RuleAttackWithWeapon(maybeCaster, targetUnit, firstWeapon, 0);
			RuleAttackWithWeapon rule2 = new RuleAttackWithWeapon(maybeCaster, targetUnit, secondWeapon, 0);
			//using (eventHandlers.Activate())
			//{
			//	context.TriggerRule<RuleAttackWithWeapon>(rule);
			//	context.TriggerRule<RuleAttackWithWeapon>(rule2);
			//}

			if (evt.IsAttackOfOpportunity && !(evt.Weapon.HoldingSlot.MaybeItem == secondWeapon))
			{
				if (!evt.Target.Descriptor.State.IsDead)// && !didAttack)
				{
					Rulebook.Trigger(new RuleAttackWithWeapon(maybeCaster, targetUnit, secondWeapon, 0)
					{
						IsAttackOfOpportunity = true,
						ForceFlatFooted = ForceFlatFooted
					});
					//didAttack = true;
				}
			}
		}

		//public void OnEventAboutToTrigger(RuleCalculateAttackBonus evt)
		//{
		//	if (base.Data.Target != null && base.Data.Target == evt.Target)
		//	{
		//		evt.AddModifier(base.Data.AttackBonus, base.Fact, Descriptor);
		//	}
		//}
		//
		//public void OnEventDidTrigger(RuleCalculateAttackBonus evt)
		//{
		//}
	}
}
