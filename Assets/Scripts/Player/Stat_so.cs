using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 스크립트 이름 : Stat_so
/// 담당자 : 이용욱
/// 요약 : 유닛의 스탯 기본값에 대한 정보, 인게임 내에서 수정 불가
/// 비고 : 
/// 업데이트 내역 :
///     - (23.04.03) : 뱀파이어 서바이벌 기반 기본 스탯 추가
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
