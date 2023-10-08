using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpellCard : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Animator animator;
    [SerializeField] private Image spell_image;
    [SerializeField] private Image spell_shadow;
    [SerializeField] private Spell spell;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spell_shadow = GetComponentsInChildren<Image>()[0];
        spell_image = GetComponentsInChildren<Image>()[1];
    }

    // 클릭시 반응
    public void OnPointerClick(PointerEventData eventData)
    {
        
    }

    public void SetSpell(Spell spell)
    {
        this.spell = spell;

    }

    public void AppearSpell()
    {
        gameObject.SetActive(true);
        animator.SetTrigger("isAppears");
    }
}
