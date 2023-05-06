using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range_base_Ai : MonoBehaviour
{
    public float speed;

    public Rigidbody2D target;
    public Unit unit;

    Rigidbody2D rigid;
    SpriteRenderer spriter;
    private void Awake()
    {
        //게임 오브젝트에서 사용할 컴포넌트 가져오기

        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        unit = GetComponent<Unit>();


    }
    void FixedUpdate()
    {
        //if (사거리가 거리보다 작으면)
        Vector2 dirvac = target.position - rigid.position;
        Vector2 dirva = dirvac.normalized;
        Vector2 nextvac = dirva * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextvac);
        rigid.velocity = Vector2.zero;
        //else
        //정지 후 발사
    }

}