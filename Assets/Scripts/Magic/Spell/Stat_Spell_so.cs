using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultStat_spell", menuName = "Scriptable Object/Stat_spell", order = int.MaxValue)]
public class Stat_Spell_so : ScriptableObject
{
    public enum SpellType { SOLE, MULTY, RANGE };
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
    [SerializeField] public SpellType spell_Type;           //����ü �߻� Ÿ��

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
