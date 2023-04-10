using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 스크립트 이름 : Enemy_melee
/// 담당자 : 이용욱
/// 요약 : 근접공격 적 범주 클래스
/// 비고 : 
/// 업데이트 내역 :
///     - (23.04.05) : 스크립트 생성
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
