using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EventMessage
{
    [SerializeField] private int moduleID;
    [SerializeField] private string actionSTR;
    [SerializeField] private string targetSTR;
    [SerializeField] private float targetNUM;
    [SerializeField] private Vector2 targetPOS;

    public int ModuleID { get => moduleID; set => moduleID = value; }
    public string ActionSTR { get => actionSTR; set => actionSTR = value; }
    public string TargetSTR { get => targetSTR; set => targetSTR = value; }
    public float TargetNUM { get => targetNUM; set => targetNUM = value; }
    public Vector2 TargetPOS { get => targetPOS; set => targetPOS = value; }

    public EventMessage()
    {

    }
    public EventMessage(int moduleID, string actionID, string targetID, float targetNo, Vector2 targetPOS)
    {
        this.moduleID = moduleID;
        this.actionSTR = actionID;
        this.targetSTR = targetID;
        this.targetNUM = targetNo;
        this.targetPOS = targetPOS;
    }
}
