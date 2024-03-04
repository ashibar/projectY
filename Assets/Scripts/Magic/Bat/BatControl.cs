using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BatControl : MonoBehaviour
{
    [SerializeField] private Unit unit;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Animator animator;
    [SerializeField] private Vector2 dir_toShoot;
    [SerializeField] private float range;
    [SerializeField] private Vector3 angle;
    [SerializeField] private string state;


    private void Awake()
    {
        unit = GetComponentInParent<Unit>();
    }

    private void Update()
    {
        dir_toShoot = unit.dir_toShoot;
        SetPos();
        SetState();
        SetAnimation();
    }

    private void SetPos()
    {
        Vector2 targetPosition = dir_toShoot * range;
        sr.transform.localPosition = targetPosition;
    }

    private void SetState()
    {
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, new Vector3(dir_toShoot.x, dir_toShoot.y, 0f));
        angle = rotation.eulerAngles;
        float z = angle.z;

        if ((315 <= z && z < 360) || (0 <= z && z < 45))
        {
            state = "back";
            sr.sortingOrder = -1;
        }
        else if (45 <= z && z < 135)
        {
            state = "left";
            sr.sortingOrder = -1;
        }
        else if (135 <= z && z < 225)
        {
            state = "front";
            sr.sortingOrder = +1;
        }
        else if (225 <= z && z < 315)
        {
            state = "right";
            sr.sortingOrder = -1;
        }
    }

    private void SetAnimation()
    {
        animator.SetTrigger(state);
    }

    private void SetRot()
    {
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, new Vector3(dir_toShoot.x, dir_toShoot.y, 0f));
        Vector3 rot = rotation.eulerAngles;
        rot.z += 90f;
        transform.rotation = Quaternion.Euler(rot);
    }
}
