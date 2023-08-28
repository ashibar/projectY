using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parts_SetShape_Sole : Parts_SetPrefabShape
{
    public override void Applier(Applier_parameter para)
    {
        base.Applier(para);
    }

    protected override void ShapeSetter(Applier_parameter para)
    {
        base.ShapeSetter(para);
        spellGenerater.SetSpell(prefabSet.prefabs[0]);
    }
}
