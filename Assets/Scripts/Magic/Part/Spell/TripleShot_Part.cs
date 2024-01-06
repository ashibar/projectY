using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleShot_Part : Spell_Part
{
    private float max_rad = 30;
    
    public override void SetAngle(DelegateParameter para)
    {
        int amount = (int)para.stat_processed.Amount;
        int cnt = para.proj_cnt;

        float angle = Mathf.Atan2(para.dir_toShoot.y, para.dir_toShoot.x) * Mathf.Rad2Deg;
        angle = (angle - max_rad) + (cnt * ((2*max_rad) / (amount)));
        Quaternion rotation = Quaternion.Euler(0, 0, angle);

        para.rotation = rotation;
    }
}