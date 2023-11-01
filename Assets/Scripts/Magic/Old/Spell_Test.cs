using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class Spell_Test : MonoBehaviour
{
    [SerializeField] private float a = 0;
    [SerializeField] private float b = 0;

    private CancellationTokenSource cts = new CancellationTokenSource();

    private void Awake()
    {
        Routine();
    }

    private async void Routine()
    {
        while (!cts.IsCancellationRequested)
        {
            a = Time.time;
            b += Time.deltaTime;
            await Task.Yield();
        }
    }
}
