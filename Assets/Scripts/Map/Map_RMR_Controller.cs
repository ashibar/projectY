using Mono.Cecil.Cil;
using ReadyMadeReality;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class Map_RMR_Controller : AsyncFunction_template, IEventListener
{
    [SerializeField] private MapPlayerControl mapPlayerControl;
    [SerializeField] private MapNodeSet nodeSet;
    [SerializeField] private List<MapNode> nodes = new List<MapNode>();

    [SerializeField] private List<StageInfo_so> stageInfoList = new List<StageInfo_so>();
    [SerializeField] private List<EventPhase_so> phase = new List<EventPhase_so>();
    [SerializeField] private List<StringNTrigger> trigger = new List<StringNTrigger>();

    [SerializeField] public int stageinfo_id = 0;

    private CancellationTokenSource cts = new CancellationTokenSource();

    private void Awake()
    {
        SubscribeEvent();
        stageinfo_id = LoadDataSingleton.Instance.PlayerInfoContainer().Progress_step;
        NodeAccessControl();
    }

    private void Start()
    {
        phase.AddRange(stageInfoList[stageinfo_id].Phases);
        for (int i = 0; i < phase.Count; i++)
            Start_Routine(i);        
    }

    public void OnEvent(string event_type, Component sender, Condition condition, params object[] param)
    {
        switch (event_type)
        {
            case "Set Actvie Map Player":
                SetActiveMapPlayer((ExtraParams)param[0]); break;
        }
    }

    public void SubscribeEvent()
    {
        EventManager.Instance.AddListener("Set Actvie Map Player", this);
    }

    private void SetActiveMapPlayer(ExtraParams para)
    {
        mapPlayerControl.isActive = para.Boolvalue;
    }

    public async void Start_Routine(int no)
    {
        foreach (EventParams p in phase[no].Events)
            p.condition.IsSatisfied = false;
        await Task_Function_(no, cts.Token);
    }

    private async Task<int> Task_Function_(int no, CancellationToken _cts)
    {
        // 페이즈 무결성
        while (no >= phase.Count && !_cts.IsCancellationRequested)
        {
            await Task.Yield();
        }

        Debug.Log("Phase Start");
        List<EventParams> para = phase[no].Events;

        float start = Time.time;
        float accumulated;
        while (!_cts.IsCancellationRequested)
        {
            // 파라미터 무결성
            if (para == null)
            {
                await Task.Yield();
                continue;
            }

            accumulated = 0;
            for (int i = 0; i < para.Count; i++)
            {
                EventParams p = para[i];
                // 이전 이벤트가 만족시만 실행되는 이벤트
                if (p.condition.IsContinued)
                {
                    // 이전 이벤트 만족 여부 검사
                    if (i > 0 && !para[i - 1].condition.IsSatisfied)
                        continue;

                    // 누적 시간
                    if (p.condition.Sort == ConditionSort.Time)
                    {
                        //Debug.Log(string.Format("{0}, {1}, {2}, {3}", Time.time, start, accumulated, p.condition.TargetNum));
                        accumulated += p.condition.TargetNum;
                        if (p.condition.IsSatisfied) continue;
                        if (Time.time - start >= accumulated)
                            p.condition.IsSatisfied = PostNotification(p);
                    }
                    else if (p.condition.Sort == ConditionSort.Trigger)
                    {
                        if (p.condition.IsSatisfied) continue;
                        bool value = CheckTrigger(p);
                        p.condition.IsSatisfied = value;
                        if (value)
                            start = Time.time;
                    }
                }
            }
            await Task.Yield();
        }

        return -1;
    }

    private bool PostNotification(EventParams parm)
    {
        //Debug.Log(string.Format("{0} : {1}, {2}, {3}", eventIndex, parm.eventcode, parm.condition.Sort, (parm.extraParams != null) ? parm.extraParams : null));
        EventManager.Instance.PostNotification(parm.eventcode, this, parm.condition, parm.extraParams);
        return true;
    }

    public bool CheckTrigger(EventParams p)
    {
        foreach (StringNTrigger t in trigger)
        {
            if (string.Equals(t.triggerName, p.condition.TargetFlag))
            {
                if (t.triggerValue == p.condition.FlagValue)
                {
                    //Debug.Log(string.Format("{0} : {1}, {2}, {3}, {4}", eventIndex, p.eventcode, p.condition.Sort, p.condition.TargetFlag, p.condition.FlagValue));
                    EventManager.Instance.PostNotification(p.eventcode, this, p.condition, p.extraParams);
                    return true;
                }
            }
        }
        return false;
    }

    private void NodeAccessControl()
    {
        if (nodeSet == null)
            return;

        nodes = new List<MapNode>(nodeSet.GetComponentsInChildren<MapNode>());

        switch (stageinfo_id)
        {
            case 0:
                nodeSet.startNode = nodes[1];
                break;
            case 1:
                nodeSet.startNode = nodes[1];
                nodes[2].isAccessable = true;
                break;
            case 2:
                nodeSet.startNode = nodes[2];
                nodes[2].isAccessable = true;
                nodes[4].isAccessable = true;
                break;
            case 3:
                nodeSet.startNode = nodes[4];
                nodes[2].isAccessable = true;
                nodes[4].isAccessable = true;
                nodes[5].isAccessable = true;
                break;
        }

        if (mapPlayerControl != null)
            mapPlayerControl.transform.position = nodeSet.startNode.transform.position;

    }

    private void OnDestroy()
    {
        cts?.Cancel();
    }

    
}
