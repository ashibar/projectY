using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

using UnityEngine.AI;

public class SpellDash : MonoBehaviour
{
    [SerializeField] Unit unit;

    [SerializeField] Rigidbody2D rb;
    [SerializeField] private float DashSpeed= 30;
    [SerializeField] private float DashTime = 0.1f;
    private float total;
    [SerializeField] private Vector2 DashToward;
    private bool isDash = false;

    private void Start()
    {
        unit = GetComponentInParent<Unit>();
        rb = GetComponentInParent<Rigidbody2D>();

        DashSpeed = 30f;
        DashTime = 0.1f;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {

            //if (!isDash) StartCoroutine(Dash());
            if (!isDash) StartCoroutine(VelocityDash());
        }

    }
   

    private IEnumerator VelocityDash()
    {
        isDash = !isDash;
        isDash = true;
        Vector3 dash_pos = unit.dir_toMove;
        total = Mathf.Abs(dash_pos.x) + Mathf.Abs(dash_pos.y);

        if (total != 1 && total != 0) dash_pos = new Vector2(dash_pos.x / total, dash_pos.y / total);

        


        Vector3 velo3 = new(dash_pos.x * DashSpeed, dash_pos.y * DashSpeed, 0);
        Debug.Log(velo3);
        rb.velocity = velo3;
        Debug.Log(dash_pos + "||||");

        yield return new WaitForSeconds(DashTime);
        isDash = false;
        rb.velocity = Vector3.zero;
    }


}