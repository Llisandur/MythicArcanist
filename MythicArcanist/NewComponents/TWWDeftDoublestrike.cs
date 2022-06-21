using Kingmaker.Blueprints;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.Items;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.ContextData;
using UnityEngine;
using TabletopTweaks.Core.Utilities;
using static MythicArcanist.Main;

namespace MythicArcanist.NewComponents
{
    /*Deft Doublestrike (Ex): At 17th level, when a two-weapon warrior hits an opponent with both weapons, he can make a disarm or sunder attempt 
	 (or trip, if one or both weapons can be used to trip) against that opponent as an immediate action that does not provoke attacks of opportunity.*/
    public class TWWDeftDoublestrike : UnitFactComponentDelegate, ISubscriber, IInitiatorRulebookSubscriber,
		IInitiatorRulebookHandler<RuleAttackWithWeapon>, IRulebookHandler<RuleAttackWithWeapon>
	{
		[SerializeField]
		public BlueprintBuffReference m_TestBuff;
		public BlueprintBuff Buff => m_TestBuff?.Get();
		public ActionList Action;
		public void OnEventAboutToTrigger(RuleAttackWithWeapon evt)
		{
		}
		
		public void OnEventDidTrigger(RuleAttackWithWeapon evt)
		{
			MechanicsContext context = base.Context;
			EntityFact fact = base.Fact;
			ItemEntityWeapon maybeWeapon = evt.Initiator.Body.PrimaryHand.MaybeWeapon;
			ItemEntityWeapon maybeWeapon2 = evt.Initiator.Body.SecondaryHand.MaybeWeapon;
			if (evt.AttackRoll.IsHit && evt.Weapon == maybeWeapon && !evt.Initiator.Buffs.HasFact(Buff))
			{
				evt.Initiator.Buffs.AddBuff(Buff, context, null);
			}
			if (evt.AttackRoll.IsHit && evt.Weapon == maybeWeapon2 && evt.Initiator.Buffs.HasFact(Buff))
			{
				evt.Initiator.Buffs.RemoveFact(Buff);
				RunActions(this, evt, context, fact);
			}
			if (!evt.AttackRoll.IsHit && evt.Initiator.Buffs.HasFact(Buff))
            {
				evt.Initiator.Buffs.RemoveFact(Buff);
			}
		}
		private static void RunActions(TWWDeftDoublestrike c, RuleAttackWithWeapon rule, MechanicsContext context, EntityFact fact)
		{
			UnitEntityData unitEntityData = rule.Target;
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
