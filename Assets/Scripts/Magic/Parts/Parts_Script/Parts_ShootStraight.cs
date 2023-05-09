using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parts_ShootStraight : Parts_OnShot
{
    public override void Applier(Applier_parameter para)
    {
        base.Applier(para);
        para.Proj.GetComponent<Rigidbody2D>().velocity = para.Dir_toShoot * para.Stat.Spell_Speed;
        para.Proj.transform.rotation = Quaternion.Euler(CalibrateRotation(para, 90));
    }

    private Vector3 CalibrateRotation(Applier_parameter para, float angle)
    {
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, new Vector3(para.Dir_toShoot.x, para.Dir_toShoot.y, 0f));
        Vector3 rot = rotation.eulerAngles;
        rot.z += 90f;
        return rot;
    }
}
