using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class Boss_base_Ai : Action_AI
{
    public Rigidbody2D target;
    Rigidbody2D rigid;
    SpriteRenderer spriter;

    [SerializeField] private Animator anim;
    [SerializeField] private float ATK_Range = 5f; //플레이어 간격
    [SerializeField] private float beforeDelay = 1f;
    [SerializeField] private float dashSpeed = 2f;
    [SerializeField] private float dashDuration = 0.3f;
    [SerializeField] private float afterDelay = 1.5f;
    [SerializeField] private float dashCooltime = 5f;
    [SerializeField] private bool smartDash;
    [SerializeField] private AfterImage afterImage;

    protected override void Awake()
    {
        anim = GetComponent<Animator>();
        GameObject player = GameObject.FindWithTag("Player");
        target = player.GetComponent<Rigidbody2D>();
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        afterImage = GetComponent<AfterImage>();
    }
    public override void ai_process()
    {
        float Distance = Vector2.Distance(target.position, rigid.position);
        //Debug.Log(Distance);
        if ((Distance >= ATK_Range || isCooltime_) && !isCasting)
        {
            MobMove();
        }
        else
        {
            DashProcess();
        }
    }
    private void MobMove()
    {
        Vector2 dir = target.position - rigid.position;
        Vector2 nextvac = dir.normalized;
        transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + nextvac, unit.stat.Speed * Time.deltaTime);
        rigid.MovePosition(rigid.position + nextvac);
        rigid.velocity = Vector2.zero;
    }
    [SerializeField] private bool isCooltime_;
    [SerializeField] private bool isCasting;
    [SerializeField] private bool isInterrupted;
    [SerializeField] private Vector2 dash_vec;
    private async void DashProcess()
    {
        if (!isCooltime_)
        {
            isCooltime_ = true;
            isCasting = true;
            DashTimer(dashCooltime);
            await WaitBeforeCast(beforeDelay);
            await DashCast(dashDuration);
            await SlashCast(afterDelay);
        }
        if (isInterrupted)
        {
            isInterrupted = false;
        }
    }
    private async Task WaitBeforeCast(float duration)
    {
        float end = Time.time + duration;
        if (!smartDash)
            dash_vec = (target.position - rigid.position).normalized;
        anim.SetTrigger("isBeforeDelay");
        while (Time.time < end)
        {
            if (isInterrupted)
            {
                await Task.FromResult(0);
            }
            // 선 동작
            await Task.Yield();
        }
        if (smartDash)
            dash_vec = (target.position - rigid.position).normalized;
    }
    private async Task DashCast(float duration)
    {
        float end = Time.time + duration;
        while (Time.time < end)
        {
            if (isInterrupted)
            {
                await Task.FromResult(0);
            }
            afterImage.SetImage(gameObject);
            afterImage.IsActive = true;
            transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + dash_vec, dashSpeed * Time.deltaTime);

            await Task.Yield();
        }
    }
    private async Task SlashCast(float duration)
    {
        float end = Time.time + duration;
        anim.SetTrigger("isSlash");
        afterImage.IsActive = false;
        while (Time.time < end)
        {
            if (isInterrupted)
            {
                await Task.FromResult(0);
            }
            await Task.Yield();
        }
        anim.SetTrigger("isWalk");
        isCasting = false;
    }
    private async void DashTimer(float duration)
    {
        float end = Time.time + duration;
        while (Time.time < end)
        {
            if (isInterrupted)
            {
                await Task.FromResult(0);
            }
            await Task.Yield();
        }
        isCooltime_ = false;
    }
}
