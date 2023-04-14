using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parts_ShootStraight : Parts_OnShot
{
    public override void Applier(Applier_parameter para)
    {
        base.Applier(para);
        para.Proj.GetComponent<Rigidbody2D>().velocity = Vector2.right * para.Stat.Spell_Speed;
    }
}
