using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ũ��Ʈ �̸� : Enemy_ranged
/// ����� : �̿��
/// ��� : ���Ÿ� ���� �� ���� Ŭ����
/// ��� : 
/// ������Ʈ ���� :
///     - (23.04.05) : ��ũ��Ʈ ����
/// </summary>

public class Enemy_ranged : Enemy
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
        Debug.Log(gameObject.name + ", r," + GetComponentInParent<SpawnManager>().name);
        GetComponentInParent<SpawnManager>().transform.position += new Vector3(1, 0, 0);
    }
}
