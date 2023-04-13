using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parts_ColliderStatApplier : Parts_OnColide
{
    protected override void CollisionProcess(GameObject proj, Stat_Spell stat, Collider2D collision)
    {
        base.CollisionProcess(proj, stat, collision);
        collision.GetComponent<Unit>().stat.Hp_current -= stat.Spell_DMG;
        Destroy(proj);
    }
}
