using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class StageClearTextAnimation : AsyncFunction_template
{
[SerializeField] private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public async Task Active(CancellationToken cts)
    {
        if (cts.IsCancellationRequested)
            return;
        
        animator.SetTrigger("isAppears");
        await Task.Yield();
    }
}
