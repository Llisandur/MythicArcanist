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
                MAContext.Logger.LogHeader("Loading New Content");

                MythicAbilities.AbundantPreparation.AddAbundantPreparation();
            }
        }
    }
}
