using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

/// <summary>
/// <para/><b>��� EventTimer ���</b>
/// <para/>����� : �̿��
/// <para/>��� : �̺�Ʈ�� �־��� ��� �ð� üũ �� ����
/// <para/>��� : 
/// <para/>������Ʈ ���� : 
/// <para/> - (23.08.22) : ��๮ ����
/// <para/> - (23.09.02) : Time���� ���� �� �۵� ����
/// <para/> - (23.09.15) : Move To Pos ����, Time���� ���� �� �����Ǵ� ���� ����
/// <para/> - (23.09.15) : Trigger, Number ���� �߰�
/// </summary>

public class EventTimer : MonoBehaviour, IEventListener
{
    [SerializeField] private UnitManager unitManager;
    [SerializeField] private List<EventPhase_so> phase = new List<EventPhase_so>();
    [SerializeField] private List<StringNTrigger> trigger = new List<StringNTrigger>();
    [SerializeField] private List<StringNNumber> number = new List<StringNNumber>();
    [SerializeField] private int eventIndex = 0;

    private List<CancellationTokenSource> cts = new List<CancellationTokenSource>();

    public static List<string> event_code = new List<string>
    {
        "Add New Trigger",
        "Remove Trigger",
        "Add New Number",
        "Remove Number",
        "Set Number",
        "Set Trigger",
    };

    private void Awake()
    {
        SubscribeEvent();
    }

    private void Start()
    {
        unitManager = UnitManager.Instance;
        //Debug.Log("?");
        //CTS(0);
    }

    public void SubscribeEvent()
    {
        foreach (string code in event_code)
            EventManager.Instance.AddListener(code, this);
    }

    public void OnEvent(string event_type, Component sender, Condition condition, params object[] param)
    {
        ExtraParams para = (ExtraParams)param[0];

        switch (event_type)
        {
            case "Add New Trigger":
                AddNewTrigger(para); break;
            case "Remove Trigger":
                RemoveTrigger(para); break;
            case "Add New Number":
                AddNewNumber(para); break;
            case "Remove Number":
                RemoveNumber(para); break;
            case "Set Number":
                SetNumber(para); break;
            case "Set Trigger":
                SetTrigger(para); break;
        }
    }

    private void AddNewTrigger(ExtraParams para)
    {
        Debug.Log(string.Format("{0}, {1}", para.Name, para.Boolvalue));
        if (!TriggerExists(para.Name))
            trigger.Add(new StringNTrigger(para.Name, para.Boolvalue));
        else
            SetTrigger(para);
    }

    private bool TriggerExists(string name)
    {
        foreach (StringNTrigger t in trigger)
            if (string.Equals(t.triggerName, name))
                return true;
        return false;
    }

    private void RemoveTrigger(ExtraParams para)
    {
        for (int i = trigger.Count - 1; i >= 0; i--)
        {
            if (string.Equals(trigger[i].triggerName, para.Name))
            {
                trigger.RemoveAt(i);
                return;
            }
        }
    }

    private void AddNewNumber(ExtraParams para)
    {
        Debug.Log(string.Format("{0}, {1}", para.Name, para.Floatvalue));
        number.Add(new StringNNumber(para.Name, para.Floatvalue));
    }

    private void RemoveNumber(ExtraParams para)
    {
        for (int i = number.Count - 1; i >= 0; i--)
        {
            if (string.Equals(number[i].numberName, para.Name))
            {
                number.RemoveAt(i);
                return;
            }
        }
    }

    private void SetNumber(ExtraParams para)
    {
        Debug.Log(string.Format("{0}, {1}", para.Name, para.Floatvalue));
        for (int i = number.Count - 1; i >= 0; i--)
        {
            if (string.Equals(number[i].numberName, para.Name))
            {
                number[i].numberValue = para.Floatvalue;
                return;
            }
        }
    }

