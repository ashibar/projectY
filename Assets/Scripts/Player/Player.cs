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
    public PlayerMovement playerMovement;
    public MagicManager magicManager;
    public AnimationManager animationManager;
    public GameManager manger;


    protected override void Awake()
    {
        base.Awake();
        instance = GetComponent<Player>();
        movementManger = GetComponentInChildren<MovementManager>();
        playerMovement = GetComponentInChildren<PlayerMovement>();
        magicManager = GetComponentInChildren<MagicManager>();
        animationManager = GetComponentInChildren<AnimationManager>();
        Collider collider1 = GameObject.FindWithTag("Projectile").GetComponent<Collider>();
        Collider collider2 = GameObject.FindWithTag("Ground").GetComponent<Collider>();

        Physics.IgnoreCollision(collider1, collider2, true);
    }

    protected override void Start()
    {

    }

    protected override void Update()
    {

    }

    [SerializeField]
    private GameManager gameManager;
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        //gameManager.Action(collision.gameObject);
    }

    
}
