using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ũ��Ʈ �̸� : Stat_so
/// ����� : �̿��
/// ��� : ������ ���� �⺻���� ���� ����, �ΰ��� ������ ���� �Ұ�
/// ��� : 
/// ������Ʈ ���� :
///     - (23.04.03) : �����̾� �����̹� ��� �⺻ ���� �߰�
/// </summary>

[CreateAssetMenu(fileName = "DefaultStat", menuName = "Scriptable Object/Stat", order = int.MaxValue)]
public class Stat_so : ScriptableObject
{
    [SerializeField] private int id;
    [SerializeField] private string name_unit;
    
    [SerializeField] private float hp;
    [SerializeField] private float speed;
    [SerializeField] private float hp_regen;
    [SerializeField] private float armor;

    [SerializeField] private float damage;
    [SerializeField] private float speed_projectile;
    [SerializeField] private float duration_projectile;
    [SerializeField] private float range_projectile;

    [SerializeField] private float cooldown;
    [SerializeField] private float amount;

    public int Id { get => id; set => id = value; }
    public string Name_unit { get => name_unit; set => name_unit = value; }
    public float Hp { get => hp; set => hp = value; }
    public float Speed { get => speed; set => speed = value; }
    public float Hp_regen { get => hp_regen; set => hp_regen = value; }
    public float Armor { get => armor; set => armor = value; }
    public float Damage { get => damage; set => damage = value; }
    public float Speed_projectile { get => speed_projectile; set => speed_projectile = value; }
    public float Duration_projectile { get => duration_projectile; set => duration_projectile = value; }
    public float Range_projectile { get => range_projectile; set => range_projectile = value; }
    public float Cooldown { get => cooldown; set => cooldown = value; }
    public float Amount { get => amount; set => amount = value; }
}
