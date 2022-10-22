using HarmonyLib;
using Kingmaker.Blueprints.JsonSystem;
using static MythicArcanist.Main;

namespace MythicArcanist.NewContent
{
    class ContentAdder
    {
        [HarmonyPatch(typeof(BlueprintsCache), "Init")]
        static class BlueprintsCache_Init_Patch
        {
            static bool Initialized;

            [HarmonyPriority(799)]
            static void Postfix()
            {
                if (Initialized) return;
                Initialized = true;
                ThisModContext.Logger.LogHeader("Loading New Content");

                MythicAbilities.AbundantPreparation.Add();
                MythicAbilities.AbundantPreparationImproved.Add();
                MythicAbilities.AbundantPreparationGreater.Add();
                Spells.ForceArmor.Add();
                Spells.MageArmor2.Add();
                Spells.MageArmor3.Add();
                Spells.MageArmor4.Add();
                Spells.MageShield2.Add();
                Spells.MagicMissileGreater.Add();
                Spells.MagicMissileMastered.Add();
                ClassFeatures.ArcanistExploits.MartialTraining.Add();
                ClassFeatures.ArcanistExploits.MartialTrainingGreater.Add();
                ClassFeatures.ArcanistExploits.ReligiousFocus.Add();
                ClassFeatures.LoremasterSecrets.LoremasterPaladinSpellSecret.Add();
                Archetypes.Fighter.TwoWeaponWarrior.Add();
            }
        }
    }
}
