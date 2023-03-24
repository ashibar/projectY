using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 스크립트 이름 : Player
/// 담당자 : 이용욱
/// 요약 : 플레이어 오브젝트 최상위 스크립트, 하위 관리 모듈 관리
/// 비고 :
/// 업데이트 내역 :
///     - (23.03.24) : 상위 객체에 Unit 추가, stat을 Unit으로 올림
/// </summary>

public class Player : Unit
{
    public static Player instance;
    // stat은 상위 클래스인 Unit으로 올렸습니다.
    
    public MovementManager movementManger;
    public MagicManager magicManager;
    public AnimationManager animationManager;

    public Vector2 dir_move = new Vector2();
    public Vector2 dir_toMouse = new Vector2();

    protected override void Awake()
    {
        base.Awake();
        instance = GetComponent<Player>();
        movementManger = GetComponentInChildren<MovementManager>();
        magicManager = GetComponentInChildren<MagicManager>();
        animationManager = GetComponentInChildren<AnimationManager>();
    }

    protected override void Start()
    {
        
    }

    protected override void Update()
    {
        
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    
}
