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
            triggerEnterTickFunction(collision, gameObject);
            await Task.Yield();
        }
        //Debug.Log("trigger enters");
        triggetEnterEndFunction(collision, gameObject, stat_processed, stat_spell);
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
            shootingFunction(cts_t, gameObject, stat_processed, stat_spell, dir_toShoot, main_element.AnimationModule);
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
            destroyFunction(gameObject);
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
