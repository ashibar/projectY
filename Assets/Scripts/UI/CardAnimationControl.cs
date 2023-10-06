using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class CardAnimationControl : AsyncFunction_template
{
    [SerializeField] List<Animator> animators = new List<Animator>();

    private void Awake()
    {
        animators.AddRange(GetComponentsInChildren<Animator>(true));
    }

    public async Task AppearAnimation(CancellationToken cts)
    {
        if (cts.IsCancellationRequested)
            return;

        foreach (Animator anim in animators)
        {
            anim.gameObject.SetActive(true);
            anim.SetTrigger("isAppears");
            await Wait(cts, 0.5f);
        }
        await Task.Yield();
    }
}
