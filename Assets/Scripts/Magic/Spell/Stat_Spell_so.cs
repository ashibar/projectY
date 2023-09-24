using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultStat_spell", menuName = "Scriptable Object/Stat_spell", order = int.MaxValue)]
public class Stat_Spell_so : ScriptableObject
{
    public enum SpellType { SOLE, MULTY, RANGE };
    [SerializeField] private string spell_Name;              //스펠 이름
    
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
    [SerializeField] public SpellType spell_Type;           //투사체 발사 타입

    public float Spell_DMG { get => spell_DMG;}
    public float Spell_Speed { get => spell_Speed;}
    public float Spell_CoolTime { get => spell_CoolTime;}
    public float Spell_Duration { get => spell_Duration;}
    public float Spell_Range_Duration { get => spell_Range_Duration;}
    public float Spell_Range_TicDMG { get => spell_Range_TicDMG;}
    public float Spell_Multy_EA { get => spell_Multy_EA;}
    public float Spell_Multy_Radius { get => spell_Multy_Radius;}
    public float Spell_Range_Area { get => spell_Range_Area;}
    public float Spell_ProjectileDelay { get => spell_ProjectileDelay;}
    public string Spell_Name { get => spell_Name;}
}
