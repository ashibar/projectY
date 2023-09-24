using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

/// <summary>
/// 스크립트 이름 : Stat
/// 담당자 : 이용욱
/// 요약 : 유닛의 스탯에 대한 정보
/// 비고 : 
/// 업데이트 내역 :
///     - (23.04.03) : 뱀파이어 서바이벌 기반 기본 스탯 추가
/// </summary>

[System.Serializable]
public class Stat
{
    // 패시브/버프등으로 변화될 수치

    [SerializeField] private float hp;                       // 최대 체력
    [SerializeField] private float speed;                    // 이동 속도
    [SerializeField] private float hp_regen;                 // 체력 리젠
    [SerializeField] private float armor;                    // 방어력

    [SerializeField] private float damage;                   // 공격력 배수
    [SerializeField] private float speed_projectile;         // 투사체 속도 배수
    [SerializeField] private float duration_projectile;      // 투사체 지속시간 배수
    [SerializeField] private float range_projectile;         // 투사체 범위 배수

    [SerializeField] private float cooldown;                 // 쿨다운 배수 (기본0)
    [SerializeField] private float amount;                   // 투사체 추가 수 (기본1)

    // 인게임 내에서 지속적으로 변화될 수치

    [SerializeField] private float hp_current;               // 현재 체력

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

    public static Stat operator +(Stat stat1, Stat stat2)
    {
        stat1.Hp += stat2.Hp;
        stat1.speed += stat2.Speed;
        stat1.Hp_regen += stat2.Hp_regen;
        stat1.Armor += stat2.Armor;
        stat1.Damage += stat2.Damage;
        stat1.Speed_projectile += stat2.Speed_projectile;
        stat1.Duration_projectile += stat2.Duration_projectile;
        stat1.Range_projectile += stat2.Range_projectile;
        stat1.Cooldown += stat2.Cooldown;
        stat1.Amount += stat2.Amount;

        stat1.Hp_current += stat2.Hp_current;

        return new Stat(stat1);
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
