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
    [SerializeField] private List<EventPhase_so> phases = new List<EventPhase_so>();
    [SerializeField] private List<StringNTrigger> trigger = new List<StringNTrigger>();
    [SerializeField] private List<StringNNumber> number = new List<StringNNumber>();
    [SerializeField] private int eventIndex = 0;

    [SerializeField] private bool verbose;

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

    /// <summary>
    /// <b>��ũ��Ʈ ù Ȱ��ȭ �� ���� ���� ����Ǵ� �Լ�.</b><br></br>
    /// <br></br>
    /// - �̺�Ʈ ���� <br></br>
    /// </summary>
    private void Awake()
    {
        SubscribeEvent();
    }

    /// <summary>
    /// <b>��ũ��Ʈ ù Ȱ��ȭ �� �ι�°�� ����Ǵ� �Լ�.</b><br></br>
    /// - UnitManager ��� �ε� <br></br>
    /// 
    /// </summary>
    private void Start()
    {
        unitManager = UnitManager.Instance;
        //Debug.Log("?");
        //CTS(0);
    }

    /// <summary>
    /// �̺�Ʈ ����
    /// </summary>
    public void SubscribeEvent()
    {
        foreach (string code in event_code)
            EventManager.Instance.AddListener(code, this);
    }

    /// <summary>
    /// �̺�Ʈ �Ŵ����� ����ϴ� �̺�Ʈ�� ���� �����ϴ� �Լ�
    /// </summary>
    /// <param name="event_type">�̺�Ʈ �ڵ�</param>
    /// <param name="sender">�۽���</param>
    /// <param name="condition">����</param>
    /// <param name="param">�Ű����� ����</param>
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

    /// <summary>
    /// <b>[OnEvent] ���ο� Ʈ���Ÿ� ���</b> <br></br>
    /// - Name : ����� Ʈ���� �̸�
    /// - Bool : Ʈ������ true/false ����
    /// </summary>
    /// <param name="para"></param>
    private void AddNewTrigger(ExtraParams para)
    {
        if (verbose)
            Debug.Log(string.Format("{0}, {1}", para.Name, para.Boolvalue));
        
        // �ߺ��� �ƴ϶�� ���
        if (!TriggerExists(para.Name))
            trigger.Add(new StringNTrigger(para.Name, para.Boolvalue));
        // �ߺ��̸� bool���� ����
        else
            SetTrigger(para);
    }

    /// <summary>
    /// <b>Ʈ���� ����Ʈ���� Ʈ���Ű� �����ϴ��� �˻�</b>
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    private bool TriggerExists(string name)
    {
        foreach (StringNTrigger t in trigger)
            if (string.Equals(t.triggerName, name))
                return true;
        return false;
    }

    /// <summary>
    /// <b>[OnEvent] Ʈ���� ����</b>
    /// - Name : ������ Ʈ���� �̸�
    /// </summary>
    /// <param name="para"></param>
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

    /// <summary>
    /// <b>[OnEvent] ���ο� Number���� ���</b> <br></br>
    /// - Name : ����� ���� �̸�
    /// - Float : ��ϵ� ������ ��ġ
    /// </summary>
    /// <param name="para"></param>
    private void AddNewNumber(ExtraParams para)
    {
        if (verbose)
            Debug.Log(string.Format("{0}, {1}", para.Name, para.Floatvalue));
        number.Add(new StringNNumber(para.Name, para.Floatvalue));
    }

    /// <summary>
    /// <b>[OnEvent] ��ġ ���� ����</b> <br></br>
    /// - Name : ������ ���� �̸�
    /// </summary>
    /// <param name="para"></param>
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

    /// <summary>
    /// <b>[OnEvent] ��ġ ������ ��ġ ����</b> <br></br>
    /// - Name : ������ ��ġ ���� �̸�
    /// - Float : ������ ��ġ ������ ��ġ
    /// </summary>
    /// <param name="para"></param>
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

    /// <summary>
    /// <b>[OnEvent] Ʈ���� ������ �� ����</b> <br></br>
    /// - Name : ���� ������ Ʈ���� ���� �̸�
    /// - Bool : �� ������ ����� ��
    /// </summary>
    /// <param name="para"></param>
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

    /// <summary>
    /// <b>EventPhase�� ����Ͽ� �����ƾ�� ����</b>
    /// </summary>
    /// <param name="id"></param>
    private async void CreatePhaseSubroutine(int id)
    {
        cts.Add(new CancellationTokenSource());
        await PhaseSubroutine(id, cts[id].Token);
    }

    /// <summary>
    /// <b>phase�� �а� �̺�Ʈ�� �����Ű�� �����ƾ</b>
    /// </summary>
    /// <param name="no"></param>
    /// <param name="_cts"></param>
    /// <returns></returns>
    private async Task<int> PhaseSubroutine(int no, CancellationToken _cts)
    {
        // ������ ���Ἲ
        while (no >= phases.Count)
        {
            if (_cts.IsCancellationRequested)
                break;
            await Task.Yield();
        }

        if (verbose)
        {
            Debug.Log(string.Format("Phase {0} subroutine Activated", no));
        }        
        List<EventParams> eventList = phases[no].Events;

        float start = Time.time;    // ���� �ð�
        float accumulated;          // ���� �ð�
        while (!_cts.IsCancellationRequested)
        {
            // �̺�Ʈ ���Ἲ
            if (eventList == null)
            {
                if (verbose)
                {
                    Debug.Log(string.Format("No event list found, Phase {0} inActivated", no));
                }
                break;
            }

            // ���� �ð� �ʱ�ȭ
            accumulated = 0;


            // ���� �˻� �� �̺�Ʈ ����
            for (int i = 0; i < eventList.Count; i++)
            {
                EventParams p = eventList[i];
                
                //if (p.condition.IsContinued)            // ���� �̺�Ʈ�� �����ø� ����Ǵ� �̺�Ʈ
                //{
                //    // ���� �̺�Ʈ ���� ���� �˻�
                //    if (i > 0 && !eventList[i - 1].condition.IsSatisfied)
                //        continue;

                //    // ���ǿ� ���� ���� �̺�Ʈ ó��, �������� ���� ��� �ٽ� �ݺ�
                //    bool isPass = ContinuousEventProcess(p, ref start, ref accumulated);
                //    if (!isPass) continue;
                //}
                if (true)            // ���� �̺�Ʈ�� �����ø� ����Ǵ� �̺�Ʈ
                {
                    // ���� �̺�Ʈ ���� ���� �˻�
                    if (i > 0 && !eventList[i - 1].isSatisfied)
                        continue;

                    // ���ǿ� ���� ���� �̺�Ʈ ó��, �������� ���� ��� �ٽ� �ݺ�
                    bool isPass = ContinuousEventProcess_(p, ref start, ref accumulated);
                    if (!isPass) continue;
                }
                //else                                    // �ٸ� �̺�Ʈ�� ���������� �۵��ϴ� �̺�Ʈ
                //{
                //    bool isPass = IndependentEventProcess(p, ref start);
                //    if (!isPass) continue;
                //}
            }


            // �̺�Ʈ���� ��� ������ �Ϸ�Ǹ� Phase�� ����
            //if (no < phases.Count)
            //{
            //    bool allCheck = true;
            //    foreach (EventParams p in phases[no].Events)
            //    {
            //        if (p.condition.IsSatisfied == false)
            //            allCheck = false;
            //    }
            //    if (allCheck)
            //    {
            //        if (verbose)
            //        {
            //            Debug.Log(string.Format("All Events of Phase {0} satisfied. Phase {0} inactivated", no));
            //        }
            //        break;
            //    }                    
            //}
            if (no < phases.Count)
            {
                bool allCheck = true;
                foreach (EventParams p in phases[no].Events)
                {
                    if (p.isSatisfied == false)
                        allCheck = false;
                }
                if (allCheck)
                {
                    if (verbose)
                    {
                        Debug.Log(string.Format("All Events of Phase {0} satisfied. Phase {0} inactivated", no));
                    }
                    break;
                }
            }

            await Task.Yield();
            
        }

        return -1;
    }

    /// <summary>
    /// <b>���������� ����Ǵ� �̺�Ʈ�鿡 ���� ó��</b>
    /// </summary>
    /// <param name="p">�̺�Ʈ</param>
    /// <param name="start">���� �ð�</param>
    /// <param name="accumulated">���� �ð�</param>
    /// <returns></returns>
    private bool ContinuousEventProcess(EventParams p, ref float start, ref float accumulated)
    {
        bool check;
        // ���� �ð�
        switch (p.condition.Sort)
        {
            // �ð�
            case ConditionSort.Time:                                
                accumulated += p.condition.TargetNum;
                if (p.condition.IsSatisfied)
                    return false;
                if (Time.time - start >= accumulated)
                    p.condition.IsSatisfied = PostNotification(p);
                return true;
            
            // ���Ǿ���(0��)
            case ConditionSort.None:
                accumulated += 0;
                if (p.condition.IsSatisfied)
                    return false;
                if (Time.time - start >= accumulated)
                    p.condition.IsSatisfied = PostNotification(p);
                return true;
            
            // Ư�� ��ġ �̵�
            case ConditionSort.MoveToPos:
                if (p.condition.IsSatisfied)
                    return false;
                bool value = CheckPosition(p);
                p.condition.IsSatisfied = value;
                if (value)
                    start = Time.time;
                return true;
            
            // Trigger Ȯ��
            case ConditionSort.Trigger:
                if (p.condition.IsSatisfied)
                    return false;
                check = CheckTrigger(p);
                p.condition.IsSatisfied = check;
                if (check)
                    start = Time.time;
                return true;
            
            // ���� Ȯ��
            case ConditionSort.Number:
                if (p.condition.IsSatisfied)
                    return false;
                check = CheckNumber(p);
                p.condition.IsSatisfied = check;
                if (check)
                    start = Time.time;
                return true;
            default:
                return false;
        }        
    }

    private bool ContinuousEventProcess_(EventParams p, ref float start, ref float accumulated)
    {
        // �̹� ������ 
        
        List<Condition> conditions = p.conditions;
        // ���� �Ѱ��� �������� ���� �� false
        bool result = true;
        bool isTime = false;
        for (int i = 0; i < conditions.Count; i++)
        {
            // ���� �ð�
            switch (conditions[i].Sort)
            {
                // �ð�
                case ConditionSort.Time:
                    accumulated += conditions[i].TargetNum;
                    if (p.isSatisfied)
                        return false;
                    if (Time.time - start < accumulated)
                        result = false;
                    isTime = true;
                    break;
                // ���Ǿ���(0��)
                case ConditionSort.None:
                    accumulated += 0;
                    if (p.isSatisfied)
                        return false;
                    if (Time.time - start < accumulated)
                        result = false;
                    isTime = true;
                    break;
                // Ư�� ��ġ �̵�
                case ConditionSort.MoveToPos:
                    if (p.isSatisfied)
                        return false;
                    if (!CheckPosition(conditions[i]))
                        result = false;
                    break;
                // Trigger Ȯ��
                case ConditionSort.Trigger:
                    if (p.isSatisfied)
                        return false;
                    if (!CheckTrigger(conditions[i]))
                        result = false;
                    break;
                // ���� Ȯ��
                case ConditionSort.Number:
                    if (p.isSatisfied)
                        return false;
                    if (!CheckNumber(conditions[i]))
                        result = false;                   
                    break;
                default:
                    break;
            }
        }

        if (!isTime)
            start = Time.time;

        if (result)
        {
            p.isSatisfied = PostNotification(p);
            return true;
        }            
        else
            return false;
    }

    /// <summary>
    /// <b>���������� ����Ǵ� �̺�Ʈ�鿡 ���� ó��</b>
    /// </summary>
    /// <param name="p"></param>
    /// <param name="start"></param>
    /// <returns></returns>
    private bool IndependentEventProcess(EventParams p, ref float start)
    {
        switch (p.condition.Sort)
        {
            case ConditionSort.Time:
                if (p.condition.IsSatisfied)
                    return false;
                if (Time.time - start >= p.condition.TargetNum)
                    p.condition.IsSatisfied = PostNotification(p);
                return true;
            case ConditionSort.None:
                p.condition.IsSatisfied = PostNotification(p);
                return true;
            case ConditionSort.MoveToPos:
                p.condition.IsSatisfied = CheckPosition(p);
                return true;
            case ConditionSort.Trigger:
                p.condition.IsSatisfied = CheckTrigger(p);
                return true;
            case ConditionSort.Number:
                p.condition.IsSatisfied = CheckNumber(p);
                return true;
            default:
                return false;
        }
    }

    /// <summary>
    /// <b>������ �����Ǿ��� EventManager�� Event�� �����</b>
    /// </summary>
    /// <param name="parm"></param>
    /// <returns></returns>
    private bool PostNotification(EventParams parm)
    {
        if (verbose)
        {
            Debug.Log(string.Format("Event ID {0} : code = {1}, condiiton sort = {2}, extra params = {3}",
                parm.no, parm.eventcode, parm.condition.Sort, (parm.extraParams != null) ? parm.extraParams : null));
        }        
        EventManager.Instance.PostNotification(parm.eventcode, this, parm.condition, parm.extraParams);
        return true;
    }
        
    /// <summary>
    /// <b>Ư�� ���� ���� Ư�� Ÿ���� ������ � �����ϴ��� �˻�</b>
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    private bool CheckPosition(EventParams p)
    {
        // ���� üũ�� ���� UnitManager ��� ���Ἲ �˻�
        if (unitManager == null) unitManager = UnitManager.Instance;

        // UnitManager ���� Clones ����Ʈ ���Ἲ �˻�
        if (unitManager.Clones == null) return false;        
        List<GameObject> clones = unitManager.Clones;
        if (clones.Count == 0) return false;

        // �̺�Ʈ�� �̹� ������ ������ ���¶�� true ��ȯ
        if (p.condition.IsSatisfied) return true;


        // clone ����Ʈ���� �±׿� ���� Ư�� ��ġ�� Ư�� ���� ���� ��ġ�ϴ� ������ ������ ����
        // p.condition.TargetTag�� ã�ƾ��ϴ� Ÿ���� �±�
        int num = 0;
        foreach (GameObject go in clones)
        {
            if (go == null) continue;
            if (!string.Equals(p.condition.TargetTag, go.tag)) continue;
            
            // ������ �����ϴ��� �˻�
            if (IsInsideRectangle(p.condition.TargetPos, p.condition.TargetRange, go.transform))
            {
                num++;
            }
        }

        // ������ ������ ��ǥġ �̻����� �˻�
        // p.condition.TargetNum�� ��ǥġ
        if (num >= p.condition.TargetNum)
        {
            p.condition.IsSatisfied = true;
            return PostNotification(p);
        }
        else
            return false;
    }

    private bool CheckPosition(Condition c)
    {
        // ���� üũ�� ���� UnitManager ��� ���Ἲ �˻�
        if (unitManager == null) unitManager = UnitManager.Instance;

        // UnitManager ���� Clones ����Ʈ ���Ἲ �˻�
        if (unitManager.Clones == null) return false;
        List<GameObject> clones = unitManager.Clones;
        if (clones.Count == 0) return false;


        // clone ����Ʈ���� �±׿� ���� Ư�� ��ġ�� Ư�� ���� ���� ��ġ�ϴ� ������ ������ ����
        // p.condition.TargetTag�� ã�ƾ��ϴ� Ÿ���� �±�
        int num = 0;
        foreach (GameObject go in clones)
        {
            if (go == null) continue;
            if (!string.Equals(c.TargetTag, go.tag)) continue;

            // ������ �����ϴ��� �˻�
            if (IsInsideRectangle(c.TargetPos, c.TargetRange, go.transform))
            {
                num++;
            }
        }

        // ������ ������ ��ǥġ �̻����� �˻�
        // p.condition.TargetNum�� ��ǥġ
        if (num >= c.TargetNum)
        {
            return true;
        }
        else
            return false;
    }

    /// <summary>
    /// <b>�Ű������� �޾ƿ� transform�� ���簢�� �ȿ� �ִ��� ���θ� Ȯ��</b>
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="size"></param>
    /// <param name="objectTransform"></param>
    /// <returns></returns>
    private bool IsInsideRectangle(Vector2 pos, Vector2 size, Transform objectTransform)
    {
        // ���Ἲ �˻�
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
        
        // �ּ� ���� ���� (x,y 0.1 ������ ���簢��)
        size = Vector2.Distance(size, Vector2.zero) <= 0 ? new Vector2(0.1f, 0.1f) : size;
        
        // ���
        float leftBoundary = pos.x - (size.x / 2);
        float rightBoundary = pos.x + (size.x / 2);
        float bottomBoundary = pos.y - (size.y / 2);
        float topBoundary = pos.y + (size.y / 2);

        // Ȯ���� ������ ��ġ
        Vector3 objectPosition = objectTransform.position;

        // ������ ��ġ�� ��� ���� ��ġ�ϴ��� �˻�
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

    /// <summary>
    /// <b>Ʈ���� ����Ʈ�� �˻��Ͽ� �̺�Ʈ�� ���ǿ� ���� Ʈ���Ű� true���� �˻�</b>
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public bool CheckTrigger(EventParams p)
    {
        foreach (StringNTrigger t in trigger)
        {
            if (string.Equals(t.triggerName, p.condition.TargetFlag))
            {
                if (t.triggerValue == p.condition.FlagValue)
                {
                    return PostNotification(p);
                }
            }                
        }
        return false;
    }

    public bool CheckTrigger(Condition c)
    {
        foreach (StringNTrigger t in trigger)
        {
            if (string.Equals(t.triggerName, c.TargetFlag))
            {
                if (t.triggerValue == c.FlagValue)
                {
                    return true;
                }
            }
        }
        return false;
    }

    /// <summary>
    /// <b>Ʈ���� ����Ʈ�� �˻��Ͽ� ��ġ�ϴ� Ʈ���Ű� true���� �˻�</b>
    /// </summary>
    /// <param name="triggerName"></param>
    /// <param name="flagValue"></param>
    /// <returns></returns>
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

    /// <summary>
    /// <b>Ȯ���Ϸ��� ���� ������ �̸��� ���� ��ġ�ϴ� ������ ���ڰ�, ���ϴ� ��ġ �̻�/�������� Ȯ��</b>
    /// p.condition.TargetFlag : ��� ����
    /// p.condition.FlagValue : true = �̻�, false = ����
    /// p.condition.TargetNum : ��ġ
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
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

    public bool CheckNumber(Condition c)
    {
        foreach (StringNNumber n in number)
        {
            if (string.Equals(n.numberName, c.TargetFlag))
            {
                if (c.FlagValue)
                {
                    if (n.numberValue >= c.TargetNum)
                    {
                        return true;
                    }
                }
                else
                {
                    if (n.numberValue <= c.TargetNum)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    /// <summary>
    /// <b>������Ʈ �ı� �� ����� �Լ�</b> <br></br>
    /// - �ı��� ��� �̺�Ʈ �ߴ�
    /// </summary>
    private void OnDestroy()
    {
        foreach(CancellationTokenSource c in cts)
            c?.Cancel();
        //isInterrupted = true;
    }

    // ** �ܺ� ���� ��ũ��Ʈ **

    /// <summary>
    /// <b>Phase����Ʈ�� Phase�� �߰��ϰ� �߰��� Phase�� �̿��� �����ƾ�� ����</b> <br></br>
    /// - �߰��� phase�� condition���� ��� �Ҹ��� ó�� �� <br></br>
    /// </summary>
    /// <param name="phase"></param>
    public void AddPhaseToPhaseList_And_CreatePhaseSubroutine(EventPhase_so phase)
    {
        // phase�� condition���� isSatisfied������ false�� ��ȯ(���ǵ��� ��� �Ҹ��� �� ���·� ��ȯ)
        phase.UnSatisfyAll();
        
        // phase����Ʈ�� �Ű������� ������ phase�� �߰�
        phases.Add(phase);

        // �߰��� phase�� ������ �ε����� ����ǹǷ� �� �ε����� �ش��ϴ� phase�� �̿��� �����ƾ�� ����
        CreatePhaseSubroutine(phases.Count - 1);
    }

    /// <summary>
    /// <b>phase ����Ʈ�� ���</b>
    /// </summary>
    public void ClearPhase()
    {
        phases.Clear();
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
