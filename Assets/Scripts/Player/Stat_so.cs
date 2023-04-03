using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultStat", menuName = "Scriptable Object/Stat", order = int.MaxValue)]
public class Stat_so : ScriptableObject
{
    [SerializeField] private float hp;
    [SerializeField] private float speed;
    [SerializeField] private Buff buff;

    public float Hp { get => hp; set => hp = value; }
    public float Speed { get => speed; set => speed = value; }
    
    public Stat_so(Stat stat)
    {
        this.hp = stat.Hp;
        this.speed = stat.Speed;
    }

    public enum Buff
    {
        buff1,
        buff2,
        buff3
    }

    
}
