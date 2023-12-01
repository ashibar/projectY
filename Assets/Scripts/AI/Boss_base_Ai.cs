using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class Boss_base_Ai : Action_AI
{
    [SerializeField] private Unit target;                   // 대쉬 및 공격 대상

    [SerializeField] private float ATK_Range = 5f;          // 플레이어 간격
    [SerializeField] private float beforeDelay = 1f;        // 대쉬스펠 선딜
    [SerializeField] private float dashSpeed = 2f;          // 대쉬스펠 돌진속도 
    [SerializeField] private float dashDuration = 0.3f;     // 대쉬스펠 지속시간
    [SerializeField] private float afterDelay = 1.5f;       // 대쉬스펠 후딜
    [SerializeField] private float dashCooltime = 5f;       // 대쉬스펠 쿨타임
    [SerializeField] private bool smartDash;                // 스마트 대쉬(돌진 방향을 선딜 이후에 결정)

    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private Animator animator;             // 애니메이터
    [SerializeField] private AfterImage afterImage;         // 잔상 모듈

    public float spd_temp = 0.02f;
    public float spd_sync = 0.02f;

    protected override void Awake()
    {
        GameObject player = GameObject.FindWithTag("Player");
        target = player.GetComponent<Player>();
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        afterImage = GetComponent<AfterImage>();
    }
    public override void ai_process()
    {
        float Distance = Vector2.Distance(target.transform.position, transform.position);
        Vector2 dir = (Vector2)(target.transform.position - transform.position).normalized;
        unit.dir_toShoot = dir;
        rb2d.velocity = Vector2.zero;
        //Debug.Log(Distance);
        if (!isDashCasting)
        {
            unit.GetComponent<SpriteRenderer>().flipX = dir.x > 0 ? false : true;
            SummonProcess();
        }
        if ((Distance >= ATK_Range || isDashCooltime) && !isDashCasting)
        {
            if (!isSummonCasting)
            {
                //Debug.Log(dir);
                ai_movement(target.transform.position, dir);
            }
                            
        }
        else
        {
            DashProcess();
        }
    }

    protected override void ai_movement(Vector3 targetpos, Vector2 dir)
    {
        base.ai_movement(targetpos, dir);
        transform.position = Vector3.MoveTowards(transform.position, transform.position + (Vector3)dir, unit.stat.Speed * Time.deltaTime);
        //spd_temp = unit.stat.Speed * Time.deltaTime *  ;
    }

    [SerializeField] private bool isDashCooltime;
    [SerializeField] private bool isDashCasting;
    [SerializeField] private bool isInterrupted;
    [SerializeField] private Vector2 dash_vec;
    private async void DashProcess()
    {
        if (!isDashCooltime)
        {
            isDashCooltime = true;
            isDashCasting = true;
            DashTimer(dashCooltime);
            await WaitBeforeCast(beforeDelay);
            await DashCast(dashDuration);
            await SlashCast(afterDelay);
        }
    }
    private async Task WaitBeforeCast(float duration)
    {
        if (cts.Token.IsCancellationRequested)
            return;

        float end = Time.time + duration;
        if (!smartDash)
            dash_vec = (target.transform.position - transform.position).normalized;
        animator.SetTrigger("isBeforeDelay");
        while (Time.time < end && !cts.Token.IsCancellationRequested)
        {
            // 선 동작
            await Task.Yield();
        }
        if (smartDash && !cts.Token.IsCancellationRequested)
            dash_vec = (target.transform.position - transform.position).normalized;
    }
    private async Task DashCast(float duration)
    {
        if (cts.Token.IsCancellationRequested)
            return;

        float end = Time.time + duration;
        while (Time.time < end && !cts.Token.IsCancellationRequested)
        {
            float start = Time.time;
            afterImage.SetImage(gameObject, unit.GetComponent<SpriteRenderer>().flipX);
            afterImage.IsActive = true;
            //Debug.Log(spd_temp);
            transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + dash_vec, dashSpeed * Time.deltaTime);
            
            await Task.Delay(1);
            Debug.Log(Time.time - start);
            spd_sync = spd_temp;
        }
    }
    private async Task SlashCast(float duration)
    {
        if (cts.Token.IsCancellationRequested)
            return;

        float end = Time.time + duration;
        animator.SetTrigger("isSlash");
        afterImage.IsActive = false;
        while (Time.time < end && !cts.Token.IsCancellationRequested)
        {
           await Task.Yield();
        }
        animator.SetTrigger("isWalk");
        isDashCasting = false;
    }
    private async void DashTimer(float duration)
    {
        if (cts.Token.IsCancellationRequested)
            return;

        float end = Time.time + duration;
        while (Time.time < end && !cts.Token.IsCancellationRequested)
        {
           await Task.Yield();
        }
        isDashCooltime = false;
    }

    [SerializeField] private bool isSummonCooltime;
    [SerializeField] private bool isSummonCasting;
    [SerializeField] private SpawnInfoContainer spawnInfo;
    private async void SummonProcess()
    {
        if (!isSummonCooltime)
        {
            isSummonCooltime = true;
            isSummonCasting = true;
            SummonTimer(7f);
            Debug.Log("a");
            await WaitBeforeSummon(beforeDelay);
            Debug.Log("b");
            await SummonCast(0.5f);
        }
    }
    private async Task WaitBeforeSummon(float duration)
    {
        if (cts.Token.IsCancellationRequested)
            return;

        float end = Time.time + duration;

        // 소환 모션
        while (Time.time < end && !cts.Token.IsCancellationRequested)
        {
            await Task.Yield();
        }

    }
    private async Task SummonCast(float duration)
    {
        if (cts.Token.IsCancellationRequested)
            return;

        float end = Time.time + duration;

        Debug.Log("summon");
        ExtraParams para = new ExtraParams();
        para.SpawnInfo = spawnInfo;
        EventManager.Instance.PostNotification("Spawn By Spawn Info", this, null, para);

        while (Time.time < end && !cts.Token.IsCancellationRequested)
        {
            await Task.Yield();
        }

        isSummonCasting = false;
    }
    private async void SummonTimer(float duration)
    {
        if (cts.Token.IsCancellationRequested)
            return;

        float end = Time.time + duration;
        while (Time.time < end && !cts.Token.IsCancellationRequested)
        {
            await Task.Yield();
        }
        
        isSummonCooltime = false;
    }
    
}
