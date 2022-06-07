using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.Items;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.ContextData;

namespace MythicArcanist.NewComponents
{
    /*Defensive Flurry (Ex): At 3rd level, when a two-weapon warrior makes a full attack with both weapons, he gains a +1 dodge bonus to AC against melee attacks 
	 until the beginning of his next turn. This bonus increases by +1 every four levels after 3rd.*/
    public class TWWDefensiveFlurry : UnitFactComponentDelegate, ISubscriber, IInitiatorRulebookSubscriber,
		IInitiatorRulebookHandler<RuleAttackWithWeapon>, IRulebookHandler<RuleAttackWithWeapon>
	{
		public ActionList Action;
		public void OnEventAboutToTrigger(RuleAttackWithWeapon evt)
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
			MechanicsContext context = base.Context;
			EntityFact fact = base.Fact;
			if (ruleAttackWithWeapon.IsFullAttack)
			{
				RunActions(this, evt, context, fact);
			}
		}
		public void OnEventDidTrigger(RuleAttackWithWeapon evt)
		{
		}
		private static void RunActions(TWWDefensiveFlurry c, RuleAttackWithWeapon rule, MechanicsContext context, EntityFact fact)
		{
			UnitEntityData unitEntityData = rule.Initiator;
			using (ContextData<ContextAttackData>.Request().Setup(rule.AttackRoll))
			{
				if (!fact.IsDisposed)
				{
					fact.RunActionInContext(c.Action, unitEntityData);
					return;
				}
				using (context.GetDataScope(unitEntityData))
				{
					c.Action.Run();
				}
			}
		}
	}
}
