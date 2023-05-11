using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
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
        //�뽬 �̵��Ҷ� ������ ǥ��

        //���� �ڷ�
        for (int i = 0; i < 12; i++)
        {
            yield return new WaitForSeconds(1/15);
            unit.transform.Translate(dash_pos * dash_Dir * Time.deltaTime * 5);
        }
        
        yield return new WaitForSeconds(DashTime);
        isDash = !isDash;

    }

    //���� ������ : ����( �� ������ // ���ӵ� ������)
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
