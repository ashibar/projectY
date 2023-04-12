using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Stat_Spell
{

    enum SpellType {SOLE, MULTY, RANGE };

    private float spell_DMG;                //���� ������
    private float spell_Speed;              //����ü �ӵ�
    private float spell_CoolTime;           //����ü ��Ÿ��
    private float spell_Duration;           //����ü ���ӽð�

    private float spell_Range_Duration;     //������ ���ӽð�
    private float spell_Range_TicDMG;       //���� ƽ ������
    private float spell_Multy_EA;           //����ü ����(MULTY����)
    private float spell_Multy_Radius;       //����ü ������ ���� (MULTY����)
    private float spell_Range_Area;         //������ ����
    private readonly SpellType spell_Type;  //����ü �߻� Ÿ��


    public Stat_Spell(Stat_Spell_so spell)
    {
        this.spell_DMG = spell.Spell_DMG;
        this.spell_Speed = spell.Spell_Speed;
        this.spell_CoolTime = spell.Spell_CoolTime;
        this.spell_Duration = spell.Spell_Duration;
        this.spell_Range_Duration = spell.Spell_Range_Duration;
        this.spell_Range_TicDMG = spell.Spell_Range_TicDMG;
        this.spell_Multy_EA = spell.Spell_Multy_EA;
        this.spell_Multy_Radius = spell.Spell_Multy_Radius;
        this.spell_Range_Area = spell.Spell_Range_Area;
        this.spell_Type = (SpellType)spell.spell_Type;
    }


    public Stat_Spell(Stat_Spell spell)
    {
        this.spell_DMG = spell.spell_DMG;
        this.spell_Speed = spell.spell_Speed;
        this.spell_CoolTime = spell.spell_CoolTime;
        this.spell_Duration = spell.spell_Duration;
        this.spell_Range_Duration = spell.spell_Range_Duration;
        this.spell_Range_TicDMG = spell.spell_Range_TicDMG;
        this.spell_Multy_EA = spell.spell_Multy_EA;
        this.spell_Multy_Radius = spell.spell_Multy_Radius;
        this.spell_Range_Area = spell.spell_Range_Area;
        this.spell_Type = spell.spell_Type;
    }
}
