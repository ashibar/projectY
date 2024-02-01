using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <b>■■ StageInfoContainer ■■</b> <br></br>
/// 담당자 : 이용욱 <br></br>
/// 요약 : 특정 모드의 모든 스테이지 리스트, 현재 스테이지 번호 <br></br>
/// 비고 : 스테이지 관련 에셋중 가장 최상위 범주 (StageInfoContainer_so > StageInfo_so > EventPhase_so > EventParams > ExtraParams <br></br>
/// 업데이트 내역 :  <br></br>
///  - (24.02.21) : 요약문 생성  <br></br>///                 
///  
///  <br></br>
/// </summary>
[CreateAssetMenu(fileName = "StageInfoContainer", menuName = "Scriptable Object/StageInfoContainer", order = int.MaxValue)]
public class StageInfoContainer_so : ScriptableObject
{
    [SerializeField] private int curID = 0;                                                     // 스테이지 번호
    [SerializeField] private List<StageInfo_so> stageInfoList = new List<StageInfo_so>();       // 스테이지 리스트

    public int CurID { get => curID; set => curID = value; }
    public List<StageInfo_so> StageInfoList { get => stageInfoList; set => stageInfoList = value; }
}
