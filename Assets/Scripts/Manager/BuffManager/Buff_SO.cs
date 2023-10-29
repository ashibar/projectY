using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "BuffSo", menuName = "Scriptable Object/Buff", order = int.MaxValue)]
public class Buff_SO : ScriptableObject
{
    [SerializeField] private string buff_name;              // ���� �̸�
    
    [SerializeField] private string buff_type;              // ���� Ÿ��

    [SerializeField] private float buff_duration;            // ���� �ð�
    [SerializeField] private float buff_tick;               // ƽ �ֱ�
    [SerializeField] private float buff_currenttime;        // ���� �ð�
    [SerializeField] private float buff_value;              // ���� ��

    public string Buff_Type { get => buff_type; }
    public string Buff_Name { get => buff_name; }    
    public float Buff_duration { get => buff_duration; }
    public float Buff_tick { get => buff_tick; set => buff_tick = value; }
    public float Buff_currentTime { get => buff_currenttime; }
    public float Buff_value { get => buff_value; }

    public static List<string> buff_type_preset = new List<string>()
    {
        "hp_buff",
        "mp_buff",
        "armmor_buff",
        "speed_buff",
        "cooldown_buff",
        "damage_buff",

        "burn_debuff",
        "poison_debuff",
        "slow_debuff",
        "silence_debuff",
        "entangle_debuff",
        "stun_debuff",
    };
}
