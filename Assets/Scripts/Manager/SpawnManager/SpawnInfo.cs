using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnInfo
{
    [SerializeField] public GameObject unit_prefab;
    [SerializeField] public string spawn_sort = "Point";
    [SerializeField] public int max;
    [SerializeField] public Vector2 point = Vector2.zero;
    [SerializeField] public List<Vector2> position = new List<Vector2>();
    [SerializeField] public float radius;
    [SerializeField] public float angle1;
    [SerializeField] public float angle2;
    [SerializeField] public float noise;
    [SerializeField] public int amount;
    [SerializeField] public float gap;
    

    [SerializeField] public int recty;
    public static List<string> spawn_sort_preset = new List<string>() { "Point", "Border", "List" ,"Area","Circle","Lines",};

    public SpawnInfo()
    {
        this.unit_prefab = null;
        this.spawn_sort = "Point";
        this.max = 0;
        this.point = Vector2.zero;
        this.position = new List<Vector2>();
        this.radius = 0;
        this.angle1 = 0;
        this.angle2 = 0;
        this.noise = 0;
        this.recty = 2;
        this.amount = 0;
        this.gap = 1;
    }
}
