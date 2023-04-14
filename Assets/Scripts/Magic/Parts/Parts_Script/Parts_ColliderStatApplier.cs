using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parts_ColliderStatApplier : Parts_OnColide
{
    protected override void CollisionProcess(Applier_parameter para)
    {
        base.CollisionProcess(para);
        para.Collision.GetComponent<Unit>().stat.Hp_current -= para.Stat.Spell_DMG;
        Destroy(para.Proj);
    }
}
