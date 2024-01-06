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

    // Spell_Core [����] ���� �Լ�

    /// <summary>
    /// ��Ÿ�� ���� ���� �ֱ� �Լ��� �����ϴ� �Լ�
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
    /// <b>��Ÿ�Ӹ��� ����� �Լ�</b>
    /// <b>���� ��Ÿ�� �������� ���� ��ٿ� ������ ������ �޾� �ֱⰡ ª����</b>
    /// <b>InstantiateMainFunction() : ����ü ���� �� �ֱ⿡ ����� ��ƾ</b>
    /// <b>FunctionWhileCooltime() : ��Ÿ�� ���� �� �����Ӹ��� ����� �Լ�</b>
    /// </summary>
    /// <returns></returns>
    protected async Task InstantiateDelayFunction_routine()
    {
        // ��Ÿ��
        float end = Time.time + stat_spell.Spell_CoolTime * (1- stat_part_applied.Cooldown);

        // ���� �ѹ��� �ֱ� �Լ�
        if (!cts.Token.IsCancellationRequested)
        {
            InstantiateMainFunction(); // 
        }

        // ���
        while (Time.time < end && !cts.Token.IsCancellationRequested && !trigger)
        {
            FunctionWhileCooltime();
            await Task.Yield();
        }
    }

    /// <summary>
    /// <b>����ü ���� �� �ֱ⿡ ����� �Լ�</b>
    /// <b>���� ���ݰ� ���� ������ ����ü ������ �տ������� ��� ��</b>
    /// <b>InstantiateOneProjectileFunction() : �Ѱ��� ����ü�� �����ϴ� �Լ�</b>
    /// <b>FunctionWhileProjectileDelay() : ����ü���� ���� ���̿� �����Ӹ��� ����� �Լ�</b>
    /// </summary>
    private async void InstantiateMainFunction()
    {
        for (int i = 0; i < (int)stat_part_applied.Amount + stat_spell.Spell_Multy_EA; i++)
        {
            // ����ü�� ������
            float end = Time.time + stat_spell.Spell_ProjectileDelay * stat_part_applied.Duration_projectile;

            // ����ü �Ѱ� ����
            DelegateParameter para_proj = new DelegateParameter()
            {
                proj_cnt = i,
            };
            instantiateOneProjectileFunction(para_proj);
            //InstantiateOneProjectileFunction();
            //Debug.Log(stat_spell.Spell_ProjectileDelay * stat_processed.Duration_projectile);
            // ���
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

    // Spell_Core [���] ���� �Լ� (����ü ����)

    /// <summary>
    /// <b>�Ѱ��� ����ü�� �����ϴ� �Լ�</b>
    /// <b>1. SetAngle() : ���� ����</b>
    /// <b>2. InstantiateProjectile() : ����ü ������ ����</b>
    /// <b>3. SetStat(): ����ü�� ���� ���� ���� </b>
    /// <b>4. SetVector(): ����ü�� ���� ����, Ÿ�� �±� ���� </b>
    /// <b>5. �ൿ ���� �Լ� ����</b>
    /// <b>6. ���� ���� ���� ����</b>
    /// <b>7. ����ü �ʱ�ȭ �Լ� ����</b>
    /// </summary>
    public override void InstantiateOneProjectileFunction(DelegateParameter para)
    {
        // �߻� ����
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
        

        // ����ü ����
        DelegateParameter para_clone = new DelegateParameter()
        {
            projectile = projectile_origin[0],
            rotation = rotation,
            dir_toShoot = dir_toShoot,
        };
        GameObject clone = InstantiateProjectile(para_clone);

        // ����ü�� ���� ����, Ÿ�� �±� �ֱ�
        //Debug.Log("dmg = " + stat_part_applied.Damage);
        clone.GetComponent<Projectile>().SetStat(stat_part_applied, stat_spell);
        clone.GetComponent<Projectile>().SetVector(target, dir_toMove, dir_toShoot_processed, pos_toShoot);

        // ����ü�� �ൿ ����
        //AddDelegate(clone.GetComponent<Projectile>());
        SendDelegateToProjectile(clone.GetComponent<Projectile>());
        // ����ü�� ���� ���� ����
        foreach (Spell_Part lower in spell_part)
            Instantiate(lower.gameObject, clone.transform);
        foreach (Spell_Element element in spell_element)
            Instantiate(element.gameObject, clone.transform);

        // ����ü�� ���� ������ ��ũ��Ʈ�� ���
        clone.GetComponent<Projectile>().RegisterAllFromChildren();
        clone.GetComponent<Projectile>().SetMainElement(element_management.GetElementInfo());
        //clone.GetComponent<SpriteRenderer>().color = element_management.GetElementInfo().

        // ����ü�� �ʱ�ȭ �Լ� Ȱ��ȭ
        clone.GetComponent<Projectile>().SetActive();
    }

    // Spell_Core [���] ���� �Լ�
    // ��� �� �������̵� �Ͽ� ���� ��� ����

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

    // ����ü�� ������ �븮�� �Լ�

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
        ////����
        //if (cts_t.IsCancellationRequested) return;
        //Debug.Log("shooting function");
        //Vector3 pos = projectile.transform.position;
        //projectile.transform.position = Vector3.MoveTowards(pos, pos + (Vector3)_dir_toShoot, stat.Spell_Speed * 5f * Time.deltaTime);
    }

    public override void DestroyFunction(DelegateParameter para)
    {
        
    }

    // �ܺ� ���� �Լ�

    /// <summary>
    /// �����ƾ Ȱ��ȭ ����
    /// </summary>
    /// <param name="value"></param>
    public void SetActive(bool value)
    {
        isActive = value;
    }

    /// <summary>
    /// �нú� ��ġ�� ����� ������ ���� ����
    /// </summary>
    /// <param name="stat">�нú� ��ġ�� ����� ����</param>
    public void SetStat(Stat stat)
    {
        stat_processed = stat;
        Stat_Process();
    }

    /// <summary>
    /// ���� ������ ���� ���� ����
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
    /// ���� ������ ���� �ҷ��� 
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
    /// �Ű������� ���� ���� Spell_Element �ϳ��� �ҷ��� 
    /// </summary>
    /// <param name="spell">�ҷ��� ���� ����</param>
    public void RegisterSpellElement(Spell_Element spell) 
    {        
        spell_element.Add(spell);
        //spell.AddDelegate(this);
    }

    /// <summary>
    /// �Ű������� ���� ���� Spell_Part �ϳ��� �ҷ��� 
    /// </summary>
    /// <param name="spell">�ҷ��� ���� ����</param>
    public void RegisterSpellPart(Spell_Part spell)
    {
        spell_part.Add(spell);
        spell.AddDelegate(this);
    }

    /// <summary>
    /// ���� �������� ����
    /// </summary>
    /// <param name="dir_toMove">�̵��� ����</param>
    /// <param name="dir_toShoot">�� ����</param>
    /// <param name="pos_toShoot">�� ��ġ</param>
    public void SetVector(string target, Vector2 dir_toMove, Vector2 dir_toShoot, Vector2 pos_toShoot)
    {
        this.target = target;
        this.dir_toMove = dir_toMove;
        this.dir_toShoot = dir_toShoot;
        this.pos_toShoot = pos_toShoot;
    }

    /// <summary>
    /// �ڡڡ� �߿� �ڡڡ�
    /// �ھ� �ʱ�ȭ �Լ�
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
