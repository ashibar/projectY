using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ũ��Ʈ �̸� : Enemy_melee
/// ����� : �̿��
/// ��� : �������� �� ���� Ŭ����
/// ��� : 
/// ������Ʈ ���� :
///     - (23.04.05) : ��ũ��Ʈ ����
/// </summary>

public class Enemy_melee : Enemy
{
    protected override void Awake()
    {

    }

    protected override void Start()
    {

    }

    protected override void Update()
    {
        
    }

    public override void Applier(GameObject obj, Stat stat)
    {
        Debug.Log(gameObject.name + ", m, " + GetComponentInParent<SpawnManager>().name);
        GetComponentInParent<SpawnManager>().transform.position += new Vector3(0, 1, 0);
    }
}
