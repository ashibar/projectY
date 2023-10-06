using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class AsyncFunction_template : MonoBehaviour
{
    public async Task Wait(CancellationToken cts, float second)
    {
        float end = Time.time + second;
        while (Time.time < end)
        {
            if (cts.IsCancellationRequested) await Task.FromResult(-1);
            else await Task.Yield();
        }
    }
}
