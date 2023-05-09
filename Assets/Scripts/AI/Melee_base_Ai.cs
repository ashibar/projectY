using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Melee_base_Ai : MonoBehaviour
{
    public float speed;
    
    public Rigidbody2D target;

    Rigidbody2D rigid;
    SpriteRenderer spriter;
    private void Awake()
    {
        //게임 오브젝트에서 사용할 컴포넌트 가져오기
        target = Player.instance.GetComponent<Rigidbody2D>();
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
