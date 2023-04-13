using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parts_ShapeSet : Parts_OnShot
{
    [SerializeField] private Animator animator;

    public override void Applier(GameObject proj, Stat_Spell stat, Collider2D collision)
    {
        base.Applier(proj, stat, collision);
        //proj.GetComponent<Animator>().runtimeAnimatorController = animator;
    }
}
