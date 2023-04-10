using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "BuffSo", menuName = "Scriptable Object/Buff", order = int.MaxValue)]
public class Buff_SO : ScriptableObject
{
    [SerializeField]
    private string buff_name;
    
    [SerializeField]
    private BuffType buff_type;

    [SerializeField]
    private float buff_dration;
    [SerializeField]
    private float buff_currenttime;
    [SerializeField]
    private float buff_value;
    
    public enum BuffType
    {
        Buff_Hp,
        Buff_Mp,
        Buff_Armmor,
        Buff_Speed,
        Buff_AttackSpeed,
        Buff_AttackDamage
    };
    public BuffType Buff_Type { get => buff_type; }
    public string Buff_Name { get => buff_name; }
    //지속 시간
    public float Buff_duration { get => Buff_duration; }
    //현재 시간
    public float Buff_currentTime { get => buff_currenttime; }
    //버프 값
    public float Buff_value { get => buff_value; }
    
}
