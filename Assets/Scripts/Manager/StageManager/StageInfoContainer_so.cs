using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <b>��� StageInfoContainer ���</b> <br></br>
/// ����� : �̿�� <br></br>
/// ��� : Ư�� ����� ��� �������� ����Ʈ, ���� �������� ��ȣ <br></br>
/// ��� : �������� ���� ������ ���� �ֻ��� ���� (StageInfoContainer_so > StageInfo_so > EventPhase_so > EventParams > ExtraParams <br></br>
/// ������Ʈ ���� :  <br></br>
///  - (24.02.21) : ��๮ ����  <br></br>///                 
///  
///  <br></br>
/// </summary>
[CreateAssetMenu(fileName = "StageInfoContainer", menuName = "Scriptable Object/StageInfoContainer", order = int.MaxValue)]
public class StageInfoContainer_so : ScriptableObject
{
    [SerializeField] private int curID = 0;                                                     // �������� ��ȣ
    [SerializeField] private List<StageInfo_so> stageInfoList = new List<StageInfo_so>();       // �������� ����Ʈ

    public int CurID { get => curID; set => curID = value; }
    public List<StageInfo_so> StageInfoList { get => stageInfoList; set => stageInfoList = value; }
}
