using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell_Passive : Spell
{
    [SerializeField] protected Stat_so additional_stat_so;
    [SerializeField] public Stat additional_stat;

    public override void Awake()
    {
        base.Awake();
        if (additional_stat_so != null)
            additional_stat = new Stat(additional_stat_so);
    }
}