    private void SetTrigger(ExtraParams para)
    {
        Debug.Log(string.Format("{0}, {1}", para.Name, para.Boolvalue));
        for (int i = trigger.Count - 1; i >= 0; i--)
        {
            if (string.Equals(trigger[i].triggerName, para.Name))
            {
                trigger[i].triggerValue = para.Boolvalue;
                return;
            }
        }
    }

    // async �Լ� Ż�� ��ū ����
    private async void CTS(int id)
    {
        cts.Add(new CancellationTokenSource());
        await Task_Function_(id, cts[id].Token);
    }

    private void Update()
    {
        //Async_Function();
        //CheckPosition();
        //Async_Function_();
    }
      

    //private async void Async_Function()
    //{
    //    // ���Ἲ
    //    if (para == null)
    //        return;

    //    // ������Ʈ
    //    if (!isCooltime && eventIndex < para.Count)
    //    {
    //        isCooltime = true;
    //        interruptedIndex = await Task_Function();
    //        isCooltime = false;
    //    }

    //    if (interruptedIndex != -1)
    //        Debug.Log("interruptedIndex = " + interruptedIndex.ToString());
    //}
    // �̺�Ʈ�� �޽��� �Է� �Լ�
    //private async Task<int> Task_Function()
    //{
                
    //    float end = Time.time + (float)para[eventIndex].condition.TargetNum;
    //    while (Time.time < end)
    //    {
    //        await Task.Yield();
    //        if (isInterrupted)
    //        {
    //            await Task.FromResult(0);
    //        }
    //    }
    //    Debug.Log(string.Format("{0} : {1}, {2}, {3}", eventIndex, para[eventIndex].eventcode, para[eventIndex].condition.Sort, (para[eventIndex].extraParams != null) ? para[eventIndex].extraParams : null));
    //    if (para[eventIndex].condition.Sort != ConditionSort.MoveToPos)
    //        PostNotification(para[eventIndex]);            
    //    else
    //        eventParam_pos.Add(eventParam_pos[eventIndex]);
        

    //    return -1;
    //}


    /// <summary>
    /// 
    /// </summary>
    /// <param name="no"></param>
    /// <param name="_cts"></param>
    /// <returns></returns>
    private async Task<int> Task_Function_(int no, CancellationToken _cts)
    {
        // ������ ���Ἲ
        while (no >= phase.Count)
        {
            await Task.Yield();
        }

        Debug.Log("?");
        List<EventParams> para = phase[no].Events;

        float start = Time.time;
        float accumulated;
        while (!_cts.IsCancellationRequested)
        {
            // �Ķ���� ���Ἲ
            if (para == null)
            {
                await Task.Yield();
                continue;
            }

            accumulated = 0;
            for (int i = 0; i < para.Count; i++)
            {
                EventParams p = para[i];
                // ���� �̺�Ʈ�� �����ø� ����Ǵ� �̺�Ʈ
                if (p.condition.IsContinued)
                {
                    // ���� �̺�Ʈ ���� ���� �˻�
                    if (i > 0 && !para[i - 1].condition.IsSatisfied)
                        continue;

                    // ���� �ð�
                    if (p.condition.Sort == ConditionSort.Time)
                    {
                        //Debug.Log(string.Format("{0}, {1}, {2}, {3}", Time.time, start, accumulated, p.condition.TargetNum));
                        accumulated += p.condition.TargetNum;
                        if (p.condition.IsSatisfied) continue;
                        if (Time.time - start >= accumulated)
                            p.condition.IsSatisfied = PostNotification(p);
                    }
                    else if (p.condition.Sort == ConditionSort.None)
                    {
                        accumulated += 0;
                        if (p.condition.IsSatisfied) continue;
                        if (Time.time - start >= accumulated)
                            p.condition.IsSatisfied = PostNotification(p);
                    }
                    // ��ġ
                    else if (p.condition.Sort == ConditionSort.MoveToPos)
                    {
                        if (p.condition.IsSatisfied) continue;
                        bool value = CheckPosition(p);
                        p.condition.IsSatisfied = value;
                        if (value)
                            start = Time.time;
                    }
                    else if (p.condition.Sort == ConditionSort.Trigger)
                    {
                        if (p.condition.IsSatisfied) continue;
                        bool value = CheckTrigger(p);
                        p.condition.IsSatisfied = value;
                        if (value)
                            start = Time.time;
                    }
                    else if (p.condition.Sort == ConditionSort.Number)
                    {
                        if (p.condition.IsSatisfied) continue;
                        bool value = CheckNumber(p);
                        p.condition.IsSatisfied = value;
                        if (value)
                            start = Time.time;
                    }
                }
                // �ٸ� �̺�Ʈ�� ���������� �۵��ϴ� �̺�Ʈ
                else
                {
                    // ���� �ð�
                    if (p.condition.Sort == ConditionSort.Time)
                    {
                        if (p.condition.IsSatisfied) continue;
                        if (Time.time - start >= p.condition.TargetNum)
                            p.condition.IsSatisfied = PostNotification(p);
                    }
                    else if (p.condition.Sort == ConditionSort.None)
                    {
                        p.condition.IsSatisfied = PostNotification(p);
                    }
                    else if (p.condition.Sort == ConditionSort.MoveToPos)
                    {
                        p.condition.IsSatisfied = CheckPosition(p);
                    }
                    else if (p.condition.Sort == ConditionSort.Trigger)
                    {
                        p.condition.IsSatisfied = CheckTrigger(p);
                    }
                    else if (p.condition.Sort == ConditionSort.Number)
                    {
                        p.condition.IsSatisfied = CheckNumber(p);
                    }
                }
            }

            
            await Task.Yield();
            
        }

        return -1;
    }

