using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BatControl : MonoBehaviour
{
    [SerializeField] private Unit unit;
    [SerializeField] private Vector2 dir_toShoot;
    [SerializeField] private float range;

    private void Awake()
    {
        unit = GetComponentInParent<Unit>();
    }

    private void Update()
    {
        dir_toShoot = unit.dir_toShoot;
        SetPos();
        SetRot();
    }

    private void SetPos()
    {
        Vector2 targetPosition = dir_toShoot * range;
        transform.localPosition = targetPosition;
    }

    private void SetRot()
    {
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, new Vector3(dir_toShoot.x, dir_toShoot.y, 0f));
        Vector3 rot = rotation.eulerAngles;
        rot.z += 90f;
        transform.rotation = Quaternion.Euler(rot);
    }
}
