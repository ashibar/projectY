using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    [SerializeField] protected Stat_Spell_so stat_spell_so;
    [SerializeField] protected Stat_Spell stat_spell;

    [SerializeField] protected Unit owner;
    [SerializeField] protected string target;

    [SerializeField] public string parent_code;

    [SerializeField] public Sprite sprite_back;
    [SerializeField] public Sprite sprite_front;
    [SerializeField] public Sprite sprite_spell;

    public virtual void Awake()
    {
        if (stat_spell_so != null)
            stat_spell = new Stat_Spell(stat_spell_so);
    }

    protected virtual void Update()
    {

    }

    /// <summary>
    /// 모듈의 주인과 공격할 타겟 태그 설정
    /// </summary>
    /// <param name="owner">모듈의 주인 유닛 컴포넌트</param>
    /// <param name="target">공격할 타겟 태그</param>
    public void SetUnits(Unit owner, string target)
    {
        this.owner = owner;
        this.target = target;
    }

    public string GetCode()
    {
        return stat_spell_so.Spell_Code;
    }

    public string GetName()
    {
        return stat_spell_so.Spell_Name;
    }
}