    private bool PostNotification(EventParams parm)
    {
        Debug.Log(string.Format("{0} : {1}, {2}, {3}", eventIndex, parm.eventcode, parm.condition.Sort, (parm.extraParams != null) ? parm.extraParams : null));
        EventManager.Instance.PostNotification(parm.eventcode, this, parm.condition, parm.extraParams);
        return true;
    }
        
    private bool CheckPosition(EventParams p)
    {
        if (unitManager == null) unitManager = UnitManager.Instance;
        if (unitManager.Clones == null) return false;
        List<GameObject> clones = unitManager.Clones;
        if (clones.Count == 0) return false;
        if (p.condition.IsSatisfied) return true;

        int num = 0;
        foreach (GameObject go in clones)
        {
            if (go == null) continue;
            if (!string.Equals(p.condition.TargetTag, go.tag)) continue;
            if (IsInsideRectangle(p.condition.TargetPos, p.condition.TargetRange, go.transform))
            {
                num++;
            }
        }
        if (num >= p.condition.TargetNum)
        {
            p.condition.IsSatisfied = true;
            Debug.Log(string.Format("{0} : {1}, {2}, {3}", eventIndex, p.eventcode, p.condition.Sort, (p.extraParams != null) ? p.extraParams : null));
            EventManager.Instance.PostNotification(p.eventcode, this, p.condition, p.extraParams);
            return true;
            //eventIndex++;
            //eventParam_pos.RemoveAt(i);
            //Debug.Log(i);
        }
        else
            return false;
    }

