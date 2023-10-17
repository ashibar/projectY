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
    private static Player instance;
    public static Player Instance
    {
        get
        {
            if (instance == null) // instance�� ����ִ�
            {
                var obj = FindObjectOfType<Player>();
                if (obj != null)
                {
                    instance = obj;                                             // ��ü ã�ƺôµ�? �ֳ�? �װ� ����
                }
                else
                {
                    var newObj = new GameObject().AddComponent<Player>(); // ��ü ã�ƺôµ�? ����? ���θ�����
                    instance = newObj;
                }
            }
            return instance; // �Ⱥ���ֳ�? �׳� �״�� ������
        }
    }

    // stat�� ���� Ŭ������ Unit���� �÷Ƚ��ϴ�.

    public MovementManager movementManger;
    public PlayerMovement playerMovement;
    public SpellManager spellManager;
    public AnimationManager animationManager;
    public GameManager manger;


    protected override void Awake()
    {
        base.Awake();
        var objs = FindObjectsOfType<Player>();
        if (objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }
        movementManger = GetComponentInChildren<MovementManager>();
        playerMovement = GetComponentInChildren<PlayerMovement>();
        spellManager = GetComponentInChildren<SpellManager>(true);
        animationManager = GetComponentInChildren<AnimationManager>();
        
    }

    protected override void Start()
    {

    }

    protected override void Update()
    {
        PlayerDeathSender();
    }

    private bool isDead;
    private void PlayerDeathSender()
    {
        if (!isDead)
            if (stat.Hp_current <= 0)
            {
                isDead = true;
                ExtraParams para = new ExtraParams();
                para.Name = "isFail";
                para.Boolvalue = true;
                EventManager.Instance.PostNotification("Add New Trigger", this, null, para);
            }
    }
}
