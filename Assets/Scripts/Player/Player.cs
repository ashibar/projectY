using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ũ��Ʈ �̸� : Player
/// ����� : �̿��
/// ��� : �÷��̾� ������Ʈ �ֻ��� ��ũ��Ʈ, ���� ���� ��� ����
/// ��� :
/// ������Ʈ ���� :
///     - (23.03.24) : ���� ��ü�� Unit �߰�, stat�� Unit���� �ø�
/// </summary>

public class Player : Unit
{
    public static Player instance;
    // stat�� ���� Ŭ������ Unit���� �÷Ƚ��ϴ�.

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
