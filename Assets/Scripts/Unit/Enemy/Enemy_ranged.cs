using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 스크립트 이름 : Enemy_ranged
/// 담당자 : 이용욱
/// 요약 : 원거리 공격 적 범주 클래스
/// 비고 : 
/// 업데이트 내역 :
///     - (23.04.05) : 스크립트 생성
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
