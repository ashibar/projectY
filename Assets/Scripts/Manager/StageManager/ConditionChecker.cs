using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

/// <summary>
/// <para/><b>■■ ConditionChecker ■■</b>
/// <para/>담당자 : 이용욱
/// <para/>요약 : 이벤트를 받아와 조건이 만족하는지 판별
/// <para/>비고 : 
/// <para/>업데이트 내역 : 
/// <para/> - (23.08.22) : 요약문 생성
/// <para/>
/// </summary>

public class ConditionChecker : MonoBehaviour, IEventListener
{
    [SerializeField] private List<EventParams> para = new List<EventParams>();
    [SerializeField] private List<ConditionArea> areas;
    [SerializeField] private List<EventParams> para_trigger = new List<EventParams>();
    [SerializeField] private List<EventParams> para_area = new List<EventParams>();
    [SerializeField] private Dictionary<string, bool> flag = new Dictionary<string, bool>();

    private void Awake()
    {
        areas.AddRange(GetComponentsInChildren<ConditionArea>());
        SubscribeEvent();
    }

    private void Update()
    {
        //ConditionCheck();
        //ConditionCheck();

    }

    public void SubscribeEvent()
    {
        EventManager.Instance.AddListener(EventCode.UnitArrived, this);
    }

    public void OnEvent(EventCode event_type, Component sender, Condition condition, params object[] param)
    {
        
        
        switch (event_type)
        {
            case EventCode.UnitArrived:
                UnitArrived(param); break;
        }
    }

    private void UnitArrived(params object[] param)
    {
        
    }

    /// <summary>
    /// <b>이벤트의 조건 종류에 따라 분류</b><br/>
    /// <br/>
    /// None : 아무 조건 없이 타이머 모듈에 전송<br/>
    /// Trigger : 플래그에 만족시 타이머 모듈에 전송<br/>
    /// MoveToArea : 조건영역에 특정 유닛이 이동 시 타이머 모듈에 전송<br/>
    /// </summary>
    //private void ConditionCheck()
    //{
    //    para.Reverse();
    //    for (int i = para.Count - 1; i >= 0; i--)
    //    {
    //        EventParams e = para[i];
    //        switch (para[i].condition.Sort)
    //        {
    //            case ConditionSort.None:
    //                StageManager.Instance.EventTimer.AddPara(e);
    //                para.Remove(e);
    //                break;
    //            case ConditionSort.Time:
    //                StageManager.Instance.EventTimer.AddPara(e);
    //                para.Remove(e);
    //                break;
    //            case ConditionSort.Trigger:
    //                para_trigger.Add(e);
    //                para.Remove(e);
    //                break;
    //            case ConditionSort.MoveToArea:
    //                EventManager.Instance.PostNotification(EventCode.RegisterPosSearch, this, new Condition(), para[i]);
    //                //para_area.Add(e);
    //                para.Remove(e);
    //                break;
    //        }
    //    }

        //foreach (EventParams e in para)
        //{
        //    switch (e.condition.Sort)
        //    {
        //        case ConditionSort.None:
        //            StageManager.Instance.EventTimer.AddPara(e);
        //            para.Remove(e);
        //            return;
        //        case ConditionSort.Trigger:
        //            break;
        //        case ConditionSort.MoveToArea:
        //            break;
        //    }
        //}
    //}

    public void SetPara(List<EventParams> para)
    {
        this.para.Clear();
        this.para.AddRange(para);
    }

    // 더미 코드
    //private void ConditionCheck()
    //{
    //    foreach (EventInfo e in events)
    //    {
    //        switch (e.Condition.Sort)
    //        {
    //            case ConditionSort.None:
    //                SendToTimer(e);
    //                return;
    //            case ConditionSort.Trigger:
    //                foreach (string f in flags)
    //                {
    //                    if (string.Equals(f, e.Condition.TargetFlag))
    //                    {
    //                        flags.Remove(f);
    //                        SendToTimer(e);
    //                        return;
    //                    }
    //                }
    //                break;
    //            case ConditionSort.MoveToArea:
    //                if (areas[e.Condition.TargetAreaID].TargetExists)
    //                {
    //                    SendToTimer(e);
    //                    return;
    //                }
    //                break;
    //        }
    //    }

    //}

    //[SerializeField] private List<EventInfo> events;
    //public List<EventInfo> Events { get => events; set => events = value; }
    //
    ///// <summary>
    ///// <b>조건에 만족한 이벤트를 타이머에 전송</b>
    ///// </summary>
    ///// <param name="eventInfo"></param>
    //private void SendToTimer(EventInfo eventInfo)
    //{
    //    StageManager.Instance.EventTimer.Events.Add(eventInfo);
    //    events.Remove(eventInfo);
    //}

}
