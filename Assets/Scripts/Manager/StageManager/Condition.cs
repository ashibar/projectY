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
    [SerializeField] private string targetString;
    [SerializeField] private float targetNum;

    public ConditionSort Sort { get => sort; set => sort = value; }
    public GameObject Target { get => target; set => target = value; }
    public Vector2 TargetPos { get => targetPos; set => targetPos = value; }
    public bool TargetTrigger { get => targetTrigger; set => targetTrigger = value; }
    public string TargetString { get => targetString; set => targetString = value; }

    public bool CheckCondition(object value)
    {
        switch (sort)
        {
            case ConditionSort.Trigger:
                if (TargetTrigger) {
                    TargetTrigger = false;
                    return true;
                }
                else
                    return false;
            case ConditionSort.MoveToPos:
                if ((Vector2)target.transform.position == targetPos)
                    return true;
                else
                    return false;
            case ConditionSort.String:
                string s = value.ToString();
                if (TargetString == s)
                    return true;
                else
                    return false;
            case ConditionSort.targetNum:
                int i = (int)value;
                if (targetNum == i)
                    return true;
                else
                    return false;
        }

        return false;
    }
}

public enum ConditionSort
{
    Trigger,
    MoveToPos,
    MoveToArea,
    String,
    targetNum
}
