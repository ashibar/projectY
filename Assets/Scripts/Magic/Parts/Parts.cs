using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parts : MonoBehaviour
{
    public enum Parts_Sort
    {
        OnShot,
        OnUpdate,
        OnColide
    }
    public Parts_Sort sort;

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {

    }

    public virtual void Applier(GameObject proj, Stat_Spell stat, Collider2D collision)
    {

    }
}
