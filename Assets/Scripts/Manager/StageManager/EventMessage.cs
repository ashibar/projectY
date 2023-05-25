using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class EventMessage
{
    [SerializeField] private int moduleID;
    [SerializeField] private string actionSTR;
    [SerializeField] private string targetSTR;
    [SerializeField] private float targetNUM;

    public int ModuleID { get => moduleID; set => moduleID = value; }
    public string ActionSTR { get => actionSTR; set => actionSTR = value; }
    public string TargetSTR { get => targetSTR; set => targetSTR = value; }
    public float TargetNUM { get => targetNUM; set => targetNUM = value; }

    public EventMessage(int moduleID, string actionID, string targetID, float targetNo)
    {
        this.moduleID = moduleID;
        this.actionSTR = actionID;
        this.targetSTR = targetID;
        this.targetNUM = targetNo;
    }
}
