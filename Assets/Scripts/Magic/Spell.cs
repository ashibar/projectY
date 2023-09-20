using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    [SerializeField] protected Stat_Spell_so stat_spell_so;
    [SerializeField] protected Stat_Spell stat_spell;
    
    public virtual void Awake()
    {
        stat_spell = new Stat_Spell(stat_spell_so);
    }

    protected virtual void Update()
    {

    }
}
