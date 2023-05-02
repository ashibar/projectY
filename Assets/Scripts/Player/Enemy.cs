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
    // public Ai Enemy_ai;
    protected override void Awake()
    {
        base.Awake();
        
        if (GetComponent<Movement>() == null)
            gameObject.AddComponent<Movement>();
        movement = GetComponent<Movement>();
        // Enemy_ai = getcomponentInChildren<Enemy_ai>();
    }

    protected override void Start()
    {
        
    }

    protected override void Update()
    {
        base.Update();

        //movement.MoveByDirection_transform(new Vector2(-1, -1), stat.Speed);
        movement.MoveToPosition_transform(Player.instance.transform.position, stat.Speed);
    }

    // �����ʿ� ����� ������ ������ ����
    // SpellProjectile Delete_FromCloneLIst()����
    public void Delete_FromCloneList()
    {
        spawner_pointer.Delete_FromCloneList(gameObject);
    }

    public virtual void Applier(GameObject obj, Stat stat)
    {

    }
}
