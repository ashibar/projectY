using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parts_ShootStraight : Parts_OnShot
{
    public override void Applier(GameObject proj, Stat_Spell stat, Collider2D collision)
    {
        base.Applier(proj, stat, collision);
        proj.GetComponent<Rigidbody2D>().velocity = Vector2.right * stat.Spell_Speed;
    }
}
