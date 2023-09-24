using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class Spell_Core : Spell
{
    [SerializeField] protected Stat stat_processed;
    
    [SerializeField] protected List<GameObject> spell_part_obj = new List<GameObject>();
    [SerializeField] protected List<Spell_Part> spell_part = new List<Spell_Part>();

    [SerializeField] protected Vector2 dir_toMove;
    [SerializeField] protected Vector2 dir_toShoot = Vector2.right;
    [SerializeField] protected Vector2 pos_toShoot = Vector2.zero;

    // test
    [SerializeField] protected List<GameObject> projectile_origin;

    private CancellationTokenSource cts;
    [SerializeField] private bool isCooltime;

    public override void Awake()
    {
        base.Awake();
        cts = new CancellationTokenSource();
        RegisterAllFromChildren();

        foreach (Spell_Part spell in spell_part)
        {
            
        }
    }

    private void Start()
    {


    }

    protected override void Update()
    {
        base.Update();
        InstantiateDelayFunction();
    }

    protected async void InstantiateDelayFunction()
    {
        if (!isCooltime)
        {
            isCooltime = true;
            await InstantiateDelayFunction_routine();
            isCooltime = false;
        }
    }

    protected async Task InstantiateDelayFunction_routine()
    {
        // 쿨타임
        float end = Time.time + stat_spell.Spell_CoolTime * (1-stat_processed.Cooldown);

        // 공격 한번의 주기 함수
        if (!cts.Token.IsCancellationRequested)
        {
            InstantiateMainFunction(); // 
        }

        // 대기
        while (Time.time < end && !cts.Token.IsCancellationRequested)
        {
            FunctionWhileCooltime();
            await Task.Yield();
        }
    }

    private async void InstantiateMainFunction()
    {
        for (int i = 0; i < (int)stat_processed.Amount + stat_spell.Spell_Multy_EA; i++)
        {
            // 투사체간 딜레이
            float end = Time.time + stat_spell.Spell_ProjectileDelay;

            // 투사체 한개 생성
            InstantiateOneProjectileFunction();

            // 대기
            while (Time.time < end && !cts.Token.IsCancellationRequested)
            {
                FunctionWhileProjectileDelay();
                await Task.Yield();
            }
        }
    }

    private void InstantiateOneProjectileFunction()
    {
        // 발사 각도
        Quaternion rotation = SetAngle();

        // 투사체 생성
        GameObject clone = InstantiateProjectile(rotation);

        // 투사체에 백터 정보 넣기
        clone.GetComponent<Projectile>().SetStat(stat_processed, stat_spell);
        clone.GetComponent<Projectile>().SetVector("Enemy", dir_toMove, dir_toShoot, pos_toShoot);

        // 투사체의 행동 결정
        clone.GetComponent<Projectile>().triggerEnterTickFunction += TriggerEnterTickFunction;
        clone.GetComponent<Projectile>().triggetEnterEndFunction += TriggerEnterEndFunction;
        clone.GetComponent<Projectile>().shootingFunction += ShootingFunction;
        clone.GetComponent<Projectile>().destroyFunction += DestroyFunction;

        // 클론에 하위 스펠 복제
        foreach (GameObject lower in spell_part_obj)
        {
            Instantiate(lower, clone.transform);
        }

        // 클론의 초기화 함수 활성화
        clone.GetComponent<Projectile>().SetActive();
    }

    protected virtual void FunctionWhileCooltime()
    {

    }

    protected virtual void FunctionWhileProjectileDelay()
    {

    }

    protected virtual Quaternion SetAngle()
    {
        float angle = Mathf.Atan2(dir_toShoot.y, dir_toShoot.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        return rotation;
    }

    protected virtual GameObject InstantiateProjectile(Quaternion rotation)
    {
        return Instantiate(projectile_origin[0], pos_toShoot, rotation, Holder.projectile_holder);
    }

    public virtual void TriggerEnterTickFunction(Collider2D collision, GameObject projectile)
    {

    }

    public virtual void TriggerEnterEndFunction(Collider2D collision, GameObject projectile)
    {
        //if (collision.tag == "Enemy")
        //    Destroy(projectile);
    }

    public virtual void ShootingFunction(CancellationToken cts_t, GameObject projectile, Stat_Spell stat, Vector2 _dir_toShoot)
    {
        ////샘플
        //if (cts_t.IsCancellationRequested) return;
        //Debug.Log("shooting function");
        //Vector3 pos = projectile.transform.position;
        //projectile.transform.position = Vector3.MoveTowards(pos, pos + (Vector3)_dir_toShoot, stat.Spell_Speed * 5f * Time.deltaTime);
    }

    public virtual void DestroyFunction(GameObject projectile)
    {
        
    }

    // 외부 접근 함수

    public void SetStat(Stat stat)
    {
        stat_processed = stat;
    }

    /// <summary>
    /// 하위 스펠을 전부 불러옴 
    /// </summary>
    public void RegisterAllFromChildren()
    {
        Spell_Part[] temp = GetComponentsInChildren<Spell_Part>();
        foreach (Spell_Part spell in temp)
        {
            RegisterLowerSpell(spell);
        }
    }

    /// <summary>
    /// 매개변수를 통해 하위 스펠 하나를 불러옴 
    /// </summary>
    /// <param name="spell">불러올 하위 스펠</param>
    public void RegisterLowerSpell(Spell_Part spell)
    {
        spell_part.Add(spell);
        spell_part_obj.Add(spell.gameObject);
    }

    /// <summary>
    /// 벡터 변수들을 설정
    /// </summary>
    /// <param name="dir_toMove">이동할 방향</param>
    /// <param name="dir_toShoot">쏠 방향</param>
    /// <param name="pos_toShoot">쏠 위치</param>
    public void SetVector(Vector2 dir_toMove, Vector2 dir_toShoot, Vector2 pos_toShoot)
    {
        this.dir_toMove = dir_toMove;
        this.dir_toShoot = dir_toShoot;
        this.pos_toShoot = pos_toShoot;
    }

    private void OnDestroy()
    {
        cts?.Cancel();
    }

    public void Test()
    {
        Debug.Log(string.Format("{0}: {1}", "Core", gameObject.name));
    }
}
