using HarmonyLib;
using Kingmaker.UnitLogic;
using Kingmaker.PubSubSystem;

namespace MythicArcanist.NewEvents
{
    public interface IGetSpellSlotsCountHandler : IUnitSubscriber //Designed by Vek17, thanks Vek
    {
        void HandleGetSlotsCount(Spellbook spellbook, int spellLevel, ref int __result);

        [HarmonyPatch(typeof(Spellbook), nameof(Spellbook.GetSpellSlotsCount))]
        static class Spellbook_GetCommandType_IGetSpellSlotsCountHandler_Patch
        {
            static void Postfix(ref int __result, Spellbook __instance, int spellLevel)
            {
                var result = __result;
                EventBus.RaiseEvent<IGetSpellSlotsCountHandler>(__instance.Owner, h => h.HandleGetSlotsCount(__instance, spellLevel, ref result));
                __result = result;
            }
        }
    }
}
