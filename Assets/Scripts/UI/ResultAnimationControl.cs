using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class ResultAnimationControl : AsyncFunction_template
{
    public async Task Active(CancellationToken cts)
    {
        if (cts.IsCancellationRequested)
            return;
        else
            await Task.Yield();
    }
}
