using System.Collections;
using UnityEngine;
using UnityEngine.AI;

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
    [SerializeField] Rigidbody2D rb;
    //[SerializeField] private float SpeedTmp = 0f;
    [SerializeField] private float DashSpeed = 10;
    [SerializeField] private float DashTime = 1f;
    [SerializeField] private float dash_Dir = 20f;
    [SerializeField] private RaycastHit hitWall;
    [SerializeField] private Vector2 DashToward;
    private bool isDash = false;
    private float total;
    private void Start()
    {
        unit = GetComponentInParent<Unit>();
        rb = GetComponentInParent<Rigidbody2D>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
           if (!isDash) StartCoroutine(VelocityDash());
        }

    }
    
    
    private IEnumerator VelocityDash()
    {
        isDash = true;
        Vector3 dash_pos = unit.dir_toMove;
        total = Mathf.Abs(dash_pos.x) + Mathf.Abs(dash_pos.y);

        if (total != 1 && total != 0) dash_pos = new Vector2(dash_pos.x / total, dash_pos.y / total);


        Vector3 velo3 = new (dash_pos.x * DashSpeed, dash_pos.y * DashSpeed, 0);
        Debug.Log(velo3);
        rb.velocity = velo3;
        Debug.Log(dash_pos+"||||");
        
        yield return new WaitForSeconds(DashTime);
        isDash = false;
        rb.velocity = Vector3.zero;
    }


}
