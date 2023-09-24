using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat_Spell
{

    enum SpellType {SOLE, MULTY, RANGE };

    [SerializeField] private string spell_Name;              //���� �̸�

    [SerializeField] private float spell_DMG;                //���� ������
    [SerializeField] private float spell_Speed;              //����ü �ӵ�
    [SerializeField] private float spell_CoolTime;           //����ü ��Ÿ��
    [SerializeField] private float spell_Duration;           //����ü ���ӽð�
    [SerializeField] private float spell_ProjectileDelay;    //�� ��Ÿ�ӿ��� �ٷ� ����� ����ü�� ��ȯ ������

    [SerializeField] private float spell_Range_Duration;     //������ ���ӽð�
    [SerializeField] private float spell_Range_TicDMG;       //���� ƽ ������
    [SerializeField] private float spell_Multy_EA;           //����ü ����(MULTY����)
    [SerializeField] private float spell_Multy_Radius;       //����ü ������ ���� (MULTY����)
    [SerializeField] private float spell_Range_Area;         //������ ����
    [SerializeField] private readonly SpellType spell_Type;  //����ü �߻� Ÿ��


    [SerializeField] private SpellType Spell_Type => spell_Type;

    public Stat_Spell(Stat_Spell_so spell)
    {
        this.Spell_Name = spell.Spell_Name;
        this.spell_DMG = spell.Spell_DMG;
        this.spell_Speed = spell.Spell_Speed;
        this.spell_CoolTime = spell.Spell_CoolTime;
        this.spell_Duration = spell.Spell_Duration;
        this.spell_ProjectileDelay = spell.Spell_ProjectileDelay;
        this.spell_Range_Duration = spell.Spell_Range_Duration;
        this.spell_Range_TicDMG = spell.Spell_Range_TicDMG;
        this.spell_Multy_EA = spell.Spell_Multy_EA;
        this.spell_Multy_Radius = spell.Spell_Multy_Radius;
        this.spell_Range_Area = spell.Spell_Range_Area;
        this.spell_Type = (SpellType)spell.spell_Type;
    }


    public Stat_Spell(Stat_Spell spell)
    {
        this.Spell_Name = spell.Spell_Name;
        this.spell_DMG = spell.spell_DMG;
        this.spell_Speed = spell.spell_Speed;
        this.spell_CoolTime = spell.spell_CoolTime;
        this.spell_Duration = spell.spell_Duration;
        this.spell_ProjectileDelay = spell.Spell_ProjectileDelay;
        this.spell_Range_Duration = spell.spell_Range_Duration;
        this.spell_Range_TicDMG = spell.spell_Range_TicDMG;
        this.spell_Multy_EA = spell.spell_Multy_EA;
        this.spell_Multy_Radius = spell.spell_Multy_Radius;
        this.spell_Range_Area = spell.spell_Range_Area;
        this.spell_Type = spell.spell_Type;
    }

    public string Spell_Name { get => spell_Name; set => spell_Name = value; }
    public float Spell_DMG { get => spell_DMG; set => spell_DMG = value; }
    public float Spell_Speed { get => spell_Speed; set => spell_Speed = value; }
    public float Spell_CoolTime { get => spell_CoolTime; set => spell_CoolTime = value; }
    public float Spell_Duration { get => spell_Duration; set => spell_Duration = value; }
    public float Spell_Range_Duration { get => spell_Range_Duration; set => spell_Range_Duration = value; }
    public float Spell_Range_TicDMG { get => spell_Range_TicDMG; set => spell_Range_TicDMG = value; }
    public float Spell_Multy_EA { get => spell_Multy_EA; set => spell_Multy_EA = value; }
    public float Spell_Multy_Radius { get => spell_Multy_Radius; set => spell_Multy_Radius = value; }
    public float Spell_Range_Area { get => spell_Range_Area; set => spell_Range_Area = value; }
    public float Spell_ProjectileDelay { get => spell_ProjectileDelay; set => spell_ProjectileDelay = value; }
}
