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

    public float Hp { get => hp; }
    public float Speed { get => speed; }
    public float Hp_regen { get => hp_regen; }
    public float Armor { get => armor; }
    public float Damage { get => damage; }
    public float Speed_projectile { get => speed_projectile; }
    public float Duration_projectile { get => duration_projectile; }
    public float Range_projectile { get => range_projectile; }
    public float Cooldown { get => cooldown; }
    public float Amount { get => amount; }

    
}
