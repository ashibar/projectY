using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// ��ũ��Ʈ �̸� : Movement
/// ����� : �̿��
/// ��� : ������Ʈ ������ ���
/// ��� :
/// ������Ʈ ���� :
///     - (23.03.24) : ��ũ��Ʈ ����
/// </summary>

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb2D;

    private void Awake() // �� ��ũ��Ʈ�� ó�� ����� �ڵ����� Unit�迭 ��ũ��Ʈ�� ������ ������ ������Ʈ�� rigidbody2d�� �߰� �մϴ�.
    {
        GameObject unit_obj = GetComponentInParent<Unit>().gameObject;
        if (unit_obj != null) {
            if (unit_obj.GetComponent<Rigidbody2D>() == null)
                unit_obj.AddComponent<Rigidbody2D>();
            rb2D = unit_obj.GetComponent<Rigidbody2D>(); 
        }
        else
        {
            if (GetComponent<Rigidbody2D>() == null)
                gameObject.AddComponent<Rigidbody2D>();
            rb2D = GetComponent<Rigidbody2D>();
        }
    }

    public void MoveByDirection_transform(Vector2 dir, float spd) // ���� ��ǥ�� �ӵ��� �޾� ��ǥ�� ���ݾ� ������ �̵��մϴ�.
    {
        Vector2 pos = transform.position;
        transform.position = Vector2.MoveTowards(pos, pos + dir, spd * Time.deltaTime);
    }

    public void MoveByDirection_rigidbody(Vector2 dir, float spd) // ���� ��ǥ�� �ӵ��� �޾� rigidbody�� �ӵ����� �����մϴ�.
    {
        rb2D.velocity = dir * spd;
    }

    public void MoveToPosition_transform(Vector2 target, float spd) // ��ġ ��ǥ�� �ӵ��� �޾� ��ǥ�� ���ݾ� ������ �ش� ��ǥ�� �̵��մϴ�.
    {
        Vector2 pos = transform.position;
        Vector2 dir = (target - pos).normalized;
        transform.position = Vector2.MoveTowards(pos, pos + dir, spd * Time.deltaTime);
    }
}
