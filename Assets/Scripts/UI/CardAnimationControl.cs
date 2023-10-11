using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class CardAnimationControl : AsyncFunction_template
{
    [SerializeField] List<SpellCard> spell_card = new List<SpellCard>();

    private void Awake()
    {
        spell_card.AddRange(GetComponentsInChildren<SpellCard>(true));
    }

    public async Task AppearAnimation(CancellationToken cts)
    {
        if (cts.IsCancellationRequested)
            return;

        foreach (SpellCard card in spell_card)
        {
            if (card != null)
                card.AppearSpell();
            await Wait(cts, 0.5f);
        }

        await Wait(cts, 1f);

        foreach (SpellCard card in spell_card)
        {
            card.RevealSpell();
        }

        await Wait(cts, 1f);

        await Task.Yield();
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
}
