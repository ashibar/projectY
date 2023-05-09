using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Applier_parameter
{
    private GameObject proj;
    private Stat_Spell stat;
    private Collider2D collision;
    private Vector2 dir_toMove = new Vector2();
    private Vector2 dir_toShoot = new Vector2();
    private Vector2 pos_toShoot = new Vector2();
    private Unit owner;
    private string target_tag;
    private ShotManage generater;

    public GameObject Proj { get => proj; set => proj = value; }
    public Stat_Spell Stat { get => stat; set => stat = value; }
    public Collider2D Collision { get => collision; set => collision = value; }
    public Vector2 Dir_toMove { get => dir_toMove; set => dir_toMove = value; }
    public Vector2 Dir_toShoot { get => dir_toShoot; set => dir_toShoot = value; }
    public Vector2 Pos_toShoot { get => pos_toShoot; set => pos_toShoot = value; }
    public Unit Owner { get => owner; set => owner = value; }
    public string Target_tag { get => target_tag; set => target_tag = value; }
    public ShotManage Generater { get => generater; set => generater = value; }

    public Applier_parameter()
    {
        this.proj = null;
        this.stat = null;
        this.collision = null;
        this.dir_toMove = Vector2.zero;
        this.dir_toShoot = Vector2.zero;
        this.pos_toShoot = Vector2.zero;
    }
    public Applier_parameter(GameObject proj, Stat_Spell stat)
    {
        this.proj = proj;
        this.stat = stat;
        this.collision = null;
        this.dir_toMove = Vector2.zero;
        this.dir_toShoot = Vector2.zero;
        this.pos_toShoot = Vector2.zero;
    }
    public Applier_parameter(GameObject proj, Stat_Spell stat, Collider2D collision)
    {
        this.proj = proj;
        this.stat = stat;
        this.collision = collision;
        this.dir_toMove = Vector2.zero;
        this.dir_toShoot = Vector2.zero;
        this.pos_toShoot = Vector2.zero;
    }
    public Applier_parameter(GameObject proj, Stat_Spell stat, Collider2D collision, Vector2 dir_toMove, Vector2 dir_toShoot, Vector2 pos_toShoot)
    {
        this.proj = proj;
        this.stat = stat;
        this.collision = collision;
        this.dir_toMove = dir_toMove;
        this.dir_toShoot = dir_toShoot;
        this.pos_toShoot = pos_toShoot;
    }
    public Applier_parameter(Applier_parameter para)
    {
        this.proj = para.Proj;
        this.stat = para.Stat;
        this.collision = para.Collision;
        this.dir_toMove = para.Dir_toMove;
        this.dir_toShoot = para.Dir_toShoot;
        this.pos_toShoot = para.pos_toShoot;
    }

}
