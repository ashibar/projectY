using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

/// <summary>
/// <para/><b>��� ConditionChecker ���</b>
/// <para/>����� : �̿��
/// <para/>��� : �̺�Ʈ�� �޾ƿ� ������ �����ϴ��� �Ǻ�
/// <para/>��� : 
/// <para/>������Ʈ ���� : 
/// <para/> - (23.08.22) : ��๮ ����
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
    /// <b>�̺�Ʈ�� ���� ������ ���� �з�</b><br/>
    /// <br/>
    /// None : �ƹ� ���� ���� Ÿ�̸� ��⿡ ����<br/>
    /// Trigger : �÷��׿� ������ Ÿ�̸� ��⿡ ����<br/>
    /// MoveToArea : ���ǿ����� Ư�� ������ �̵� �� Ÿ�̸� ��⿡ ����<br/>
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

    // ���� �ڵ�
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
    ///// <b>���ǿ� ������ �̺�Ʈ�� Ÿ�̸ӿ� ����</b>
    ///// </summary>
    ///// <param name="eventInfo"></param>
    //private void SendToTimer(EventInfo eventInfo)
    //{
    //    StageManager.Instance.EventTimer.Events.Add(eventInfo);
    //    events.Remove(eventInfo);
    //}

}
