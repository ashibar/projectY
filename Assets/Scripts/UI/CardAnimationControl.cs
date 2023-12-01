using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class CardAnimationControl : AsyncFunction_template
{
    [SerializeField] public List<SpellCard> spell_card = new List<SpellCard>();

    private void Awake()
    {
        spell_card.AddRange(GetComponentsInChildren<SpellCard>(true));
        for (int i = 0; i < spell_card.Count; i++)
            spell_card[i].index = i;
    }

    public async Task AppearAnimation(CancellationToken cts, bool isScaled = true)
    {
        if (cts.IsCancellationRequested)
            return;
        //SetActive(true);

        foreach (SpellCard card in spell_card)
        {
            if (card != null)
            {
                card.GetComponent<CanvasGroup>().alpha = 1;
                card.AppearSpell();
            }
            await Wait(cts, 0.5f, isScaled);
        }

        await Wait(cts, 1f, isScaled);

        foreach (SpellCard card in spell_card)
        {
            card.RevealSpell();
        }
        await Task.Yield();
    }

    public async Task SpellSelected(CancellationToken cts, int id, bool isScaled = true)
    {
        if (cts.IsCancellationRequested)
            return;

        for (int i = 0; i < spell_card.Count; i++)
        {
            if (i == id)
                spell_card[i].SelectSpellAnimation();
            else
                spell_card[i].DeleteSpellAnimation();
        }

        await Wait(cts, 2f, isScaled);
    }

    public void SetSpell(List<Spell> list)
    {
        foreach (Spell s in list)
        {
            Debug.Log(s.GetName());
        }
        for (int i = 0; i < spell_card.Count; i++)
            spell_card[i].SetSpell(list[i]);
    }

    public void SetInteratable(bool value)
    {
        foreach(SpellCard card in spell_card)
        {
            card.isInteractable = value;
        }
    }

    public void SetActive(bool value)
    {
        foreach (SpellCard card in spell_card)
            card.gameObject.SetActive(value);
    }
}
