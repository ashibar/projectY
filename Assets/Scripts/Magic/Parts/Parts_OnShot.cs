using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parts_OnShot : Parts
{
    public override void Applier(Applier_parameter para)
    {
        Apply_Stat(para.Stat);
        
    }

    protected virtual void Apply_Stat(Stat_Spell stat)
    {
        // ���⿡ Stat_Spell ����
    }
}
