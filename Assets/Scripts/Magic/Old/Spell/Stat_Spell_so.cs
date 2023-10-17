using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultStat_spell", menuName = "Scriptable Object/Stat_spell", order = int.MaxValue)]
public class Stat_Spell_so : ScriptableObject
{
    [SerializeField] private SpellType spell_Type;           //스펠 타입
    [SerializeField] private string spell_Name;              //스펠 이름
    [SerializeField] private string spell_Code;              //스펠 코드

    [SerializeField] private float spell_DMG;                //마법 데미지
    [SerializeField] private float spell_Speed;              //투사체 속도
    [SerializeField] private float spell_CoolTime;           //투사체 쿨타임
    [SerializeField] private float spell_Duration;           //투사체 지속시간
    [SerializeField] private float spell_ProjectileDelay;    //한 쿨타임에서 다량 생산된 투사체간 소환 딜레이

    [SerializeField] private float spell_Range_Duration;     //범위기 지속시간
    [SerializeField] private float spell_Range_TicDMG;       //범위 틱 데미지
    [SerializeField] private float spell_Multy_EA;           //투사체 개수(MULTY한정)
    [SerializeField] private float spell_Multy_Radius;       //투사체 퍼지는 범위 (MULTY한정)
    [SerializeField] private float spell_Range_Area;         //범위기 넓이
    [SerializeField] private float spell_Amount_Tic = 1;     //틱당 상호작용하는 유닛의 개수

    public SpellType Spell_Type { get => spell_Type; set => spell_Type = value; }
    public string Spell_Name { get => spell_Name; set => spell_Name = value; }
    public string Spell_Code { get => spell_Code; set => spell_Code = value; }
    public float Spell_DMG { get => spell_DMG; set => spell_DMG = value; }
    public float Spell_Speed { get => spell_Speed; set => spell_Speed = value; }
    public float Spell_CoolTime { get => spell_CoolTime; set => spell_CoolTime = value; }
    public float Spell_Duration { get => spell_Duration; set => spell_Duration = value; }
    public float Spell_ProjectileDelay { get => spell_ProjectileDelay; set => spell_ProjectileDelay = value; }
    public float Spell_Range_Duration { get => spell_Range_Duration; set => spell_Range_Duration = value; }
    public float Spell_Range_TicDMG { get => spell_Range_TicDMG; set => spell_Range_TicDMG = value; }
    public float Spell_Multy_EA { get => spell_Multy_EA; set => spell_Multy_EA = value; }
    public float Spell_Multy_Radius { get => spell_Multy_Radius; set => spell_Multy_Radius = value; }
    public float Spell_Range_Area { get => spell_Range_Area; set => spell_Range_Area = value; }
    public float Spell_Amount_Tic { get => spell_Amount_Tic; set => spell_Amount_Tic = value; }
}

public enum SpellType { Core, Part, Element, Passive };
