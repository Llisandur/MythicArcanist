using System;
using Kingmaker;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Facts;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.EntitySystem;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.QA;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;

namespace MythicArcanist.NewComponents
{
    public class AEMatrtialTraining : UnitFactComponentDelegate<AEMatrtialTraining>
    {
        private StatType Stat = StatType.BaseAttackBonus;
        public int Value;
        //public ModifierDescriptor Descriptor;
        public override void OnTurnOn()
        {
            int level = base.Owner.Progression.CharacterLevel;
            ModifiableValue baseAB = base.Owner.Stats.GetStat(Stat);
            int bab = base.Owner.Stats.GetStat(StatType.BaseAttackBonus).BaseValue;
            int num1 = Value;
            int num2 = bab + num1;

            if (num2 > level) { num2 = level; }
            int num3 = num2 - bab;
            baseAB.AddModifier(num3, base.Runtime, ModifierDescriptor.None);
        }
        public override void OnTurnOff()
        {
            base.Owner.Stats.GetStat(this.Stat).RemoveModifiersFrom(base.Runtime);
        }
    }
}
