using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Melee_base_Ai : Action_AI
{
    public float speed;
    
    public Rigidbody2D target;
 
    Rigidbody2D rigid;
    SpriteRenderer spriter;
    protected override void Awake()
    {
        GameObject player = GameObject.FindWithTag("Player");
        target =player.GetComponent<Rigidbody2D>();
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        
        
    }
    void FixedUpdate()
    {
        Vector2 dir = target.position - rigid.position;
        Vector2 nextvac = dir.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position+nextvac);
        rigid.velocity = Vector2.zero;
    }

}
