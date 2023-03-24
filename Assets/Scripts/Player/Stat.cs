using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat
{
    private float hp;
    private float speed;


    public float Hp { get => hp; set => hp = value; }
    public float Speed { get => speed; set => speed = value; }
    
    public Stat(Stat_so stat)
    {
        this.hp = stat.Hp;
        this.speed = stat.Speed;
    }
}
