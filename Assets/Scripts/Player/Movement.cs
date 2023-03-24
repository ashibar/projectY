using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 스크립트 이름 : Movement
/// 담당자 : 이용욱
/// 요약 : 오브젝트 움직임 모듈
/// 비고 :
/// 업데이트 내역 :
///     - (23.03.24) : 스크립트 생성
/// </summary>

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb2D;

    private void Awake() // 이 스크립트는 처음 실행시 자동으로 Unit계열 스크립트를 감지해 장착된 오브젝트에 rigidbody2d를 추가 합니다.
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

    public void MoveByDirection_transform(Vector2 dir, float spd) // 방향 좌표와 속도를 받아 좌표를 조금씩 움직여 이동합니다.
    {
        Vector2 pos = transform.position;
        transform.position = Vector2.MoveTowards(pos, pos + dir, spd * Time.deltaTime);
    }

    public void MoveByDirection_rigidbody(Vector2 dir, float spd) // 방향 좌표와 속도를 받아 rigidbody의 속도값을 조정합니다.
    {
        rb2D.velocity = dir * spd;
    }

    public void MoveToPosition_transform(Vector2 target, float spd) // 위치 좌표와 속도를 받아 좌표를 조금씩 움직여 해당 좌표로 이동합니다.
    {
        Vector2 pos = transform.position;
        Vector2 dir = (target - pos).normalized;
        transform.position = Vector2.MoveTowards(pos, pos + dir, spd * Time.deltaTime);
    }
}
