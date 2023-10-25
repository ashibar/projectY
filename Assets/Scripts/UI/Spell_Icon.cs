using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spell_Icon : MonoBehaviour
{
    [SerializeField] private Image shadow;
    [SerializeField] private Image background;
    [SerializeField] private Image image;

    public void SetIcon(Spell spell)
    {
        if (background == null || image == null || spell == null)
            return;

        image.sprite = spell.sprite_spell;
        switch (spell.GetStatSpell().Spell_Type)
        {
            case SpellType.Core:
                background.color = Color.yellow; break;
            case SpellType.Part:
                background.color = Color.green; break;
            case SpellType.Element:
                background.color = new Color(0.64f, 0.45f, 0.86f); break;
            case SpellType.Passive:
                background.color = Color.gray; break;
        }
    }
}
