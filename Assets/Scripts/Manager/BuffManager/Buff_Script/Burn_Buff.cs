using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burn_Buff : Buff
{
    protected override void Tick_Function()
    {
        UnitManager.Instance.damageCalculation.Calculate(buffmanager.unit.gameObject, buff_value, Color.red);
        stat.Hp_current -= buff_value;
    }
}
