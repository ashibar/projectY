using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 *  
    * �ۼ��� : ������
    * ���� �ð� : 23/5/11 15:14   
    * �ۼ� ���� : �뽬���
    * ���� ���� : Unit�� stat�� speed�� ������(�ð��� ��������) ������� �뽬��� ����
        ������� ���� ���߿� ������ ����.
    * ���� ������ : Movement�� Speed�� �������� ������� ���⿡ ������ ����� Ȯ�� �Ұ�.
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
