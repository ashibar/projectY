using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// <para/><b>■■ StageManager ■■</b>
/// <para/>담당자 : 이용욱
/// <para/>요약 : 스테이지 데이터 및 하위모듈 관리
/// <para/>비고 : 
/// <para/>업데이트 내역 : 
/// <para/> - (23.08.22) : 요약문 생성
/// <para/>
/// </summary>

public class StageManager : MonoBehaviour, IEventListener
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
    // 하위 모듈
    [SerializeField] private ConditionChecker conditionChecker;
    [SerializeField] private EventTimer eventTimer;
    [SerializeField] private StageEndCheck stageEndCheck;
    // 스테이지 정보
    [SerializeField] private StageInfoContainer_so stageInfoContainer_so;
    [SerializeField] private StageInfo_so stageInfo_so;
    [SerializeField] private StageInfo stageInfo;

    public ConditionChecker ConditionChecker { get => conditionChecker; set => conditionChecker = value; }
    public EventTimer EventTimer { get => eventTimer; set => eventTimer = value; }
    public StageEndCheck StageEndCheck { get => stageEndCheck; set => stageEndCheck = value; }

    public List<EventMessage> messageBuffer = new List<EventMessage>();

    public static List<string> event_code = new List<string>
    {
        "Goto Next Phase",
    };

    private void Awake()
    {
        var objs = FindObjectsOfType<StageManager>();
        if (objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }
        conditionChecker = GetComponentInChildren<ConditionChecker>();
        eventTimer = GetComponentInChildren<EventTimer>();
        stageEndCheck = GetComponentInChildren<StageEndCheck>();
        messageBuffer.Clear();
        Time.timeScale = 1.0f;

        SubscribeEvent();
        SetStageInfo(stageInfoContainer_so.StageInfoList[stageInfoContainer_so.CurID]); // 나중에 로딩할때 대체
        //SetStageInfo_(stageInfoContainer_so.StageInfoList[stageInfoContainer_so.CurID]);
    }

    private void Start()
    {
        foreach (GameObject g in stageInfo.spawners)
        {
            Instantiate(g, SpawnManager.Instance.spawner_holder);
        }
        SpawnManager.Instance.SetSpawner();
        if (UIManager.Instance.TopIndicator)
        {
            UIManager.Instance.TopIndicator.Sort = stageInfo.StageSort;
            UIManager.Instance.TopIndicator.SetActive(); 
        }
        SetTargetUnit();
    }

    private void Update()
    {
        if (UIManager.Instance.ResultWindow)
            TestStageClear();
        //Async_Function();
    }

    public void SubscribeEvent()
    {
        foreach (string code in event_code)
            EventManager.Instance.AddListener(code, this);
    }

    public void OnEvent(string event_type, Component sender, Condition condition, params object[] param)
    {

        switch (event_type)
        {
            case "Goto Next Phase":
                GotoNextPhase((ExtraParams)param[0]); break;
        }
    }

    private void GotoNextPhase(ExtraParams par)
    {
        Debug.Log(par.NextPhase);
        eventTimer.AddPhase(par.NextPhase);
    }

    /// <summary>
    /// <para/> <b>로딩 시 StageManager에 스테이지 정보를 넣기 위한 함수</b>
    /// </summary>
    /// <param name="_stageInfo_so">스테이지 데이터 so파일</param>
    public void SetStageInfo(StageInfo_so _stageInfo_so)
    {
        stageInfo_so = _stageInfo_so;
        stageInfo = new StageInfo(_stageInfo_so);

        //SetTestPara();
        foreach(EventPhase_so phase in stageInfo.Phases)
            eventTimer.AddPhase(phase);
        //conditionChecker.SetPara(stageInfo.Para);
    }

    /// <summary>
    /// 임시 파라미터 설정 함수
    /// 후에 로딩 담당 컴포넌트가 대체함
    /// </summary>
    private void SetTestPara()
    {
        stageInfo.Para.Clear();

        Condition condition1 = new Condition();
        condition1.Sort = ConditionSort.Time;
        condition1.TargetNum = 1;

        List<Vector2> vecList = new List<Vector2>() {
            new Vector2( 5f,  5f),
            new Vector2( 5f, -5f),
            new Vector2(-5f,  5f),
            new Vector2(-5f, -5f)
        };

        ExtraParams p1 = new ExtraParams();
        p1.Id = 0;
        p1.VecList.Add(new Vector2(3f, 3f));

        ExtraParams p2 = new ExtraParams();
        p2.Id = 0;
        p2.VecList.AddRange(vecList);

        stageInfo.Para.Add(new EventParams(1, "Spawn Enemy At Vector By ID", condition1, p1));
        stageInfo.Para.Add(new EventParams(2, "Spawn Enemy At Vector List By ID", condition1, p2));
    }

    /// <summary>
    /// <b>다른 모듈들이 StageManager의 메시지버퍼를 참조하기 위한 함수</b><br/>
    /// <br/>
    /// - Module List -<br/>
    /// 1.<br/>
    /// 2. SpawnManager<br/>
    /// 3. UnitManager<br/>
    /// 4. AnimationManager<br/>
    /// 5. UIManager<br/>
    /// </summary>
    /// <param name="moduleID"></param>
    /// <param name="buffer"></param>
    /// <returns></returns>
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

    /// <summary>
    /// 테스트용 결과창 윈도우 출력 함수 <br/>
    /// 후에 대체 예정
    /// </summary>
    private void TestStageClear()
    {
        float cur = UnitManager.Instance.TargetDestroyed;
        float max = UnitManager.Instance.MaxDestroyed;
        if (cur >= max)
        {
            Time.timeScale = 0f;
            UIManager.Instance.ResultWindow.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// 결과창 윈도우에서 참조할 다음 스테이지 이동 함수 <br/>
    /// 후에 대체 예정
    /// </summary>
    public void GoNextStage()
    {
        stageInfoContainer_so.CurID += 1;
        int next = stageInfoContainer_so.CurID;
        LoadingSceneController.LoadScene("BattleScene", stageInfoContainer_so.CurID);
    }

    /// <summary>
    /// 상단 체력바 표시를 위한 주요 적 설정 <br/>
    /// 후에 대체 및 이동 예정
    /// </summary>
    public void SetTargetUnit()
    {
        if (UnitManager.Instance.Clones.Count <= 1)
            return;

        if (stageInfo.StageSort == StageSort.targetHp)
        {
            if (UnitManager.Instance.Clones.Count <= 1)
                return;
            UnitManager.Instance.TargetUnit = UnitManager.Instance.Clones[1].GetComponent<Unit>();
        }
    }

    





    // 더미 코드

    // 스테이지 정보의 이벤트 순회 함수
    //[SerializeField] private bool isCooltime;
    //[SerializeField] private int eventIndex = 0;
    //[SerializeField] private int interruptedIndex = -1;
    //private async void Async_Function()
    //{
    //    // 무결성
    //    if (stageInfo == null)
    //        return;

    //    // 업데이트
    //    if (!isCooltime && eventIndex < stageInfo.EventList.Count)
    //    {
    //        isCooltime = true;
    //        interruptedIndex = await Task_Function();
    //        isCooltime = false;
    //    }

    //    if (interruptedIndex != -1)
    //        Debug.Log("interruptedIndex = " + interruptedIndex.ToString());
    //}
    //// 이벤트의 메시지 입력 함수
    //private async Task<int> Task_Function()
    //{
    //    float end = Time.time + stageInfo.EventList[eventIndex].DurationToStart;

    //    while (Time.time < end)
    //    {
    //        if (stageInfo.EventList[eventIndex].Isinterrupted)
    //            await Task.FromResult(0);
    //        await Task.Yield();
    //    }

    //    messageBuffer.Add(stageInfo.EventList[eventIndex].Message);
    //    eventIndex++;

    //    return -1;
    //}
    //// 파괴시 모든 이벤트 중단
    //private void OnDestroy()
    //{
    //    foreach (EventInfo e in stageInfo.EventList)
    //    {
    //        e.Isinterrupted = true;
    //    }
    //}

    // 이전 초기화 함수
    //public void SetStageInfo(StageInfo_so _stageInfo_so)
    //{
    //    stageInfo_so = _stageInfo_so;
    //    stageInfo = new StageInfo(_stageInfo_so);
    //    conditionChecker.Events.AddRange(stageInfo.EventList);
    //}
}
