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

			if (evt.IsAttackOfOpportunity && (evt.Weapon.HoldingSlot.MaybeItem == firstWeapon))
			{
				if (!evt.Target.Descriptor.State.IsDead)
				{
					Rulebook.Trigger(new RuleAttackWithWeapon(maybeCaster, targetUnit, secondWeapon, 0)
					{
						IsAttackOfOpportunity = true,
						ForceFlatFooted = ForceFlatFooted
					});
				}
			}
		}
	}
}
