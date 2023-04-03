using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 스크립트 이름 : Unit
/// 담당자 : 이용욱
/// 요약 : 유닛 최상위 객체
/// 비고 :
/// 업데이트 내역 :
///     - (23.03.24) : 스크립트 생성
/// </summary>

public class Unit : MonoBehaviour
{
    [SerializeField]
    public Stat_so stat_so;

    [SerializeField]
    public Stat stat;

    [SerializeField]
    public Stat stat_processed;

    // movement
    // statuscheck
    // 

    // stat 세부 정보는 Stat스크립트 참조해주세요
    // 더 필요한 스탯이 생기면 바로 바로 피드백 부탁드립니다.

    public Vector2 dir_toMove = new Vector2();
    public Vector2 dir_toShoot = new Vector2();

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
