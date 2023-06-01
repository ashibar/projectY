using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    private static StageManager instance; // private instance
    public static StageManager Instance
    {
        get
        {
            if (instance == null) // instance�� ����ִ�
            {
                var obj = FindObjectOfType<StageManager>();
                if (obj != null)
                {
                    instance = obj;                                             // ��ü ã�ƺôµ�? �ֳ�? �װ� ����
                }
                else
                {
                    var newObj = new GameObject().AddComponent<StageManager>(); // ��ü ã�ƺôµ�? ����? ���θ�����
                    instance = newObj;
                }
            }
            return instance; // �Ⱥ���ֳ�? �׳� �״�� ������
        }
    }
    
    [SerializeField] private StageInfo_so stageInfo_so;
    [SerializeField] private StageInfo stageInfo;

    public List<EventMessage> messageBuffer = new List<EventMessage>();

    private void Awake()
    {
        var objs = FindObjectsOfType<StageManager>();
        if (objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }
        SetStageInfo(stageInfo_so); // ���߿� �ε��Ҷ� ��ü
    }

    private void Update()
    {
        Async_Function();
    }

    // �ε� �� StageManager�� �������� ������ �ֱ� ���� �Լ�
    public void SetStageInfo(StageInfo_so _stageInfo_so)
    {
        stageInfo_so = _stageInfo_so;
        stageInfo = new StageInfo(_stageInfo_so);
    }

    // �ٸ� ������ StageManager�� �޽������۸� �����ϱ� ���� �Լ�
    /*
     * Module List
     *      1.
     *      2. SpawnManager
     *      3. UnitManager
     *      4. AnimationManager
     *      5. UIManager
     */
    public int SearchMassage(int moduleID, List<EventMessage> buffer)
    {
        if (messageBuffer.Count == 0)
            return -1;

        List<EventMessage> addition = new List<EventMessage>();

        for (int i = 0; i < messageBuffer.Count; i++)
        {
            if (messageBuffer[i].ModuleID == moduleID) 
            {
                EventMessage tmp = messageBuffer[i];
                messageBuffer.RemoveAt(i);
                addition.Add(tmp);
            }
        }

        if (addition.Count > 0)
        {
            buffer.AddRange(addition);
            return 0;
        }
        else
            return -1;
    }

    // �������� ������ �̺�Ʈ ��ȸ �Լ�
    [SerializeField] private bool isCooltime;
    [SerializeField] private int eventIndex = 0;
    [SerializeField] private int interruptedIndex = -1;
    private async void Async_Function()
    {
        // ���Ἲ
        if (stageInfo == null)
            return;

        // ������Ʈ
        if (!isCooltime && eventIndex < stageInfo.event_seq.Count)
        {
            isCooltime = true;
            interruptedIndex = await Task_Function();
            isCooltime = false;
        }

        if (interruptedIndex != -1)
            Debug.Log("interruptedIndex = " + interruptedIndex.ToString());
    }
    // �̺�Ʈ�� �޽��� �Է� �Լ�
    private async Task<int> Task_Function()
    {
        float end = Time.time + stageInfo.event_seq[eventIndex].DurationToStart;

        while (Time.time < end)
        {
            if (stageInfo.event_seq[eventIndex].Isinterrupted)
                await Task.FromResult(0);
            await Task.Yield();
        }

        messageBuffer.Add(stageInfo.event_seq[eventIndex].Message);
        eventIndex++;

        return -1;
    }
    // ���� �˻�
    [SerializeField] public List<string> conditionTriggers = new List<string>();
    private void ConditionCheck()
    {
        for (int i = 0; i < stageInfo.event_req.Count; i++)
        {
            foreach (string t in conditionTriggers)
            {
                if (stageInfo.event_req[i].Condition.CheckCondition(t))
                {
                    conditionTriggers.Remove(t);
                    messageBuffer.Add(stageInfo.event_req[i].Message);
                    stageInfo.event_req.RemoveAt(i);
                    return;
                }
            }
            if (stageInfo.event_req[i].Condition.CheckCondition(""))
            {

            }
        }
    }
    // �ı��� ��� �̺�Ʈ �ߴ�
    private void OnDestroy()
    {
        foreach (EventInfo e in stageInfo.event_seq)
        {
            e.Isinterrupted = true;
        }
    }
}
