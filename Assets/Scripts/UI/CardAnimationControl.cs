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
        await Task.Yield();
    }
}
