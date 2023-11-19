using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Spell_Icon : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    [SerializeField] private Image shadow;
    [SerializeField] protected Image background;
    [SerializeField] protected Image image;

    [SerializeField] protected Spell spell_;
    [SerializeField] protected int id;

    [SerializeField] private GameObject explain_textbox;

    private void Awake()
    {
        explain_textbox = SpellExplainText.Instance.gameObject;
    }

    public void SetIcon(Spell spell, int id = 0)
    {
        if (background == null || image == null)
            return;
        else if (spell == null)
        {
            image.sprite = Resources.Load<Sprite>("transparent_sprite");
            background.color = Color.white;
            spell_ = null;
            return;
        }

        spell_ = spell;
        this.id = id;
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
        if (spell_ == null) return;
        explain_textbox.SetActive(true);
        explain_textbox.transform.position = eventData.position;
        if (spell_ != null)
        {
            explain_textbox.GetComponentInChildren<TextMeshProUGUI>().text = spell_.GetName();
        }
    }
}
