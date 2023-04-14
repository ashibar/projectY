using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parts_ShapeSet : Parts_OnShot
{
    [SerializeField] private Animator animator;

    public override void Applier(Applier_parameter para)
    {
        base.Applier(para);
        //proj.GetComponent<Animator>().runtimeAnimatorController = animator;
    }
}
