using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parts_OnInstantiate : Parts
{
    [SerializeField] protected ShotManage spellGenerater;

    public override void Applier(Applier_parameter para)
    {
        base.Applier(para);
        spellGenerater = para.Generater;
    }
}
