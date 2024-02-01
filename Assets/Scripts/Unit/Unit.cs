using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 스크립트 이름 : Unit
/// 담당자 : 이용욱
/// 요약 : 유닛 최상위 객체
/// 비고 : 스텟, 스펠, 버프관련 참조 가능
/// 업데이트 내역 :
///     - (23.03.24) : 스크립트 생성
///     - (24.02.01) : 비고 및 주석 추가
/// </summary>

public class Unit : MonoBehaviour
{
    [SerializeField] public Stat_so stat_so;                // 유닛의 스텟 기본값 (수정X)
    [SerializeField] public Stat stat;                      // 유닛의 스텟
    [SerializeField] public Stat stat_processed;            // 버프 모듈이 후처리한 스텟

    public SpellManager spellManager;                       // 유닛의 스펠을 관리하는 최상위 모듈
    public BuffManager buffManager;                         // 유닛의 버프/디버프/패스브를 관리하는 최상위 모듈

    public Vector2 dir_toMove = new Vector2();              // 유닛의 이동 방향에 대한 방향좌표
    public Vector2 dir_toShoot = new Vector2();             // 유닛의 스펠 발사 방향에 대한 방향좌표
    public Vector2 pos_toShoot = new Vector2();             // 유닛의 스펠 발사 위치에 대한 방향좌표

    /// <summary>
    /// 무결성 및 하위 모듈 로드
    /// </summary>
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

    // 유닛 피격 시 빨갛게 점멸하는 효과
    // 추후에 모듈로 분리 예정
    [SerializeField] protected bool isBlinkCooltime;
    [SerializeField] protected float blinkCooltime = 0.1f;
    [SerializeField] protected Color color_origin;

    /// <summary>
    /// <b>피격 시 점멸을 위한 서브루틴 생성 함수</b>
    /// - 스펠 모듈에서 TriggerEnterStackProcess()에 아래의 코드를 입력하면 점멸함.
    /// - collision.gameObject.GetComponent<Unit>().ActiveBlink();
    /// </summary>
    public void ActiveBlink()
    {
        if (!isBlinkCooltime)
            StartCoroutine(Blink_Routine());
    }

    /// <summary>
    /// <b>점멸 서브루틴</b>
    /// </summary>
    /// <returns></returns>
    private IEnumerator Blink_Routine()
    {
        // 빨간색으로 유지될 시간
        float end = Time.time + blinkCooltime / 2;
        
        // 무결성 검사
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr == null)
            yield break;

        isBlinkCooltime = true;
        
        // 빨간색으로 변경
        color_origin = sr.color;
        sr.color = Color.red;

        // 대기
        while (Time.time < end)
        {
            yield return null;
        }

        // 원래 색으로 변경
        sr.color = color_origin;

        // 원래 색으로 유지될 시간
        end = Time.time + blinkCooltime / 2;

        //대기
        while (Time.time < end)
        {
            yield return null;
        }

        isBlinkCooltime = false;
    }

}
