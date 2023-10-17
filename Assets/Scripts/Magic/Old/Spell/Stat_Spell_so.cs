using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultStat_spell", menuName = "Scriptable Object/Stat_spell", order = int.MaxValue)]
public class Stat_Spell_so : ScriptableObject
{
    [SerializeField] private SpellType spell_Type;           //���� Ÿ��
    [SerializeField] private string spell_Name;              //���� �̸�
    [SerializeField] private string spell_Code;              //���� �ڵ�

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
    [SerializeField] private float spell_Amount_Tic = 1;     //ƽ�� ��ȣ�ۿ��ϴ� ������ ����

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
