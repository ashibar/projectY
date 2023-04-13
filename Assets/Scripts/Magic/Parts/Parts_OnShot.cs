using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parts_OnShot : Parts
{
    public override void Applier(GameObject proj, Stat_Spell stat, Collider2D collision)
    {
        Apply_Stat(stat);
        
    }

    protected virtual void Apply_Stat(Stat_Spell stat)
    {
        // 여기에 Stat_Spell 변경
    }
}
