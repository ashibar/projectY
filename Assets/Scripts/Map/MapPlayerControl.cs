using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class MapPlayerControl : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float speed = 5;

    [SerializeField] public bool isActive;

    private CancellationTokenSource cts = new CancellationTokenSource();

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public async Task Move(MapNode target)
    {
        if (isActive) return;
        isActive = true;

        Vector3 pos = target.transform.position;

        animator.SetBool("isWalk", true);
        Vector3 s = transform.localScale;
        Vector2 dir = (pos - transform.position).normalized;
        transform.localScale = dir.x >= 0 ? new Vector3(1, s.y, s.z) : new Vector3(-1, s.y, s.z);

        while (!cts.IsCancellationRequested && isActive && Vector2.Distance(pos, transform.position) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + dir, Time.deltaTime * speed);
            await Task.Yield();
        }

        animator.SetBool("isWalk", false);
        isActive = false;
    }

    private void OnDestroy()
    {
        cts?.Cancel();
    }
}