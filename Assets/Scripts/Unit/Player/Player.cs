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
    private static Player instance;
    public static Player Instance
    {
        get
        {
            if (instance == null) // instance가 비어있다
            {
                var obj = FindObjectOfType<Player>();
                if (obj != null)
                {
                    instance = obj;                                             // 전체 찾아봤는데? 있네? 그걸 넣자
                }
                else
                {
                    var newObj = new GameObject().AddComponent<Player>(); // 전체 찾아봤는데? 없네? 새로만들자
                    instance = newObj;
                }
            }
            return instance; // 안비어있네? 그냥 그대로 가져와
        }
        set { instance = value; }
    }

    // stat은 상위 클래스인 Unit으로 올렸습니다.

    public MovementManager movementManger;
    public PlayerMovement playerMovement;
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
        if (!isDead && stat != null)
            if (stat.Hp_current <= 0)
            {
                isDead = true;
                StartCoroutine(DeathCamZoom());
                animationManager.AnimationControl("Dead");
                ExtraParams para = new ExtraParams();
                para.Name = "isFail";
                para.Boolvalue = true;
                EventManager.Instance.PostNotification("Add New Trigger", this, null, para);
            }
    }

    private IEnumerator DeathCamZoom()
    {
        float end = Time.time + 1f;

        while (Time.time < end)
        {
            Camera.main.orthographicSize = Mathf.MoveTowards(Camera.main.orthographicSize, 6f, Time.deltaTime * 2);
            yield return null;
        }
    }
}
