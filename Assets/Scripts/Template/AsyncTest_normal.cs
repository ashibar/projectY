using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class AsyncTest_normal : MonoBehaviour
{
    [SerializeField] private Vector2 target;
    [SerializeField] private float spd = 10f;
    [SerializeField] private int fps;

    private CancellationTokenSource cts = new CancellationTokenSource();

    private void Awake()
    {
        Application.targetFrameRate = fps;
        
        Move_routine();
    }

    private async void Move_routine()
    {
        Vector2 dir = (target - (Vector2)transform.position).normalized;
        while (!cts.Token.IsCancellationRequested && Vector2.Distance(transform.position, target) > 0.5f)
        {
            Debug.Log(string.Format("{0}, {1}", Time.deltaTime, Time.fixedDeltaTime));
            transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + dir, spd * Time.deltaTime);
            await Task.Yield();
        }
        
        Debug.Log(string.Format("Normal : {0}", Time.time));
    }

    private void OnDestroy()
    {
        cts?.Cancel();
    }
}
