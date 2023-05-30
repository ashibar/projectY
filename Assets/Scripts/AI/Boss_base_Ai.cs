using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_base_Ai : Action_AI
{
    public float speed;
    public float ATK_Range = 2f; //플레이어 간격
    public Rigidbody2D target;
    Rigidbody2D rigid;
    SpriteRenderer spriter;
    protected override void Awake()
    {
        GameObject player = GameObject.FindWithTag("Player");
        target = player.GetComponent<Rigidbody2D>();
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();

    }
    void FixedUpdate()
    {
        

        float Distance = Vector2.Distance(target.position, rigid.position);
        //Debug.Log(Distance);
        if (Distance >= ATK_Range)
        {
           MobMove();

        }
        else
        {
            
            
        }


    }
    private void MobMove()
    {
        Vector2 dir = target.position - rigid.position;
        Vector2 nextvac = dir.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextvac);
        rigid.velocity = Vector2.zero;
    }
   

}
