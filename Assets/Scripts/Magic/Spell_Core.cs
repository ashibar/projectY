using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class Spell_Core : Spell
{
    [SerializeField] protected List<GameObject> spell_lower_obj = new List<GameObject>();
    [SerializeField] protected List<Spell> spell_lower = new List<Spell>();

    [SerializeField] protected Vector2 dir_toMove;
    [SerializeField] protected Vector2 dir_toShoot;
    [SerializeField] protected Vector2 pos_toShoot;
    
    // test
    private float cooltime;
    private List<GameObject> projectile_origin;
    private float projectile_amount;

    private CancellationTokenSource cts;
    private bool isCooltime;

    public override void Awake()
    {
        base.Awake();
        cts = new CancellationTokenSource();
        RegisterAllFromChildren();
    }

    protected override void Update()
    {
        base.Update();
        InstantiateDelayFunction();
    }


    private async void InstantiateDelayFunction()
    {
        if (isCooltime)
        {
            isCooltime = false;
            await InstantiateDelayFunction_routine(cooltime);
            isCooltime = true;
        }
    }

    private async Task InstantiateDelayFunction_routine(float duration)
    {
        float end = Time.time + duration;

        if (!cts.IsCancellationRequested)
        {
            InstantiateMainFunction(); // 
        }

        while (Time.time < end && !cts.IsCancellationRequested)
        {
            await Task.Yield();
        }        
    }

    private void InstantiateMainFunction()
    {
        for (int i = 0; i < (int)projectile_amount; i++)
        {
            float angle = Mathf.Atan2(dir_toShoot.y, dir_toShoot.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            GameObject clone = Instantiate(projectile_origin[0], pos_toShoot, rotation, Holder.projectile_holder);
            clone.GetComponent<Projectile>().SetVector(dir_toMove, dir_toShoot, pos_toShoot);
            foreach (GameObject lower in spell_lower_obj)
            {
                Instantiate(lower, clone.transform);

            }
            clone.GetComponent<Projectile>().Awake();
        }
    }

    // 외부 접근 함수

    public void RegisterAllFromChildren()
    {
        Spell[] temp = GetComponentsInChildren<Spell>();
        foreach (Spell spell in temp)
        {
            RegisterLowerSpell(spell);
        }
    }

    public void RegisterLowerSpell(Spell spell)
    {
        spell_lower.Add(spell);
        spell_lower_obj.Add(spell.gameObject);
    }

    public void SetVector(Vector2 dir_toMove, Vector2 dir_toShoot, Vector2 pos_toShoot)
    {
        this.dir_toMove = dir_toMove;
        this.dir_toMove = dir_toShoot;
        this.pos_toShoot = pos_toShoot;
    }

    private void OnDestroy()
    {
        cts?.Cancel();
    }
}
