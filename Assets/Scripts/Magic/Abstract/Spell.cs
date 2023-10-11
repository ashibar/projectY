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
    /// ����� ���ΰ� ������ Ÿ�� �±� ����
    /// </summary>
    /// <param name="owner">����� ���� ���� ������Ʈ</param>
    /// <param name="target">������ Ÿ�� �±�</param>
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
