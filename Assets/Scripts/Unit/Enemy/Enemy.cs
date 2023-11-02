using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// ��ũ��Ʈ �̸� : Enemy
/// ����� : �̿��
/// ��� : �� ������Ʈ �ֻ��� Ŭ����
/// ��� : �÷��̾ �����ϴ� �Լ��� �ӽ÷� ������ ���̸�, ���Ŀ� ���� Ŭ������ �Ű� ������ ����
/// ������Ʈ ���� :
///     - (23.03.24) : ��ũ��Ʈ ����
///     - (23.03.25) : �Ҹ�� �������� ����Ʈ���� ������ �����ϴ� �Լ� ����
/// </summary>

public class Enemy : Unit
{
    private Movement movement;

    public Spawner spawner_pointer;
    public Action_AI action_ai;

    [SerializeField] protected EnemyMeleeAttack_Core meleeAttack;

    protected override void Awake()
    {
        base.Awake();
        
        if (GetComponent<Movement>() == null)
            gameObject.AddComponent<Movement>();
        movement = GetComponent<Movement>();

        action_ai = GetComponent<Action_AI>();
        meleeAttack = GetComponentInChildren<EnemyMeleeAttack_Core>();
        meleeAttack.SetUnits(this, "Player");
    }

    protected override void Start()
    {
        if (buffManager == null)
        {
            GameObject obj = new GameObject("BuffManager");
            obj.transform.parent = transform;
            buffManager = obj.AddComponent<BuffManager>();            
        }
        if (spellManager != null)
        {
            spellManager.SetSpell();
        }
    }

    protected override void Update()
    {
        base.Update();

        //movement.MoveByDirection_transform(new Vector2(-1, -1), stat.Speed);
        //movement.MoveToPosition_transform(Player.instance.transform.position, stat.Speed);
        //action_ai.ai_process();
        if (stat.Hp_current <= 0)
        {
            Delete_FromCloneList();

        }
            
    }

    // �����ʿ� ����� ������ ������ ����
    // SpellProjectile Delete_FromCloneLIst()����
    public void Delete_FromCloneList()
    {
        UnitManager.Instance.Delete_FromCloneList(gameObject);
        Destroy(gameObject);
    }

    public virtual void Applier(GameObject obj, Stat stat)
    {

    }

    //[SerializeField] private bool isCooltime;
    //[SerializeField] private bool isInterrupted;
    //[SerializeField] private float damage = 10f;
    //[SerializeField] private float cooltime = 3f;
    protected void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            meleeAttack.MeleeDamange(collision);
    }
    //private async void MeleeDamange(Collision2D collision)
    //{
    //    //Debug.Log("");
    //    if (!isCooltime)
    //    {
    //        isCooltime = true;
    //        await MeleeDamage_Task(collision, cooltime);
    //        isCooltime = false;
    //    }
    //}
    //private async Task MeleeDamage_Task(Collision2D collision, float duration)
    //{
    //    float end = Time.time + duration;
    //    collision.gameObject.GetComponent<Unit>().stat.Hp_current -= stat.Damage * stat.Cooldown;
    //    while (Time.time < end)
    //    {
    //        if (isInterrupted)
    //            await Task.FromResult(0);
    //        await Task.Yield();
    //    }
    //} 
}
