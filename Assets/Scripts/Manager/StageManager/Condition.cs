using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Condition
{
    [SerializeField] private ConditionSort sort;
    [SerializeField] private GameObject target;
    [SerializeField] private Vector2 targetPos;
    [SerializeField] private Transform targetArea;
    [SerializeField] private bool targetTrigger;
    [SerializeField] private bool targetString;
    [SerializeField] private float targetNum;

    public ConditionSort Sort { get => sort; set => sort = value; }
    public GameObject Target { get => target; set => target = value; }
    public Vector2 TargetPos { get => targetPos; set => targetPos = value; }
    public bool TargetTrigger { get => targetTrigger; set => targetTrigger = value; }
    public bool TargetString { get => targetString; set => targetString = value; }
}

public enum ConditionSort
{
    Trigger,
    MoveToPos,
    MoveToArea,
    String,
    targetNum
}
