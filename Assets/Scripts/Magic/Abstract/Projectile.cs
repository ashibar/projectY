using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class Projectile : Spell
{
    [SerializeField] protected Stat stat_processed;

    [SerializeField] protected List<Spell_Element> spell_element = new List<Spell_Element>();
    [SerializeField] protected List<Spell_Part> spell_part = new List<Spell_Part>();

    [SerializeField] protected Vector2 dir_toMove;
    [SerializeField] protected Vector2 dir_toShoot;
    [SerializeField] protected Vector2 pos_toShoot;

    [SerializeField] protected Sprite current_sprite;

    [SerializeField] protected Spell_Element main_element;

    public delegate void TriggerEnterTickFunction(Collider2D collision, GameObject projectile);
    public delegate void TriggerEnterEndFunction(Collider2D collision, GameObject projectile, Stat stat_processed, Stat_Spell stat_spell);
    public delegate void ShootingFunction(CancellationToken cts_t, GameObject projectile, Stat stat_processed, Stat_Spell stat_spell, Vector2 _dir_toShoot, Projectile_AnimationModule anim_module);
    public delegate void DestroyFunction(GameObject projectile);
    [SerializeField] public TriggerEnterTickFunction triggerEnterTickFunction;
    [SerializeField] public TriggerEnterEndFunction triggetEnterEndFunction;
    [SerializeField] public ShootingFunction shootingFunction;
    [SerializeField] public DestroyFunction destroyFunction;
    
    private CancellationTokenSource cts = new CancellationTokenSource();

    public override void Awake()
    {
        base.Awake();
    }

    protected override void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == target)
            TriggerEnterRoutine(cts.Token, collision);
    }

    /// <summary>
    /// <b>충돌 시 루틴</b>
    /// <b>triggerEnterTickFunction : 충돌 후 지속시간동안 프레임마다 실행될 함수</b>
    /// <b>triggetEnterEndFunctio : 지속시간이 만료되면 실행될 함수</b>
    /// </summary>
    /// <param name="cts_t"></param>
    /// <param name="collision"></param>
    private async void TriggerEnterRoutine(CancellationToken cts_t, Collider2D collision)
    {
        float duration = 0;
        float end = Time.time + duration;

        while (Time.time < end && !cts_t.IsCancellationRequested)
        {
            triggerEnterTickFunction(collision, gameObject);
            await Task.Yield();
        }
        //Debug.Log("trigger enters");
        triggetEnterEndFunction(collision, gameObject, stat_processed, stat_spell);
    }

    /// <summary>
    /// <b>발사되는 동안 실행될 루틴</b>
    /// <b>shootingFunction : 발사되는 지속시간동안 프레임마다 실행될 함수</b>
    /// <b>지속시간이 만료되면 파괴 루틴이 실행 됨</b>
    /// </summary>
    /// <param name="cts_t"></param>
    private async void ShootingFunction_routine(CancellationToken cts_t)
    {
        float end = Time.time + stat_spell.Spell_Duration;

        while (Time.time < end && !cts_t.IsCancellationRequested)
        {
            //Debug.Log(string.Format("isShooting, {0}, {1}, {2}", dir_toShoot, Time.time, end));
            shootingFunction(cts_t, gameObject, stat_processed, stat_spell, dir_toShoot, main_element.AnimationModule);
            await Task.Yield();
        }

        if (!cts_t.IsCancellationRequested)
            DestroyProcess_routine(cts_t, gameObject);
    }

    /// <summary>
    /// <b>파괴 되는동안 실행될 루틴</b>
    /// <b>destroyFunction : 파괴 되는 루틴의 지속시간동안 프레임마다 실행될 함수</b>
    /// </summary>
    /// <param name="cts_t"></param>
    /// <param name="projectile"></param>
    private async void DestroyProcess_routine(CancellationToken cts_t, GameObject projectile)
    {
        float duration = 0;
        float end = Time.time + duration; 
        
        while (Time.time < end && !cts_t.IsCancellationRequested)
        {
            destroyFunction(gameObject);
            await Task.Yield();
        }

        //Debug.Log("isDestroy" + stat_spell.Spell_Duration.ToString());
        if (projectile != null)
            Destroy(projectile);
    }

    // 외부 참조 함수

    /// <summary>
    /// 초기 세팅후에 루틴 함수를 활성화하기 위한 함수
    /// 가장 마지막에 실행할 것
    /// </summary>
    public void SetActive()
    {
        ShootingFunction_routine(cts.Token);
    }

    /// <summary>
    /// 패시브, 파츠로 인해 변경된 스텟과 스펠 자체 스텟 설정
    /// </summary>
    /// <param name="stat_processed">소유자 스텟</param>
    /// <param name="stat">스펠 스텟</param>    
    public void SetStat(Stat stat_processed, Stat_Spell stat)
    {
        this.stat_processed = stat_processed; 
        stat_spell = new Stat_Spell(stat);
    }

    /// <summary>
    /// 타겟 태그와 벡터 정보 설정
    /// </summary>
    /// <param name="target">적대할 타겟 태그</param>
    /// <param name="dir_toMove">발사 주체의 이동 방향 벡터</param>
    /// <param name="dir_toShoot">발사 방향 벡터</param>
    /// <param name="pos_toShoot">발사 될 시작 위치</param>
    public void SetVector(string target, Vector2 dir_toMove, Vector2 dir_toShoot, Vector2 pos_toShoot)
    {
        this.target = target;
        this.dir_toMove = dir_toMove;
        this.dir_toShoot = dir_toShoot;
        this.pos_toShoot = pos_toShoot;
    }

    /// <summary>
    /// 하위 파트 스펠을 전부 불러옴
    /// </summary>
    public void RegisterAllFromChildren()
    {
        Spell_Element[] elements = GetComponentsInChildren<Spell_Element>();
        Spell_Part[] parts = GetComponentsInChildren<Spell_Part>();
        foreach (Spell_Element spell in elements)
            RegisterSpellElement(spell);
        foreach (Spell_Part spell in parts)
            RegisterSpellPart(spell);
    }

    /// <summary>
    /// 특정된 Spell_Element 하나를 불러옴
    /// </summary>
    /// <param name="spell"></param>
    public void RegisterSpellElement(Spell_Element spell)
    {
        spell_element.Add(spell);
    }

    /// <summary>
    /// 주요 원소 등록
    /// </summary>
    /// <param name="spell"></param>
    public void SetMainElement(Spell_Element spell)
    {
        main_element = spell;
        shootingFunction += spell.ShootingFunction;
    }

    /// <summary>
    /// 특정된 Spell_Part 하나를 불러옴
    /// </summary>
    /// <param name="spell"></param>
    public void RegisterSpellPart(Spell_Part spell)
    {
        spell_part.Add(spell);
    }

    private void OnDestroy()
    {
        cts?.Cancel();
    }
}
