using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Spell_Icon_CoreDisplay : Spell_Icon, IDropHandler
{
    [SerializeField] private InventoryWindow window;
    
    public void OnDrop(PointerEventData eventData)
    {
        Spell spell = SpellExplainText.Instance.spell;
        if (spell != null)
        {
            SpellType type = spell.GetStatSpell().Spell_Type;

            switch (type)
            {
                case SpellType.Core:
                    spell_ = spell;
                    image.sprite = spell.sprite_spell;
                    background.color = Color.yellow;
                    window.spell_displayed = spell;
                    window.Update_Status();
                    Debug.Log("drop");
                    break;
                case SpellType.Part:
                case SpellType.Element:
                    if (spell_ == null) return;
                    string code = spell_.GetCode();
                    window.playerInfoContainer.AddSpellToPlayerInfo_detailed(new StringNString(spell.GetCode(), code));
                    window.playerInfoContainer.Spell_inventory.RemoveAt(SpellExplainText.Instance.id);
                    window.Update_Status();
                    break;
            }
            
            
        }
    }
}