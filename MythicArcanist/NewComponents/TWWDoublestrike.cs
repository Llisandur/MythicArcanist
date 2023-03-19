using System;
using System.Collections.Generic;
using Kingmaker;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.Items;
using Kingmaker.Items.Slots;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.Utility;

namespace MythicArcanist.NewComponents
{
    public class TWWDoublestrike : AbilityCustomLogic
    {
        /*Doublestrike (Ex): At 9th level, a two-weapon warrior may, as a standard action, make one attack with both his primary and secondary weapons. 
         The penalties for attacking with two weapons apply normally.*/
        public override IEnumerator<AbilityDeliveryTarget> Deliver(AbilityExecutionContext context, TargetWrapper target)
        {
            UnitEntityData maybeCaster = context.MaybeCaster;
            if (maybeCaster == null)
            {
                PFLog.Default.Error(this, "Caster is missing");
                yield break;
            }
            WeaponSlot threatHand = maybeCaster.GetThreatHandMelee();
            if (threatHand == null)
            {
                PFLog.Default.Error("Caster can't attack");
                yield break;
            }
            ItemEntityWeapon firstWeapon = maybeCaster.Body.PrimaryHand.MaybeWeapon;
            ItemEntityWeapon secondWeapon = maybeCaster.Body.SecondaryHand.MaybeWeapon;
            if (firstWeapon == null || secondWeapon == null)
            {
                PFLog.Default.Error("Caster not dual wielding");
                yield break;
            }
            UnitEntityData targetUnit = target.Unit;
            if (targetUnit == null)
            {
                PFLog.Default.Error("Can't be applied to point");
                yield break;
            }
            EventHandlers eventHandlers = new EventHandlers();
            RuleAttackWithWeapon rule = new RuleAttackWithWeapon(maybeCaster, targetUnit, firstWeapon, 0);
            RuleAttackWithWeapon rule2 = new RuleAttackWithWeapon(maybeCaster, targetUnit, secondWeapon, 0);
            using (eventHandlers.Activate())
            {
                context.TriggerRule(rule);
                context.TriggerRule(rule2);
            }
            yield return new AbilityDeliveryTarget(target);
            yield break;
        }
        public override void Cleanup(AbilityExecutionContext context)
        {
        }
    }
}
