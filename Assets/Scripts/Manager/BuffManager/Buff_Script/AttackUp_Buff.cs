using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackUp_Buff : Buff
{
    public override void BuffStat(Stat stat)
    {
        stat.Damage += buff_value;
    }
}
