using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.UnitLogic;
using UnityEngine;

namespace MythicArcanist.NewComponents
{
    [AllowMultipleComponents]
    [AllowedOn(typeof(BlueprintFeature), false)]
    public class ReplaceSpellbook : UnitFactComponentDelegate
    {
        [SerializeField]
        public BlueprintSpellbookReference m_Spellbook;
        [SerializeField]
        public BlueprintSpellbookReference m_ReplaceSpellbook;
        [SerializeField]
        public BlueprintCharacterClassReference m_CharacterClass;
        private BlueprintCharacterClass CharacterClass => m_CharacterClass?.Get();
        public BlueprintSpellbook Spellbook => m_ReplaceSpellbook?.Get();

        public override void OnActivate()
        {
            if (m_Spellbook != null)
            {
                ClassData classData = base.Owner.Progression.GetClassData(CharacterClass);
                if (classData != null && m_ReplaceSpellbook != null && classData.Spellbook == m_ReplaceSpellbook?.Get())
                {
                    classData.Spellbook = m_Spellbook?.Get();
                }
            }
        }
        public override void OnDeactivate()
        {
            if (m_Spellbook != null)
            {
                ClassData classData = base.Owner.Progression.GetClassData(CharacterClass);
                if (classData != null)
                {
                    classData.CharacterClass.m_Spellbook = CharacterClass?.m_Spellbook;
                }
            }
        }
    }
}
