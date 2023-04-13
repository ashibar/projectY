using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ũ��Ʈ �̸� : Stat
/// ����� : �̿��
/// ��� : ������ ���ȿ� ���� ����
/// ��� : 
/// ������Ʈ ���� :
///     - (23.04.03) : �����̾� �����̹� ��� �⺻ ���� �߰�
/// </summary>

[System.Serializable]
public class Stat
{
    // �нú�/���������� ��ȭ�� ��ġ

    private float hp;                       // �ִ� ü��
    private float speed;                    // �̵� �ӵ�
    private float hp_regen;                 // ü�� ����
    private float armor;                    // ����

    private float damage;                   // ���ݷ� ���
    private float speed_projectile;         // ����ü �ӵ� ���
    private float duration_projectile;      // ����ü ���ӽð� ���
    private float range_projectile;         // ����ü ���� ���

    private float cooldown;                 // ��ٿ� ��� (�⺻0)
    private float amount;                   // ����ü �߰� �� (�⺻1)

    // �ΰ��� ������ ���������� ��ȭ�� ��ġ

    private float hp_current;               // ���� ü��

    public Stat(Stat_so stat)
    {
        this.hp = stat.Hp;
        this.speed = stat.Speed;
        this.hp_regen = stat.Hp_regen;
        this.armor = stat.Armor;
        this.damage = stat.Damage;
        this.speed_projectile = stat.Speed_projectile;
        this.duration_projectile = stat.Duration_projectile;
        this.range_projectile = stat.Range_projectile;
        this.cooldown = stat.Cooldown;
        this.amount = stat.Amount;

        this.hp_current = stat.Hp;
    }

    public Stat(Stat stat)
    {
        this.hp = stat.Hp;
        this.speed = stat.Speed;
        this.hp_regen = stat.Hp_regen;
        this.armor = stat.Armor;
        this.damage = stat.Damage;
        this.speed_projectile = stat.Speed_projectile;
        this.duration_projectile = stat.Duration_projectile;
        this.range_projectile = stat.Range_projectile;
        this.cooldown = stat.Cooldown;
        this.amount = stat.Amount;

        this.hp_current = stat.Hp;
    }

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
    
    public float Hp_current { get => hp_current; set => hp_current = value; }
}
