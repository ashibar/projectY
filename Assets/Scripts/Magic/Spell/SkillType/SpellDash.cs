using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;


/**
 *  
    * 작성자 : 박종성
    * 변경 시간 : 23/5/11 15:14   
    * 작성 내용 : 대쉬기능
    * 구현 사항 : Unit의 stat의 speed를 순간적(시간은 조정예정) 상승으로 대쉬기능 구현
        무적기능 또한 나중에 구현할 예정.
    * 현재 문제점 : Movement에 Speed가 독자적인 상수값을 쓰기에 변경이 됬는지 확인 불가.
 */
public class SpellDash : MonoBehaviour
{
    [SerializeField] Unit unit;
    [SerializeField] private float SpeedTmp = 0f;
    [SerializeField] private float DashSpeed;
    [SerializeField] private float DashTime = 1f;
    [SerializeField] private float dash_Dir = 50f;
    private float total = 0;
    private bool isDash = false;
    private void Start()
    {
        unit = GetComponentInParent<Unit>();
        
        SpeedTmp = unit.stat.Speed;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {

            if(!isDash) StartCoroutine(PerforDash());
            //if (!isDash) StartCoroutine(Dash());
        }
        
    }
    private IEnumerator PerforDash()
    {
        isDash = !isDash;
        
        Vector2 dash_pos = unit.dir_toMove;
        total = Mathf.Abs(dash_pos.x) + Mathf.Abs(dash_pos.y);
        
        if (total != 1 && total != 0) dash_pos = new Vector2(dash_pos.x / total, dash_pos.y / total);
        //대쉬 이동할때 나눠서 표현

        //만약 텔레
        for (int i = 0; i < 12; i++)
        {
            yield return new WaitForSeconds(1/15);
            unit.transform.Translate(dash_pos * dash_Dir * Time.deltaTime * 5);
        }
        
        yield return new WaitForSeconds(DashTime);
        isDash = !isDash;

    }

    //현재 수정중 : 버그( 벽 투과됨 // 가속도 받으면)
    private IEnumerator Dash()
    {
        isDash = !isDash;
        Vector3 dash_pos = unit.dir_toMove;
        total = Mathf.Abs(dash_pos.x) + Mathf.Abs(dash_pos.y);

        if (total != 1 && total != 0) dash_pos = new Vector2(dash_pos.x / total, dash_pos.y / total);

        dash_pos = (Vector3)unit.dir_toMove * dash_Dir/30 + unit.transform.position;
        //Debug.Log("dash: "+ Mathf.Abs(dash_pos.x) + Mathf.Abs(dash_pos.y));
        RaycastHit hitWall;
        bool ishit = Physics.Raycast(unit.transform.position, unit.dir_toMove, out hitWall,0.5f);
        Debug.Log(ishit);
        Debug.Log(hitWall.point);

        if (ishit && hitWall.distance <= 0.2)
        {
            
            unit.transform.position = hitWall.point;
        }
        else
        {
            unit.transform.position = dash_pos;
        }


        yield return new WaitForSeconds(DashTime);
        isDash = !isDash;
    }
}
