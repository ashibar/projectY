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
        //���� ������Ʈ���� ����� ������Ʈ ��������

        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        unit = GetComponent<Unit>();


    }
    void FixedUpdate()
    {
        //if (��Ÿ��� �Ÿ����� ������)
        Vector2 dirvac = target.position - rigid.position;
        Vector2 dirva = dirvac.normalized;
        Vector2 nextvac = dirva * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextvac);
        rigid.velocity = Vector2.zero;
        //else
        //���� �� �߻�
    }

}