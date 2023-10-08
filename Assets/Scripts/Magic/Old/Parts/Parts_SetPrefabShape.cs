using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parts_SetPrefabShape : Parts_OnInstantiate
{
    [SerializeField] protected PrefabSet_so prefabSet;

    public override void Applier(Applier_parameter para)
    {
        base.Applier(para);
        ShapeSetter(para);
    }

    protected virtual void ShapeSetter(Applier_parameter para)
    {
        

    }
}
