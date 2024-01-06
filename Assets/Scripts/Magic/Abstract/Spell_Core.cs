using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class Spell_Core : Spell
{
    [SerializeField] protected KeyCode triggerKey = KeyCode.Mouse0;
    [SerializeField] public bool trigger;

    [SerializeField] protected Stat stat_processed;
    [SerializeField] protected Stat stat_part_applied;

    [SerializeField] protected List<Spell_Element> spell_element = new List<Spell_Element>();
    [SerializeField] protected List<Spell_Part> spell_part = new List<Spell_Part>();

    [SerializeField] protected Vector2 dir_toMove;
    [SerializeField] protected Vector2 dir_toShoot = Vector2.right;
    [SerializeField] protected Vector2 pos_toShoot = Vector2.zero;

    [SerializeField] protected List<GameObject> projectile_origin;
    [SerializeField] protected Element_ManagementModule element_management;
    [SerializeField] protected DamageTextRenderModule damageTextRenderModule;

    protected CancellationTokenSource cts;
    [SerializeField] protected bool isCooltime;
    [SerializeField] protected bool isActive = true;

    public override void Awake()
    {
        base.Awake();
        cts = new CancellationTokenSource();
        element_management = GetComponentInChildren<Element_ManagementModule>();
        damageTextRenderModule = GetComponentInChildren<DamageTextRenderModule>();
        //RegisterAllFromChildren();
        //Stat_Process();
        //if (element_management != null) element_management.Init();
    }

    protected override void Update()
    {
        base.Update();
        if (!isActive) return;
        InstantiateDelayFunction();
        //Debug.Log(element_management.GetElementInfo());
        //List<Spell_Element> list = element_management.GetElementList();
        //for (int i = 0; i < list.Count; i++) Debug.Log(string.Format("{0}, {1}", i, list[i]));
    }

    // Spell_Core [고정] 내부 함수

    /// <summary>
    /// 쿨타임 마다 공격 주기 함수를 실행하는 함수
    /// </summary>
    protected virtual async void InstantiateDelayFunction()
    {
        if (!isCooltime && ((Input.GetKey(triggerKey) && (owner is Player)) || trigger))
        {
            isCooltime = true;
            await InstantiateDelayFunction_routine();
            trigger = false;
            isCooltime = false;
        }
    }

    /// <summary>
    /// <b>쿨타임마다 실행될 함수</b>
    /// <b>스펠 쿨타임 기준으로 스텟 쿨다운 스텟의 영향을 받아 주기가 짧아짐</b>
    /// <b>InstantiateMainFunction() : 투사체 생성 한 주기에 실행될 루틴</b>
    /// <b>FunctionWhileCooltime() : 쿨타임 동안 한 프레임마다 실행될 함수</b>
    /// </summary>
    /// <returns></returns>
    protected async Task InstantiateDelayFunction_routine()
    {
        // 쿨타임
        float end = Time.time + stat_spell.Spell_CoolTime * (1- stat_part_applied.Cooldown);

        // 공격 한번의 주기 함수
        if (!cts.Token.IsCancellationRequested)
        {
            InstantiateMainFunction(); // 
        }

        // 대기
        while (Time.time < end && !cts.Token.IsCancellationRequested && !trigger)
        {
            FunctionWhileCooltime();
            await Task.Yield();
        }
    }

    /// <summary>
    /// <b>투사체 생성 한 주기에 실행될 함수</b>
    /// <b>스펠 스텟과 유닛 스텟의 투사체 갯수는 합연산으로 계산 됨</b>
    /// <b>InstantiateOneProjectileFunction() : 한개의 투사체를 생성하는 함수</b>
    /// <b>FunctionWhileProjectileDelay() : 투사체간의 간격 사이에 프레임마다 실행될 함수</b>
    /// </summary>
    private async void InstantiateMainFunction()
    {
        for (int i = 0; i < (int)stat_part_applied.Amount + stat_spell.Spell_Multy_EA; i++)
        {
            // 투사체간 딜레이
            float end = Time.time + stat_spell.Spell_ProjectileDelay * stat_part_applied.Duration_projectile;

            // 투사체 한개 생성
            DelegateParameter para_proj = new DelegateParameter()
            {
                proj_cnt = i,
            };
            instantiateOneProjectileFunction(para_proj);
            //InstantiateOneProjectileFunction();
            //Debug.Log(stat_spell.Spell_ProjectileDelay * stat_processed.Duration_projectile);
            // 대기
            if (stat_spell.Spell_ProjectileDelay * stat_part_applied.Duration_projectile == 0)
            {
                
                FunctionWhileProjectileDelay();
            }
            else
            {
                while (Time.time < end && !cts.Token.IsCancellationRequested)
                {
                    FunctionWhileProjectileDelay();
                    await Task.Yield();
                }
            }            
        }
    }

    // Spell_Core [상속] 내부 함수 (투사체 생성)

    /// <summary>
    /// <b>한개의 투사체를 생성하는 함수</b>
    /// <b>1. SetAngle() : 각도 설정</b>
    /// <b>2. InstantiateProjectile() : 투사체 복제본 생성</b>
    /// <b>3. SetStat(): 투사체에 스텟 정보 설정 </b>
    /// <b>4. SetVector(): 투사체에 벡터 정보, 타겟 태그 설정 </b>
    /// <b>5. 행동 결정 함수 설정</b>
    /// <b>6. 하위 스펠 파츠 복제</b>
    /// <b>7. 투사체 초기화 함수 실행</b>
    /// </summary>
    public override void InstantiateOneProjectileFunction(DelegateParameter para)
    {
        // 발사 각도
        DelegateParameter para_angle = new DelegateParameter()
        {
            projectile = projectile_origin[0],
            dir_toShoot = dir_toShoot,
            stat_processed = stat_part_applied,
            proj_cnt = para.proj_cnt,
        };
        setAngle(para_angle);
        Quaternion rotation = para_angle.rotation;
        float angle = rotation.eulerAngles.z * Mathf.Deg2Rad;
        Vector2 dir_toShoot_processed = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        

        // 투사체 생성
        DelegateParameter para_clone = new DelegateParameter()
        {
            projectile = projectile_origin[0],
            rotation = rotation,
            dir_toShoot = dir_toShoot,
        };
        GameObject clone = InstantiateProjectile(para_clone);

        // 투사체에 백터 정보, 타겟 태그 넣기
        //Debug.Log("dmg = " + stat_part_applied.Damage);
        clone.GetComponent<Projectile>().SetStat(stat_part_applied, stat_spell);
        clone.GetComponent<Projectile>().SetVector(target, dir_toMove, dir_toShoot_processed, pos_toShoot);

        // 투사체의 행동 결정
        //AddDelegate(clone.GetComponent<Projectile>());
        SendDelegateToProjectile(clone.GetComponent<Projectile>());
        // 투사체에 하위 스펠 복제
        foreach (Spell_Part lower in spell_part)
            Instantiate(lower.gameObject, clone.transform);
        foreach (Spell_Element element in spell_element)
            Instantiate(element.gameObject, clone.transform);

        // 투사체의 하위 스펠을 스크립트에 등록
        clone.GetComponent<Projectile>().RegisterAllFromChildren();
        clone.GetComponent<Projectile>().SetMainElement(element_management.GetElementInfo());
        //clone.GetComponent<SpriteRenderer>().color = element_management.GetElementInfo().

        // 투사체의 초기화 함수 활성화
        clone.GetComponent<Projectile>().SetActive();
    }

    // Spell_Core [상속] 내부 함수
    // 상속 시 오버라이딩 하여 고유 기능 개발

    protected override void FunctionWhileCooltime()
    {

    }

    protected override void FunctionWhileProjectileDelay()
    {

    }

    public override void SetAngle(DelegateParameter para)
    {
        float angle = Mathf.Atan2(dir_toShoot.y, dir_toShoot.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        para.rotation = rotation;
    }

    protected override GameObject InstantiateProjectile(DelegateParameter para)
    {
        return Instantiate(para.projectile, pos_toShoot, para.rotation, Holder.projectile_holder);
    }

    // 투사체에 전달할 대리자 함수

    public override void TriggerEnterTickFunction(DelegateParameter para)
    {

    }

    public override void TriggerEnterEndFunction(DelegateParameter para)
    {
        //if (collision.tag == "Enemy")
        //    Destroy(projectile);
    }

    public override void TriggerEnterStackProcess(DelegateParameter para)
    {
        
    }

    public override void ShootingFunction(DelegateParameter para)
    {
        ////샘플
        //if (cts_t.IsCancellationRequested) return;
        //Debug.Log("shooting function");
        //Vector3 pos = projectile.transform.position;
        //projectile.transform.position = Vector3.MoveTowards(pos, pos + (Vector3)_dir_toShoot, stat.Spell_Speed * 5f * Time.deltaTime);
    }

    public override void DestroyFunction(DelegateParameter para)
    {
        
    }

    // 외부 접근 함수

    /// <summary>
    /// 서브루틴 활성화 여부
    /// </summary>
    /// <param name="value"></param>
    public void SetActive(bool value)
    {
        isActive = value;
    }

    /// <summary>
    /// 패시브 수치가 적용된 유닛의 스텟 설정
    /// </summary>
    /// <param name="stat">패시브 수치가 적용된 스텟</param>
    public void SetStat(Stat stat)
    {
        stat_processed = stat;
        Stat_Process();
    }

    /// <summary>
    /// 스텟 파츠의 스텟 변동 적용
    /// </summary>
    public void Stat_Process()
    {
        Stat stat_part_applied = new Stat(stat_processed);
        foreach (Spell_Part p in spell_part)
            if (p.additional_stat != null)
                stat_part_applied += p.additional_stat;

        this.stat_part_applied = stat_part_applied;
    }

    /// <summary>
    /// 하위 스펠을 전부 불러옴 
    /// </summary>
    public void RegisterAllFromChildren()
    {
        Spell_Element[] elements = GetComponentsInChildren<Spell_Element>();
        Spell_Part[] parts = GetComponentsInChildren<Spell_Part>();
        spell_element.Clear();
        spell_part.Clear();
        foreach (Spell_Element spell in elements)
            RegisterSpellElement(spell);
        foreach (Spell_Part spell in parts)
            RegisterSpellPart(spell);
    }

    /// <summary>
    /// 매개변수를 통해 하위 Spell_Element 하나를 불러옴 
    /// </summary>
    /// <param name="spell">불러올 하위 스펠</param>
    public void RegisterSpellElement(Spell_Element spell) 
    {        
        spell_element.Add(spell);
        //spell.AddDelegate(this);
    }

    /// <summary>
    /// 매개변수를 통해 하위 Spell_Part 하나를 불러옴 
    /// </summary>
    /// <param name="spell">불러올 하위 스펠</param>
    public void RegisterSpellPart(Spell_Part spell)
    {
        spell_part.Add(spell);
        spell.AddDelegate(this);
    }

    /// <summary>
    /// 벡터 변수들을 설정
    /// </summary>
    /// <param name="dir_toMove">이동할 방향</param>
    /// <param name="dir_toShoot">쏠 방향</param>
    /// <param name="pos_toShoot">쏠 위치</param>
    public void SetVector(string target, Vector2 dir_toMove, Vector2 dir_toShoot, Vector2 pos_toShoot)
    {
        this.target = target;
        this.dir_toMove = dir_toMove;
        this.dir_toShoot = dir_toShoot;
        this.pos_toShoot = pos_toShoot;
    }

    /// <summary>
    /// ★★★ 중요 ★★★
    /// 코어 초기화 함수
    /// </summary>
    public void InitCore()
    {
        ResetDelegate(this);
        AddDelegate(this);
        RegisterAllFromChildren();
        Stat_Process();
        if (element_management != null)
        {
            element_management.Init();
            Spell_Element main_element = element_management.GetElementInfo();
            if (main_element != null)
                main_element.AddDelegate(this);
        }
    }

    private void OnDestroy()
    {
        cts?.Cancel();
    }

    private void SendDelegateToProjectile(Projectile target)
    {
        target.instantiateOneProjectileFunction += instantiateOneProjectileFunction;
        target.functionWhileCooltime += functionWhileCooltime;
        target.functionWhileProjectileDelay += functionWhileProjectileDelay;
        target.setAngle += setAngle;
        target.instantiateProjectile += instantiateProjectile;
        target.triggerEnterTickFunction += triggerEnterTickFunction;
        target.triggerEnterEndFunction += triggerEnterEndFunction;
        target.triggerEnterStackProcess += triggerEnterStackProcess;
        target.shootingFunction += shootingFunction;
        target.destroyFunction += destroyFunction;
    }
}
