using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range_base_Ai : MonoBehaviour
{
    public float speed;
    public float ATK_Range = 5f;
    public float Ra_Range = 3f;
    public Rigidbody2D target;
    

    Rigidbody2D rigid;
    SpriteRenderer spriter;
    private void Awake()
    {
        GameObject player = GameObject.FindWithTag("Player");
        target = player.GetComponent<Rigidbody2D>();
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();


    }
    void FixedUpdate()
    {
        
       
        float Distance = Vector2.Distance(target.position, rigid.position);
        Debug.Log(Distance);
        if (Distance >= ATK_Range)
        {
            MobMove();


        }
        else if(Distance >=0 && Distance < Ra_Range)
        {
            RunAwayMob();
        }
        else 
        {
            Vector2 nextvac = Vector2.zero;
            rigid.velocity = Vector2.zero;


        }

    }
    private void MoveCheck()
    {
        
    }
    private void MobMove()
    {
        Vector2 dir = target.position - rigid.position;
        Vector2 nextvac = dir.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextvac);
        rigid.velocity = Vector2.zero;
    }
    private void RunAwayMob()
    {
        Vector2 dir = target.position + rigid.position;
        Vector2 nextvac = dir.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextvac);
        rigid.velocity = Vector2.zero;
    }
}
