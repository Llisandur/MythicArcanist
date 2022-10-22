using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.UnitLogic;
using UnityEngine;

namespace MythicArcanist.NewComponents
{
    [AllowMultipleComponents]
    [AllowedOn(typeof(BlueprintFeature), false)]
    public class ReplaceSpellList : UnitFactComponentDelegate
    {
        [SerializeField]
        public BlueprintSpellListReference m_SpellList;
        [SerializeField]
        public BlueprintSpellListReference m_ReplaceSpellList;
        [SerializeField]
        public BlueprintCharacterClassReference m_CharacterClass;
        private BlueprintCharacterClass CharacterClass => m_CharacterClass?.Get();
        
        public override void OnActivate()
        {
            if (m_SpellList != null)
            {
                ClassData classData = base.Owner.Progression.GetClassData(CharacterClass);
                if (classData != null && m_ReplaceSpellList != null && classData.Spellbook.m_SpellList == m_ReplaceSpellList)
                {
                    classData.Spellbook.m_SpellList = m_SpellList;
                }
            }
        }
        public override void OnDeactivate()
        {
            if (m_SpellList != null)
            {
                ClassData classData = base.Owner.Progression.GetClassData(CharacterClass);
                if (classData != null)
                {
                    classData.Spellbook.m_SpellList = CharacterClass?.Spellbook.m_SpellList;
                }
            }
        }
    }
}
