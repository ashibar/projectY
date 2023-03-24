using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ũ��Ʈ �̸� : Unit
/// ����� : �̿��
/// ��� : ���� �ֻ��� ��ü
/// ��� :
/// ������Ʈ ���� :
///     - (23.03.24) : ��ũ��Ʈ ����
/// </summary>

public class Unit : MonoBehaviour
{
    [SerializeField]
    public Stat_so stat_so;

    [SerializeField]
    public Stat stat;

    // stat ���� ������ Stat��ũ��Ʈ �������ּ���
    // �� �ʿ��� ������ ����� �ٷ� �ٷ� �ǵ�� ��Ź�帳�ϴ�.


    protected virtual void Awake()
    {
        stat = new Stat(stat_so);
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {

    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {

    }
}