using System.Collections;
using System.Collections.Generic;
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

    private void Start()
    {
        unit = GetComponentInParent<Unit>();
        SpeedTmp = unit.stat.Speed;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartCoroutine(PerforDash());
        }
        
    }
    private IEnumerator PerforDash()
    {
        DashSpeed = SpeedTmp * 10;
        unit.stat_processed.Speed = DashSpeed;
        yield return new WaitForSeconds(DashTime);
        unit.stat_processed.Speed = SpeedTmp;
    }
}
