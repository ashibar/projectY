using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpellCard : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Animator animator;
    [SerializeField] private Image spell_backImage;
    [SerializeField] private Image spell_frontImage;
    [SerializeField] private Image spell_frontSpellImage;
    [SerializeField] private Image spell_shadow;
    [SerializeField] private TextMeshProUGUI spell_name;
    [SerializeField] private Spell spell;

    [SerializeField] private Sprite sprite_back_default;
    [SerializeField] private Sprite sprite_front_default;
    [SerializeField] private Sprite sprite_spell_default;

    public ResultWindow resultWindow;
    public int index;
    public bool isInteractable;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spell_shadow = GetComponentsInChildren<Image>()[0];
        spell_backImage = GetComponentsInChildren<Image>()[1];
    }

    // 클릭시 반응
    public void OnPointerClick(PointerEventData eventData)
    {
        if (isInteractable)
        {
            StageManager.Instance.playerInfoContainer_so.AddSpellToPlayerInfo(new StringNString(spell.GetCode()));
            resultWindow.SpellSelected(index); 
        }
    }

    public void SetSpell(Spell spell)
    {
        this.spell = spell;
        if (spell != null)
        {
            spell_backImage.sprite = spell.sprite_back != null ? spell.sprite_back : sprite_back_default;
            spell_frontImage.sprite = spell.sprite_front != null ? spell.sprite_back : sprite_front_default;
            spell_frontSpellImage.sprite = spell.sprite_spell != null ? spell.sprite_spell : sprite_spell_default;
            spell_name.text = spell.GetName();
        }
    }

    public void AppearSpell()
    {
        gameObject.SetActive(true);
        animator.SetTrigger("isAppears");
    }

    public void RevealSpell()
    {
        animator.SetTrigger("isReveals");
    }

    public void SelectSpellAnimation()
    {
        animator.SetTrigger("isSelected");
    }

    public void DeleteSpellAnimation()
    {
        animator.SetTrigger("isDeleted");
    }
}
