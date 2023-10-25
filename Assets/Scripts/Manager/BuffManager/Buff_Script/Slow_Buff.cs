using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow_Buff : Buff
{
    public override void BuffStat(Stat stat)
    {
        stat.Speed -= buff_value;
    }
}
