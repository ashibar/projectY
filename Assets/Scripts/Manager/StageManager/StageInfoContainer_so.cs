using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageInfoContainer", menuName = "Scriptable Object/StageInfoContainer", order = int.MaxValue)]
public class StageInfoContainer_so : ScriptableObject
{
    [SerializeField] private int curID = 0;
    [SerializeField] private List<StageInfo_so> stageInfoList = new List<StageInfo_so>();

    public int CurID { get => curID; set => curID = value; }
    public List<StageInfo_so> StageInfoList { get => stageInfoList; set => stageInfoList = value; }
}
