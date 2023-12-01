using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpellCard : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Animator animator;
    [SerializeField] private Image spell_backBackground;
    [SerializeField] private Image spell_backImage;
    [SerializeField] private Image spell_frontImage;
    [SerializeField] private Image spell_frontSpellImage;
    [SerializeField] private Image spell_shadow;
    [SerializeField] private TextMeshProUGUI spell_name;
    [SerializeField] private Spell spell;

    [SerializeField] private Sprite sprite_backdesign_default;
    [SerializeField] private Sprite sprite_background_default;
    [SerializeField] private Sprite sprite_front_default;
    [SerializeField] private Sprite sprite_spell_default;

    public ResultWindow resultWindow;
    public int index;
    public bool isInteractable;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // 클릭시 반응
    public void OnPointerClick(PointerEventData eventData)
    {
        if (isInteractable)
        {
            StageManager.Instance.playerInfoContainer_so.AddSpellToPlayerInfo(new StringNString(spell.GetCode()));
            GameObject spell_prefab = LoadDataSingleton.Instance.SpellPrefabContainer().Search(spell.GetCode());
            if (spell.GetCode()[0] == 'd')
                Instantiate(spell_prefab, Player.Instance.spellManager.transform);
            Player.Instance.spellManager.SetSpell();
            resultWindow.SpellSelected(index); 
        }
    }

    public void SetSpell(Spell spell)
    {
        this.spell = spell;
        if (spell != null)
        {
            spell_backBackground.color = SetColor(spell);
            spell_backImage.sprite = spell.sprite_back != null ? spell.sprite_back : sprite_backdesign_default;
            spell_frontImage.color = SetColor(spell);
            spell_frontSpellImage.sprite = spell.sprite_spell != null ? spell.sprite_spell : sprite_spell_default;
            spell_name.text = spell.GetName();
        }
    }

    private Color SetColor(Spell spell)
    {
        if (spell is Spell_Core)
            return new Color(0.8f, 0.7f, 0f);/*- new Color(0.2f, 0.2f, 0)*/
        else if (spell is Spell_Part)
            return new Color(0f, 0.7f, 0f);/* - new Color(0, 0.2f, 0)*/
        else if (spell is Spell_Element)
            return new Color(0.44f, 0.25f, 0.66f); /*- new Color(0.2f, 0.2f, 0.2f)*/
        else if (spell is Spell_Passive)
            return new Color(0.3f, 0.3f, 0.3f); /*- new Color(0.2f, 0.2f, 0.2f)*/
        else
            return new Color(0.3f, 0.3f, 0.3f); /*- new Color(0.2f, 0.2f, 0.2f)*/
    }

    public void AppearSpell()
    {
        gameObject.SetActive(true);
        animator.SetTrigger("isAppears");
    }

    public Spell GetSpell()
    {
        return spell;
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

    public void ResetSpellAnimation()
    {
        animator.SetTrigger("isReset");
    }
}
