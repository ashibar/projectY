using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parts : MonoBehaviour
{
    public enum Parts_Sort
    {
        OnShot,
        OnUpdate,
        OnColide
    }                               // 발사당시 | 발사동안 | 충돌당시
    public enum Parts_Ability
    {
        none,
        stat,
        visual,
        throw_or_area
    }                            // 없음 | 스탯적용 | 시각효과 | 투사체or장판형
    [SerializeField] private Parts_Sort sort;
    [SerializeField] private Parts_Ability ability;
    [SerializeField] protected Stat_Spell_so stat_spell_so;
    [SerializeField] protected Stat_Spell stat_spell;

    public Stat_Spell Stat_spell { get => stat_spell; set => stat_spell = value; }
    public Stat_Spell_so Stat_spell_so { get => stat_spell_so; set => stat_spell_so = value; }
    public Parts_Sort Sort { get => sort; set => sort = value; }
    public Parts_Ability Ability { get => ability; set => ability = value; }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {

    }

    public virtual void Applier(Applier_parameter para)
    {

    }
}
