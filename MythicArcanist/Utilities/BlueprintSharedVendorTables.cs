using Kingmaker.Blueprints.Items;
using TabletopTweaks.Core.Utilities;

namespace MythicArcanist.Utilities
{
    public static class BlueprintSharedVendorTables
    {
        public static BlueprintSharedVendorTable Scrolls_DefendersHeartVendorTable => BlueprintTools.GetBlueprint<BlueprintSharedVendorTable>("79b995e5fc910f34ab9dfec3c6b16c8f"); //Chapter1
        public static BlueprintSharedVendorTable WarCamp_ScrollVendorClericTable => BlueprintTools.GetBlueprint<BlueprintSharedVendorTable>("cdd7aa16e900b9146bc6963ca53b8e71"); //Act_2_SwordOfValor
        public static BlueprintSharedVendorTable WarCamp_REVendorTableMagic => BlueprintTools.GetBlueprint<BlueprintSharedVendorTable>("f02cf582e915ae343aa489f11dba42aa"); //Act_2_SwordOfValor
        public static BlueprintSharedVendorTable RE_Chapter3VendorTableMagic => BlueprintTools.GetBlueprint<BlueprintSharedVendorTable>("e8e384f0e411fab42a69f16991cac161"); //Chapter3
        public static BlueprintSharedVendorTable Scroll_Chapter3VendorTable => BlueprintTools.GetBlueprint<BlueprintSharedVendorTable>("d33d4c7396fc1d74c9569bc38e887e86"); //Chapter3
        public static BlueprintSharedVendorTable Scroll_Chapter5VendorTable => BlueprintTools.GetBlueprint<BlueprintSharedVendorTable>("5b73c93dccd743668734070160dfb82f"); //Chapter5
    }
}
