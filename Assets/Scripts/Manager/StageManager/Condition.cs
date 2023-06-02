using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Condition
{
    [SerializeField] private ConditionSort sort;
    [SerializeField] private Type target;
    [SerializeField] private Vector2 targetPos;
    [SerializeField] private int targetAreaID;
    [SerializeField] private string targetFlag;
    [SerializeField] private float targetNum;

    public ConditionSort Sort { get => sort; set => sort = value; }
    public Type Target { get => target; set => target = value; }
    public Vector2 TargetPos { get => targetPos; set => targetPos = value; }
    public int TargetAreaID { get => targetAreaID; set => targetAreaID = value; }
    public string TargetFlag { get => targetFlag; set => targetFlag = value; }
    public float TargetNum { get => targetNum; set => targetNum = value; }
}

public enum ConditionSort
{
    None,
    Trigger,
    MoveToPos,
    MoveToArea,
    targetDestroy,
    targetNum
}
