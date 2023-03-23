using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 스크립트 이름 : Player
/// 담당자 : 이용욱
/// 요약 : 플레이어 오브젝트 최상위 스크립트, stat정보 보유
/// 비고 :
/// </summary>

public class Player : MonoBehaviour
{
    public Stat_so stat;
    // stat 세부 정보는 Stat스크립트 참조해주세요
    // 더 필요한 스탯이 생기면 바로 바로 피드백 부탁드립니다.
    public MovementManager movementManger;
    public MagicManager magicManager;
    public AnimationManager animationManager;

    public Vector2 dir_move = new Vector2();
    public Vector2 dir_toMouse = new Vector2();

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
