using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Condition
{
    [SerializeField] private ConditionSort sort;
    [SerializeField] private Type target;
    [SerializeField] private string targetTag;
    [SerializeField] private Vector2 targetPos;
    [SerializeField] private int targetAreaID;
    [SerializeField] private string targetFlag;
    [SerializeField] private bool flagValue;
    [SerializeField] private float targetNum;
    [SerializeField] private bool isSatisfied;
    [SerializeField] private bool isContinued;

    public ConditionSort Sort { get => sort; set => sort = value; }
    public Type Target { get => target; set => target = value; }
    public string TargetTag { get => targetTag; set => targetTag = value; }
    public Vector2 TargetPos { get => targetPos; set => targetPos = value; }
    public int TargetAreaID { get => targetAreaID; set => targetAreaID = value; }
    public string TargetFlag { get => targetFlag; set => targetFlag = value; }
    public bool FlagValue { get => flagValue; set => flagValue = value; }
    public float TargetNum { get => targetNum; set => targetNum = value; }
    public bool IsSatisfied { get => isSatisfied; set => isSatisfied = value; }
    public bool IsContinued { get => isContinued; set => isContinued = value; }
}

public enum ConditionSort
{
    None,
    Time,
    Trigger,
    MoveToPos,
    MoveToArea,
    targetDestroy
}
