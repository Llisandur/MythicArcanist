using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using TabletopTweaks.Core.Utilities;
using TabletopTweaks.Core.ModLogic;
using Kingmaker.ElementsSystem;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.Blueprints.Classes;

namespace MythicArcanist.Utilities
{
    public static class SpellToolsMA
    {
        public static void PatchArchmageArmor(ModContextBase modContext, BlueprintAbility Spell, BlueprintBuff Buff)
        {
            BlueprintFeature ArchmageArmor = BlueprintTools.GetBlueprint<BlueprintFeature>("c3ef5076c0feb3c4f90c229714e62cd0"); //ArchmageArmor

            ArchmageArmor.TemporaryContext(bp =>
            {
                //bp.SetComponents();
                bp.AddComponent<AddAbilityUseTrigger>(c =>
                {
                    c.AfterCast = true;
                    c.ForOneSpell = true;
                    c.m_Ability = Spell.ToReference<BlueprintAbilityReference>();
                    c.Type = Spell.Type;
                    c.UseCastRule = true;
                    c.Action = Helpers.CreateActionList
                    (
                        new Conditional()
                        {
                            ConditionsChecker = new ConditionsChecker()
                            { Conditions = new Condition[] { Helpers.Create<ContextConditionIsCaster>(cond => { cond.Not = false; }) } },
                            IfTrue = new ActionList()
                            {
                                Actions = new GameAction[]
                                {
                                        Helpers.Create<ContextActionApplyBuff>(ga =>
                                        {
                                            ga.m_Buff = Buff.ToReference<BlueprintBuffReference>();
                                            ga.SameDuration = true;
                                        })
                                }
                            },
                            IfFalse = Helpers.CreateActionList()
                        }
                    );
                });
            });
            modContext.Logger.Log($"Patched:{Spell.ToReference<BlueprintAbilityReference>()} - {Spell.name} added to {ArchmageArmor.ToReference<BlueprintFeatureReference>()} - {ArchmageArmor.name}");
        }
    }
}
