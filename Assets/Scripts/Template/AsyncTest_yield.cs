using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class AsyncTest_yield : MonoBehaviour
{
    [SerializeField] private Vector2 target;
    [SerializeField] private float spd = 10f;
    [SerializeField] private float sync;

    private CancellationTokenSource cts = new CancellationTokenSource();

    private void Awake()
    {
        Move_routine();
    }

    private async void Move_routine()
    {
        Vector2 dir = (target - (Vector2)transform.position).normalized;
        
        while (!cts.Token.IsCancellationRequested && Vector2.Distance(transform.position, target) > 0.5f)
        {
            //Debug.Log(string.Format("FPS : {0}, sync : {1}", (1 / Time.unscaledDeltaTime), sync));
            //float start = Time.time;
            transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + dir, spd * Time.fixedDeltaTime);
            await Task.Yield();
            //sync = Time.fixedDeltaTime *   * (1 + (Time.time - start) * Time.fixedDeltaTime) / 1.5f;
        }

        Debug.Log(string.Format("Yield : {0}", Time.time));
    }

    private void OnDestroy()
    {
        cts?.Cancel();
    }
}