    // �Ű������� �޾ƿ� transformn�� ���簢�� �ȿ� �ִ��� ���θ� Ȯ��
    private bool IsInsideRectangle(Vector2 pos, Vector2 size, Transform objectTransform)
    {
        if (pos == null)
        {
            Debug.Log("pos is null");
            return false;
        }
        if (size == null)
        {
            Debug.Log("range is null");
            return false;
        }
        size = Vector2.Distance(size, Vector2.zero) <= 0 ? new Vector2(0.1f, 0.1f) : size;
        
        float leftBoundary = pos.x - (size.x / 2);
        float rightBoundary = pos.x + (size.x / 2);
        float bottomBoundary = pos.y - (size.y / 2);
        float topBoundary = pos.y + (size.y / 2);

        Vector3 objectPosition = objectTransform.position;

        if (objectPosition.x >= leftBoundary && objectPosition.x <= rightBoundary &&
            objectPosition.y >= bottomBoundary && objectPosition.y <= topBoundary)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CheckTrigger(EventParams p)
    {
        foreach (StringNTrigger t in trigger)
        {
            if (string.Equals(t.triggerName, p.condition.TargetFlag))
            {
                if (t.triggerValue == p.condition.FlagValue)
                {
                    Debug.Log(string.Format("{0} : {1}, {2}, {3}, {4}", eventIndex, p.eventcode, p.condition.Sort, p.condition.TargetFlag, p.condition.FlagValue));
                    EventManager.Instance.PostNotification(p.eventcode, this, p.condition, p.extraParams);
                    return true;
                }
            }                
        }
        return false;
    }

    public bool CheckTrigger(string triggerName, bool flagValue)
    {
        foreach (StringNTrigger t in trigger)
        {
            if (string.Equals(t.triggerName, triggerName))
            {
                if (t.triggerValue == flagValue)
                {
                    Debug.Log(string.Format("Custom CheckTrigger : {0}, {1}", triggerName, flagValue));
                    return true;
                }
            }
        }
        return false;
    }

    public bool CheckNumber(EventParams p)
    {
        foreach (StringNNumber n in number)
        {
            if (string.Equals(n.numberName, p.condition.TargetFlag))
            {
                if (p.condition.FlagValue)
                {
                    if (n.numberValue >= p.condition.TargetNum)
                    {
                        Debug.Log(string.Format("{0} : {1}, {2}, {3}, {4}", eventIndex, p.eventcode, p.condition.Sort, p.condition.TargetFlag, p.condition.TargetNum));
                        EventManager.Instance.PostNotification(p.eventcode, this, p.condition, p.extraParams);
                        return true;
                    }
                }
                else
                {
                    if (n.numberValue <= p.condition.TargetNum)
                    {
                        Debug.Log(string.Format("{0} : {1}, {2}, {3}, {4}", eventIndex, p.eventcode, p.condition.Sort, p.condition.TargetFlag, p.condition.TargetNum));
                        EventManager.Instance.PostNotification(p.eventcode, this, p.condition, p.extraParams);
                        return true;
                    }
                }
            }
        }
        return false;
    }

    // �ı��� ��� �̺�Ʈ �ߴ�
    private void OnDestroy()
    {
        foreach(CancellationTokenSource c in cts)
            c?.Cancel();
        //isInterrupted = true;
    }

    // ** �ܺ� ���� ��ũ��Ʈ **
    //public void SetPara(List<EventParams> para)
    //{
    //    this.para = para;
    //}

    //public void AddPara(EventParams para)
    //{
    //    this.para.Add(para);
    //}

    public void AddPhase(EventPhase_so phase)
    {
        EventPhase_so so = phase;
        foreach (EventParams p in so.Events)
            p.condition.IsSatisfied = false;
        this.phase.Add(phase);
        CTS(this.phase.Count - 1);
    }

    


    // ���� �ڵ�
    //private async void Async_Function()
    //{
    //    // ���Ἲ
    //    if (events == null)
    //        return;

    //    // ������Ʈ
    //    if (!isCooltime && eventIndex < events.Count)
    //    {
    //        isCooltime = true;
    //        interruptedIndex = await Task_Function();
    //        isCooltime = false;
    //    }

    //    if (interruptedIndex != -1)
    //        Debug.Log("interruptedIndex = " + interruptedIndex.ToString());
    //}
    //// �̺�Ʈ�� �޽��� �Է� �Լ�
    //private async Task<int> Task_Function()
    //{
    //    float end = Time.time + events[eventIndex].DurationToStart;
    //    while (Time.time < end)
    //    {
    //        if (events[eventIndex].Isinterrupted)
    //            await Task.FromResult(0);
    //        await Task.Yield();
    //    }
    //    Debug.Log(eventIndex + ", " + events[eventIndex].Id + ", " + events[eventIndex].DurationToStart);
    //    StageManager.Instance.messageBuffer.Add(events[eventIndex].Message);
    //    eventIndex++;

    //    return -1;
    //}

}
