using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Utility;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.FactLogic;
using TabletopTweaks.Core.NewComponents;
using TabletopTweaks.Core.NewComponents.Prerequisites;
using TabletopTweaks.Core.Utilities;
using static MythicArcanist.Main;

namespace MythicArcanist.NewContent.ClassFeatures.LoremasterSecrets
{
    static class LoremasterPaladinSpellSecret
    {
        private static readonly BlueprintCharacterClass PaladinClass = BlueprintTools.GetBlueprint<BlueprintCharacterClass>("bfa11238e7ae3544bbeb4d0b92e897ec");
        private static readonly BlueprintFeatureSelection LoremasterClericSpellSecret = BlueprintTools.GetBlueprint<BlueprintFeatureSelection>("904ce918c85c9f947910340b956fb877");
        public static void Add()
        {
            var LoremasterSecretSelection = BlueprintTools.GetBlueprint<BlueprintFeatureSelection>("beeb25d7a7732e14f9986cdb79acecfc");

            var LoremasterPaladinSpellSecret = Helpers.CreateBlueprint<BlueprintFeatureSelection>(ThisModContext, "LoremasterPaladinSpellSecret", bp =>
            {
                bp.SetName(ThisModContext, "Paladin Spell");
                bp.SetDescription(ThisModContext, LoremasterClericSpellSecret.Description);
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
                bp.AddPrerequisite<PrerequisiteNoFeature>(c => {
                    c.m_Feature = bp.ToReference<BlueprintFeatureReference>();
                });
            });

            LoremasterPaladinSpellSecret.AddFeatures(CreateSpellSecretClasses());

            BlueprintFeature[] CreateSpellSecretClasses()
            {
                return SpellTools.SpellCastingClasses.AllClasses.Select(castingClass => {
                    var name = $"LoremasterPaladinSecret{castingClass.name.Replace("Class", "")}";
                    if (Regex.Matches(name, "Paladin").Count > 1)
                    {
                        return null;
                    }
                    var spellSecret = Helpers.CreateBlueprint<BlueprintFeature>(ThisModContext, name, bp => {
                        bp.SetName(ThisModContext, $"{LoremasterPaladinSpellSecret.Name} — {castingClass.Name}");
                        bp.SetDescription(LoremasterPaladinSpellSecret.m_Description);
                        bp.IsClassFeature = true;
                        bp.Groups = LoremasterClericSpellSecret.Groups;
                        bp.HideNotAvailibleInUI = true;
                        bp.AddComponent<AdditionalSpellSelection>(c => {
                            c.m_SpellCastingClass = castingClass.ToReference<BlueprintCharacterClassReference>();
                            c.m_SpellList = PaladinClass.Spellbook.m_SpellList;
                            c.UseOffset = true;
                            c.Count = 1;
                        });
                        bp.AddComponent<PrerequisiteClassSpellLevel>(c => {
                            c.m_CharacterClass = castingClass.ToReference<BlueprintCharacterClassReference>();
                            c.RequiredSpellLevel = 1;
                            c.HideInUI = true;
                        });
                    });
                    return spellSecret;
                }).Where(secret => secret != null).ToArray();
            }

            if (ThisModContext.Homebrew.Loremaster.IsDisabled("PaladinSpellSecret")) { return; }
            LoremasterSecretSelection.AddFeatures(LoremasterPaladinSpellSecret);
        }
    }
}
