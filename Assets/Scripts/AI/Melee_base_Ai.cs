using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee_base_Ai : Action_AI
{
    public float speed;
    
    public Rigidbody2D target;

    Rigidbody2D rigid;
    SpriteRenderer spriter;
    protected override void Awake()
    {
        GameObject player = Player.Instance.gameObject;
        target =player.GetComponent<Rigidbody2D>();
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        
        unit = GetComponent<Unit>();
        speed = unit.stat.Speed;
    }
    public override void ai_process()
    {
        //base.ai_process();
        Vector2 dir = target.position - rigid.position;
        Vector2 nextvac = dir.normalized * unit.stat.Speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextvac);
        rigid.velocity = Vector2.zero;
    }
    void FixedUpdate()
    {
        //Vector2 dir = target.position - rigid.position;
        //Vector2 nextvac = dir.normalized * speed * Time.fixedDeltaTime;
        //rigid.MovePosition(rigid.position+nextvac);
        //rigid.velocity = Vector2.zero;
    }

}
