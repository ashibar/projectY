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
            if (instance == null) // instance가 비어있다
            {
                var obj = FindObjectOfType<StageManager>();
                if (obj != null)
                {
                    instance = obj;                                             // 전체 찾아봤는데? 있네? 그걸 넣자
                }
                else
                {
                    var newObj = new GameObject().AddComponent<StageManager>(); // 전체 찾아봤는데? 없네? 새로만들자
                    instance = newObj;
                }
            }
            return instance; // 안비어있네? 그냥 그대로 가져와
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
        SetStageInfo(stageInfo_so); // 나중에 로딩할때 대체
    }

    private void Update()
    {
        Async_Function();
    }

    // 로딩 시 StageManager에 스테이지 정보를 넣기 위한 함수
    public void SetStageInfo(StageInfo_so _stageInfo_so)
    {
        stageInfo_so = _stageInfo_so;
        stageInfo = new StageInfo(_stageInfo_so);
    }

    // 다른 모듈들이 StageManager의 메시지버퍼를 참조하기 위한 함수
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

    // 스테이지 정보의 이벤트 순회 함수
    [SerializeField] private bool isCooltime;
    [SerializeField] private int eventIndex = 0;
    [SerializeField] private int interruptedIndex = -1;
    private async void Async_Function()
    {
        // 무결성
        if (stageInfo == null)
            return;

        // 업데이트
        if (!isCooltime && eventIndex < stageInfo.event_seq.Count)
        {
            isCooltime = true;
            interruptedIndex = await Task_Function();
            isCooltime = false;
        }

        if (interruptedIndex != -1)
            Debug.Log("interruptedIndex = " + interruptedIndex.ToString());
    }
    // 이벤트의 메시지 입력 함수
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
    // 조건 검사
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
    // 파괴시 모든 이벤트 중단
    private void OnDestroy()
    {
        foreach (EventInfo e in stageInfo.event_seq)
        {
            e.Isinterrupted = true;
        }
    }
}
