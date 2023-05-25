using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    private static StageManager instance;
    public static StageManager Instance
    {
        get
        {
            if (instance == null)
            {
                var obj = FindObjectOfType<StageManager>();
                if (obj != null)
                {
                    instance = obj;
                }
                else
                {
                    var newObj = new GameObject().AddComponent<StageManager>();
                    instance = newObj;
                }
            }
            return instance;
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
    // �ı��� ��� �̺�Ʈ �ߴ�
    private void OnDestroy()
    {
        foreach (EventInfo e in stageInfo.event_seq)
        {
            e.Isinterrupted = true;
        }
    }
}
