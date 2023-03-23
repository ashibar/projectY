using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultSpellStat", menuName = "Scriptable Object/SpellStat", order = int.MaxValue)]
public class Spell_Stat_so : ScriptableObject
{
    [SerializeField] private float coolTime;
    [SerializeField] private float attack;

    public float CoolTime { get => coolTime; set => coolTime = value; }
    public float Attack { get => attack; set => attack = value; }
}
