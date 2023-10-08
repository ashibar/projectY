using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class Projectile_AnimationModule : MonoBehaviour
{
    [SerializeField] private Projectile_Animation_so so;
    [SerializeField] private Sprite current_sprite;
    [SerializeField] private int index = 0;

    private CancellationTokenSource cts = new CancellationTokenSource();

    public async void SpriteChange_routine()
    {
        if (so.sprites.Count <= 0)
            return;

        while (!cts.Token.IsCancellationRequested)
        {
            if (index >= so.sprites.Count) return;
            current_sprite = so.sprites[index];
            index = index + 1 >= so.sprites.Count ? 0 : index + 1;
            await Task.Delay((int)(so.fixedMS / so.speed / so.sprites.Count));
        }
    }

    public Sprite GetSprite()
    {
        return current_sprite;
    }

    private void OnDestroy()
    {
        cts?.Cancel();
    }
}
