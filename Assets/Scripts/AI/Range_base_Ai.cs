using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range_base_Ai : Action_AI
{
    public float speed;
    public float ATK_Range = 5f;
    public float Ra_Range = 3f;
    public Unit target;

    [SerializeField] private float cooltime_attack = 3;

    Rigidbody2D rigid;
    SpriteRenderer spriter;
    protected override void Awake()
    {
        GameObject player = GameObject.FindWithTag("Player");
        target = player.GetComponent<Player>();
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();


    }
    public override void ai_process()
    {
        base.ai_process();
        Vector2 dir = (Vector2)(target.transform.position - transform.position).normalized;
        
        unit.dir_toShoot = dir;
        unit.GetComponent<SpriteRenderer>().flipX = dir.x > 0 ? false : true;
        float Distance = Vector2.Distance(target.transform.position, rigid.position);
        
        
        if (Distance >= ATK_Range)
        {
            ai_movement(target.transform.position,dir);


        }
        else if (Distance >= 0 && Distance < Ra_Range)
        {
            ai_movement(target.transform.position, -dir);
        }
        else
        {
            //Vector2 nextvac = Vector2.zero;
            //rigid.velocity = Vector2.zero;
        }
    }
    protected override void ai_Attack_base()
    {
        base.ai_Attack_base();
        ai_Attack_cooltime(0, target.transform.position, unit.dir_toShoot, cooltime_attack);
    }
    protected override void ai_movement(Vector3 targetpos, Vector2 dir)
    {
        base.ai_movement(targetpos, dir);
        transform.position = Vector3.MoveTowards(transform.position, transform.position + (Vector3)dir, unit.stat.Speed * Time.deltaTime);
    }
    private void MoveCheck()
    {
        
    }

    private void MobMove()
    {


        //Vector2 dir = target.position - rigid.position;
        //Vector2 nextvac = dir.normalized * speed * Time.fixedDeltaTime;
        //rigid.MovePosition(rigid.position + nextvac);
        //rigid.velocity = Vector2.zero;
    }
    private void RunAwayMob()
    {
        //Vector2 dir = target.position + rigid.position;
        //Vector2 nextvac = dir.normalized * speed * Time.fixedDeltaTime;
        //rigid.MovePosition(rigid.position + nextvac);
        //rigid.velocity = Vector2.zero;
    }
}
