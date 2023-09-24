using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class Projectile : Spell
{
    [SerializeField] protected Stat stat_processed;
    
    [SerializeField] protected List<GameObject> spell_lower_obj = new List<GameObject>();
    [SerializeField] protected List<Spell> spell_lower = new List<Spell>();

    [SerializeField] protected Vector2 dir_toMove;
    [SerializeField] protected Vector2 dir_toShoot;
    [SerializeField] protected Vector2 pos_toShoot;

    public delegate void TriggerEnterTickFunction(Collider2D collision, GameObject projectile);
    public delegate void TriggerEnterEndFunction(Collider2D collision, GameObject projectile);
    public delegate void ShootingFunction(CancellationToken cts_t, GameObject projectile, Stat_Spell stat, Vector2 _dir_toShoot);
    public delegate void DestroyFunction(GameObject projectile);
    [SerializeField] public TriggerEnterTickFunction triggerEnterTickFunction;
    [SerializeField] public TriggerEnterEndFunction triggetEnterEndFunction;
    [SerializeField] public ShootingFunction shootingFunction;
    [SerializeField] public DestroyFunction destroyFunction;
    
    private CancellationTokenSource cts;

    public override void Awake()
    {
        base.Awake();
        cts = new CancellationTokenSource();
    }

    protected override void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == target)
            TriggerEnterRoutine(cts.Token, collision);
    }

    private async void TriggerEnterRoutine(CancellationToken cts_t, Collider2D collision)
    {
        float duration = 0;
        float end = Time.time + duration;

        while (Time.time < end && !cts_t.IsCancellationRequested)
        {
            triggerEnterTickFunction(collision, gameObject);
            await Task.Yield();
        }
        Debug.Log("trigger enters");
        triggetEnterEndFunction(collision, gameObject);
    }

    private async void ShootingFunction_routine(CancellationToken cts_t)
    {
        float end = Time.time + stat_spell.Spell_Duration;

        while (Time.time < end && !cts_t.IsCancellationRequested)
        {
            //Debug.Log(string.Format("isShooting, {0}, {1}, {2}", dir_toShoot, Time.time, end));
            shootingFunction(cts_t, gameObject, stat_spell, dir_toShoot);
            await Task.Yield();
        }

        if (!cts_t.IsCancellationRequested)
            DestroyProcess_routine(cts_t, gameObject);
    }
    
    private async void DestroyProcess_routine(CancellationToken cts_t, GameObject projectile)
    {
        float duration = 0;
        float end = Time.time + duration; 
        
        while (Time.time < end && !cts_t.IsCancellationRequested)
        {
            destroyFunction(gameObject);
            await Task.Yield();
        }

        Debug.Log("isDestroy" + stat_spell.Spell_Duration.ToString());
        if (projectile != null)
            Destroy(projectile);
    }

    // 외부 참조 함수

    public void SetActive()
    {
        ShootingFunction_routine(cts.Token);
    }

    public void SetStat(Stat stat_processed, Stat_Spell stat)
    {
        this.stat_processed = stat_processed; 
        stat_spell = new Stat_Spell(stat);
    }

    public void SetVector(string target, Vector2 dir_toMove, Vector2 dir_toShoot, Vector2 pos_toShoot)
    {
        this.target = target;
        this.dir_toMove = dir_toMove;
        this.dir_toShoot = dir_toShoot;
        this.pos_toShoot = pos_toShoot;
    }

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

    private void OnDestroy()
    {
        cts?.Cancel();
    }
}
