using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Spell_Icon : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    [SerializeField] private Image shadow;
    [SerializeField] private Image background;
    [SerializeField] private Image image;

    [SerializeField] private Spell spell_;

    [SerializeField] private GameObject explain_textbox;

    private void Awake()
    {
        explain_textbox = SpellExplainText.Instance.gameObject;
    }

    public void SetIcon(Spell spell)
    {
        if (background == null || image == null || spell == null)
            return;

        spell_ = spell;
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

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        if (!eventData.fullyExited) return;
        explain_textbox.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        explain_textbox.SetActive(true);
        explain_textbox.transform.position = eventData.position;
        if (spell_ != null)
        {
            explain_textbox.GetComponentInChildren<TextMeshProUGUI>().text = spell_.GetName();
        }
    }
}
