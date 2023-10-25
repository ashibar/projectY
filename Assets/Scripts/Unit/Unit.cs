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
    [SerializeField] public Stat_so stat_so;
    [SerializeField] public Stat stat;
    [SerializeField] public Stat stat_processed;

    public SpellManager spellManager;
    public BuffManager buffManager;

    public Vector2 dir_toMove = new Vector2();
    public Vector2 dir_toShoot = new Vector2();
    public Vector2 pos_toShoot = new Vector2();

    protected virtual void Awake()
    {
        if (stat_so != null)
            stat = new Stat(stat_so);

        spellManager = GetComponentInChildren<SpellManager>(true);
        buffManager = GetComponentInChildren<BuffManager>(true);
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
