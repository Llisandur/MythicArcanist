using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.UnitLogic;
using MythicArcanist.NewEvents;

namespace MythicArcanist.NewComponents
{

    [AllowMultipleComponents]
    [AllowedOn(typeof(BlueprintFeature), false)]
    public class AddSpellSlotsMA : UnitFactComponentDelegate, IGetSpellSlotsCountHandler
    {
        public int Amount;
        public int[] Levels;
        public void HandleGetSlotsCount(Spellbook spellbook, int spellLevel, ref int __result)
        {
            if (spellbook.Blueprint.IsArcanist)
            {
                foreach (int num in this.Levels)
                {
                    if (spellLevel == num)
                    {
                        __result += Amount;
                    }
                }
            }
        }
    }
}
