using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Stat_Spell
{

    enum SpellType {SOLE, MULTY, RANGE };

    private float spell_DMG;                //마법 데미지
    private float spell_Speed;              //투사체 속도
    private float spell_CoolTime;           //투사체 쿨타임
    private float spell_Duration;           //투사체 지속시간

    private float spell_Range_Duration;     //범위기 지속시간
    private float spell_Range_TicDMG;       //범위 틱 데미지
    private float spell_Multy_EA;           //투사체 개수(MULTY한정)
    private float spell_Multy_Radius;       //투사체 퍼지는 범위 (MULTY한정)
    private float spell_Range_Area;         //범위기 넓이
    private readonly SpellType spell_Type;  //투사체 발사 타입


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
