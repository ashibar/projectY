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
        
    private CancellationTokenSource cts = new CancellationTokenSource();

    public override void Awake()
    {
        base.Awake();
    }

    protected override void Update()
    {
        
    }

    private bool isStackProcessing = false;
    private List<Collider2D> collider_stack = new List<Collider2D>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == target)
        {
            TriggerEnterRoutine(cts.Token, collision);
            collider_stack.Add(collision);
            if (!isStackProcessing)
            {
                isStackProcessing = true;
                StackProcess_routine(cts.Token, collision);
            }            
        }
    }

    private async void StackProcess_routine(CancellationToken cts_t, Collider2D collision)
    {
        bool isProcessEnd = false;

        while (!cts_t.IsCancellationRequested && !isProcessEnd)
        {
            DelegateParameter para = new DelegateParameter()
            {
                collider_stack = collider_stack,
                projectile = gameObject,
                stat_processed = stat_processed,
                stat_spell = stat_spell,
            };
            isProcessEnd = StackProcess_Function(para);
            await Task.Yield();
        }
        Debug.Log("process End");
    }

    private bool StackProcess_Function(DelegateParameter para)
    {
        if (para.collider_stack.Count <= 0) return false;

        for (int i = 0; i < Mathf.Min((int)(stat_spell.Spell_Amount_Tic), para.collider_stack.Count); i++)
        {
            para.collision = collider_stack[i];
            triggerEnterStackProcess(para);
            Destroy(para.projectile);
        }
        para.collider_stack.Clear();

        return true;
    }

    /// <summary>
    /// <b>�浹 �� ��ƾ</b>
    /// <b>triggerEnterTickFunction : �浹 �� ���ӽð����� �����Ӹ��� ����� �Լ�</b>
    /// <b>triggetEnterEndFunctio : ���ӽð��� ����Ǹ� ����� �Լ�</b>
    /// </summary>
    /// <param name="cts_t"></param>
    /// <param name="collision"></param>
    private async void TriggerEnterRoutine(CancellationToken cts_t, Collider2D collision)
    {
        float duration = 0;
        float end = Time.time + duration;
        
        while (Time.time < end && !cts_t.IsCancellationRequested)
        {
            DelegateParameter para1 = new DelegateParameter()
            {
                collision = collision,
                projectile = gameObject,
            };
            triggerEnterTickFunction(para1);
            await Task.Yield();
        }
        //Debug.Log("trigger enters");
        DelegateParameter para2 = new DelegateParameter()
        {
            collision = collision,
            projectile = gameObject,
            stat_processed = stat_processed,
            stat_spell = stat_spell,
        };
        triggerEnterEndFunction(para2);
    }

    /// <summary>
    /// <b>�߻�Ǵ� ���� ����� ��ƾ</b>
    /// <b>shootingFunction : �߻�Ǵ� ���ӽð����� �����Ӹ��� ����� �Լ�</b>
    /// <b>���ӽð��� ����Ǹ� �ı� ��ƾ�� ���� ��</b>
    /// </summary>
    /// <param name="cts_t"></param>
    private async void ShootingFunction_routine(CancellationToken cts_t)
    {
        float end = Time.time + stat_spell.Spell_Duration;

        while (Time.time < end && !cts_t.IsCancellationRequested)
        {
            //Debug.Log(string.Format("isShooting, {0}, {1}, {2}", dir_toShoot, Time.time, end));
            DelegateParameter para = new DelegateParameter()
            {
                cts_t = cts_t,
                projectile = gameObject,
                stat_processed = stat_processed,
                stat_spell = stat_spell,
                dir_toShoot = dir_toShoot,
                anim_module = main_element.AnimationModule
            };
            shootingFunction(para);
            await Task.Yield();
        }

        if (!cts_t.IsCancellationRequested)
            DestroyProcess_routine(cts_t, gameObject);
    }

    /// <summary>
    /// <b>�ı� �Ǵµ��� ����� ��ƾ</b>
    /// <b>destroyFunction : �ı� �Ǵ� ��ƾ�� ���ӽð����� �����Ӹ��� ����� �Լ�</b>
    /// </summary>
    /// <param name="cts_t"></param>
    /// <param name="projectile"></param>
    private async void DestroyProcess_routine(CancellationToken cts_t, GameObject projectile)
    {
        float duration = 0;
        float end = Time.time + duration; 
        
        while (Time.time < end && !cts_t.IsCancellationRequested)
        {
            DelegateParameter para = new DelegateParameter() { projectile = gameObject };
            destroyFunction(para);
            await Task.Yield();
        }

        //Debug.Log("isDestroy" + stat_spell.Spell_Duration.ToString());
        if (projectile != null)
            Destroy(projectile);
    }

    // �ܺ� ���� �Լ�

    /// <summary>
    /// �ʱ� �����Ŀ� ��ƾ �Լ��� Ȱ��ȭ�ϱ� ���� �Լ�
    /// ���� �������� ������ ��
    /// </summary>
    public void SetActive()
    {
        ShootingFunction_routine(cts.Token);
    }

    /// <summary>
    /// �нú�, ������ ���� ����� ���ݰ� ���� ��ü ���� ����
    /// </summary>
    /// <param name="stat_processed">������ ����</param>
    /// <param name="stat">���� ����</param>    
    public void SetStat(Stat stat_processed, Stat_Spell stat)
    {
        this.stat_processed = stat_processed; 
        stat_spell = new Stat_Spell(stat);
    }

    /// <summary>
    /// Ÿ�� �±׿� ���� ���� ����
    /// </summary>
    /// <param name="target">������ Ÿ�� �±�</param>
    /// <param name="dir_toMove">�߻� ��ü�� �̵� ���� ����</param>
    /// <param name="dir_toShoot">�߻� ���� ����</param>
    /// <param name="pos_toShoot">�߻� �� ���� ��ġ</param>
    public void SetVector(string target, Vector2 dir_toMove, Vector2 dir_toShoot, Vector2 pos_toShoot)
    {
        this.target = target;
        this.dir_toMove = dir_toMove;
        this.dir_toShoot = dir_toShoot;
        this.pos_toShoot = pos_toShoot;
    }

    /// <summary>
    /// ���� ��Ʈ ������ ���� �ҷ���
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
    /// Ư���� Spell_Element �ϳ��� �ҷ���
    /// </summary>
    /// <param name="spell"></param>
    public void RegisterSpellElement(Spell_Element spell)
    {
        spell_element.Add(spell);
    }

    /// <summary>
    /// �ֿ� ���� ���
    /// </summary>
    /// <param name="spell"></param>
    public void SetMainElement(Spell_Element spell)
    {
        //Debug.Log(spell);
        main_element = spell;
        shootingFunction += spell.ShootingFunction;
    }

    /// <summary>
    /// Ư���� Spell_Part �ϳ��� �ҷ���
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
