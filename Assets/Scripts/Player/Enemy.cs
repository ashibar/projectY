using System.Collections;
using System.Collections.Generic;
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
    protected override void Awake()
    {
        base.Awake();
        
        if (GetComponent<Movement>() == null)
            gameObject.AddComponent<Movement>();
        movement = GetComponent<Movement>();

        action_ai = GetComponentInChildren<Action_AI>();
    }

    protected override void Start()
    {
        
    }

    protected override void Update()
    {
        base.Update();

        //movement.MoveByDirection_transform(new Vector2(-1, -1), stat.Speed);
        //movement.MoveToPosition_transform(Player.instance.transform.position, stat.Speed);
        action_ai.ai_process();
    }

    // �����ʿ� ����� ������ ������ ����
    // SpellProjectile Delete_FromCloneLIst()����
    public void Delete_FromCloneList()
    {
        UnitManager.Instance.Delete_FromCloneList(gameObject);
    }

    public virtual void Applier(GameObject obj, Stat stat)
    {

    }
}
