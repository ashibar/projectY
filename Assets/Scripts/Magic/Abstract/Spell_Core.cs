using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class Spell_Core : Spell
{
    [SerializeField] protected Stat stat_processed;
    [SerializeField] protected Stat stat_part_applied;

    [SerializeField] protected List<Spell_Element> spell_element = new List<Spell_Element>();
    [SerializeField] protected List<Spell_Part> spell_part = new List<Spell_Part>();

    [SerializeField] protected Vector2 dir_toMove;
    [SerializeField] protected Vector2 dir_toShoot = Vector2.right;
    [SerializeField] protected Vector2 pos_toShoot = Vector2.zero;

    [SerializeField] protected List<GameObject> projectile_origin;
    [SerializeField] protected Element_ManagementModule element_management;

    private CancellationTokenSource cts;
    [SerializeField] private bool isCooltime;

    public override void Awake()
    {
        base.Awake();
        cts = new CancellationTokenSource();
        element_management = GetComponentInChildren<Element_ManagementModule>();
        RegisterAllFromChildren();
        Stat_Process();
        element_management.Init();
    }

    protected override void Update()
    {
        base.Update();
        InstantiateDelayFunction();
    }

    // Spell_Core [����] ���� �Լ�
        
    /// <summary>
    /// ��Ÿ�� ���� ���� �ֱ� �Լ��� �����ϴ� �Լ�
    /// </summary>
    protected async void InstantiateDelayFunction()
    {
        if (!isCooltime)
        {
            isCooltime = true;
            await InstantiateDelayFunction_routine();
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
        while (Time.time < end && !cts.Token.IsCancellationRequested)
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
            float end = Time.time + stat_spell.Spell_ProjectileDelay;

            // ����ü �Ѱ� ����
            InstantiateOneProjectileFunction();

            // ���
            while (Time.time < end && !cts.Token.IsCancellationRequested)
            {
                FunctionWhileProjectileDelay();
                await Task.Yield();
            }
        }
    }

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
    private void InstantiateOneProjectileFunction()
    {
        // �߻� ����
        Quaternion rotation = SetAngle();

        // ����ü ����
        GameObject clone = InstantiateProjectile(rotation);

        // ����ü�� ���� ����, Ÿ�� �±� �ֱ�
        clone.GetComponent<Projectile>().SetStat(stat_part_applied, stat_spell);
        clone.GetComponent<Projectile>().SetVector(target, dir_toMove, dir_toShoot, pos_toShoot);

        // ����ü�� �ൿ ����
        clone.GetComponent<Projectile>().triggerEnterTickFunction += TriggerEnterTickFunction;
        clone.GetComponent<Projectile>().triggetEnterEndFunction += TriggerEnterEndFunction;
        clone.GetComponent<Projectile>().shootingFunction += ShootingFunction;
        clone.GetComponent<Projectile>().destroyFunction += DestroyFunction;

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

    // ����ü�� ������ �븮�� �Լ�

    public virtual void TriggerEnterTickFunction(Collider2D collision, GameObject projectile)
    {

    }

    public virtual void TriggerEnterEndFunction(Collider2D collision, GameObject projectile)
    {
        //if (collision.tag == "Enemy")
        //    Destroy(projectile);
    }

    public virtual void ShootingFunction(CancellationToken cts_t, GameObject projectile, Stat_Spell stat, Vector2 _dir_toShoot, Projectile_AnimationModule anim_module)
    {
        ////����
        //if (cts_t.IsCancellationRequested) return;
        //Debug.Log("shooting function");
        //Vector3 pos = projectile.transform.position;
        //projectile.transform.position = Vector3.MoveTowards(pos, pos + (Vector3)_dir_toShoot, stat.Spell_Speed * 5f * Time.deltaTime);
    }

    public virtual void DestroyFunction(GameObject projectile)
    {
        
    }

    // �ܺ� ���� �Լ�

    /// <summary>
    /// �нú� ��ġ�� ����� ������ ���� ����
    /// </summary>
    /// <param name="stat">�нú� ��ġ�� ����� ����</param>
    public void SetStat(Stat stat)
    {
        stat_processed = stat;
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
    }

    /// <summary>
    /// �Ű������� ���� ���� Spell_Part �ϳ��� �ҷ��� 
    /// </summary>
    /// <param name="spell">�ҷ��� ���� ����</param>
    public void RegisterSpellPart(Spell_Part spell)
    {
        spell_part.Add(spell);
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

    private void OnDestroy()
    {
        cts?.Cancel();
    }

}
