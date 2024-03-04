using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// <b>■■ StageManager ■■</b> <br></br>
/// 담당자 : 이용욱 <br></br>
/// 요약 : 스테이지 데이터 및 하위모듈 관리 <br></br>
/// 비고 :  <br></br>
/// 업데이트 내역 :  <br></br>
///  - (23.08.22) : 요약문 생성  <br></br>
///  <br></br>
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
    // 플레이어 정보
    [SerializeField] public PlayerInfoContainer playerInfoContainer_so;
    [SerializeField] public SpellPrefabContainer SpellPrefabContainer_so;

    // 하위 모듈    
    public ConditionChecker ConditionChecker { get => conditionChecker; set => conditionChecker = value; }
    public EventTimer EventTimer { get => eventTimer; set => eventTimer = value; }
    public StageEndCheck StageEndCheck { get => stageEndCheck; set => stageEndCheck = value; }

    // 디버그
    [SerializeField] private bool verbose;

    public static List<string> event_code = new List<string>
    {
        "Goto Next Phase",
        "Force Load",
        "Set Stage Info By ID",
        "Set Next Stage Of Infinite Mode",
    };

    /// <summary>
    /// <b>스크립트 첫 활성화 시 가장 먼저 실행되는 함수.</b><br></br>
    /// <br></br>
    /// - 싱글톤 처리 <br></br>
    /// - 데이터 로드 <br></br>
    /// - 하위 모듈 로드 <br></br>
    /// - 이벤트 구독 <br></br>
    /// </summary>
    private void Awake()
    {
        // 싱글톤 오브젝트 단일화
        var objs = FindObjectsOfType<StageManager>();
        if (objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }

        // 디버그씬 전용 예외 처리
        // 디버그씬인것이 감지되었을 때 StageInfoContainer를 디버그씬 전용으로 교체
        if (SceneManager.GetActiveScene().name == "Debug_Scene")
            LoadDataSingleton.Instance.SetStageInfoContainer("Debug_Scene");
        
        // LoadDataSingleton으로부터 데이터를 불러옴
        stageInfoContainer_so = LoadDataSingleton.Instance.StageInfoContainer();
        playerInfoContainer_so = LoadDataSingleton.Instance.PlayerInfoContainer();
        SpellPrefabContainer_so = LoadDataSingleton.Instance.SpellPrefabContainer();
        
        // 하위 모듈 불러옴
        conditionChecker = GetComponentInChildren<ConditionChecker>();
        eventTimer = GetComponentInChildren<EventTimer>();
        stageEndCheck = GetComponentInChildren<StageEndCheck>();

        // 전체 시간 배율 초기화
        Time.timeScale = 1.0f;

        // 이벤트 구독
        SubscribeEvent();
    }

    /// <summary>
    /// <b>스크립트 첫 활성화 시 두번째로 실행되는 함수.</b><br></br>
    /// <br></br>
    /// 
    /// </summary>
    private void Start()
    {
        if (verbose)
        {
            Debug.Log(stageInfoContainer_so.name);
            Debug.Log("id = " + stageInfoContainer_so.CurID.ToString());
        }

        // ★★★
        // StageInfoContainer를 EventTimer모듈에 전달 후 이벤트 실행을 위한 비동기 서브루틴 생성
        SetStageInfo(stageInfoContainer_so.StageInfoList[stageInfoContainer_so.CurID]);

        // ★★★
        // LoadDataSingleton으로부터 기존 저장된 스펠 정보를 불러와 씬 시작 시 적용
        LoadPlayerSpell();

        // 상단 체력바 표시를 위한 주요 적 설정
        SetTargetUnit();
    }

    private void Update()
    {
        
    }

    /// <summary>
    /// 이벤트 구독
    /// </summary>
    public void SubscribeEvent()
    {
        foreach (string code in event_code)
            EventManager.Instance.AddListener(code, this);
    }

    /// <summary>
    /// 이벤트 매니저가 방송하는 이벤트와 비교후 실행하는 함수
    /// </summary>
    /// <param name="event_type">이벤트 코드</param>
    /// <param name="sender">송신자</param>
    /// <param name="condition">조건</param>
    /// <param name="param">매개변수 묶음</param>
    public void OnEvent(string event_type, Component sender, Condition condition, params object[] param)
    {

        switch (event_type)
        {
            case "Goto Next Phase":
                GotoNextPhase((ExtraParams)param[0]); break;
            case "Force Load":
                ForceLoad((ExtraParams)param[0]); break;
            case "Set Stage Info By ID":
                SetStageInfoByID((ExtraParams)param[0]); break;
            case "Set Next Stage Of Infinite Mode":
                SetNextStageOfInfiniteMode((ExtraParams)param[0]); break;
        }
    }

    /// <summary>
    /// <b>[OnEvent] 다음 Phase로 전환</b> <br></br>
    /// NextPhase : 전환될 다음 Phase(EventPhase_so) <br></br>
    /// </summary>
    /// <param name="par"></param>
    private void GotoNextPhase(ExtraParams par)
    {
        if (verbose)
        {
            Debug.Log(par.NextPhase); 
        }
        eventTimer.AddPhaseToPhaseList_And_CreatePhaseSubroutine(par.NextPhase);
    }

    /// <summary>
    /// <b>[OnEvent] 씬이름을 매개변수로 받아 강제 로드</b> <br></br>
    /// Name : 로드 될 씬 이름 <br></br>
    /// Int : 맵 번호가 필요할 경우, 맵 번호
    /// </summary>
    /// <param name="para"></param>
    private void ForceLoad(ExtraParams para)
    {
        LoadingSceneController.LoadScene(para.Name, para.Intvalue);
    }

    /// <summary>
    /// <b>[OnEvent] 맵 번호를 받아 BattleScene의 이벤트 타이머 서브루틴 생성</b> <br></br>
    /// Int : 맵 번호
    /// </summary>
    /// <param name="para"></param>
    private void SetStageInfoByID(ExtraParams para)
    {
        SetStageInfo(stageInfoContainer_so.StageInfoList[Mathf.Clamp(para.Intvalue, 0, stageInfoContainer_so.StageInfoList.Count)]);
    }

    /// <summary>
    /// <b>[OnEvent] 무한모드 시 스테이지 설정, 이벤트 서브루틴 생성</b> <br></br>
    /// 매개변수 X
    /// PlayerInfoContainer의 progress_step_infinite값의 영향을 받음
    /// </summary>
    /// <param name="para"></param>
    private void SetNextStageOfInfiniteMode(ExtraParams para)
    {
        LoadDataSingleton.Instance.PlayerInfoContainer().Progress_step_infinite += 1;
        int progress = LoadDataSingleton.Instance.PlayerInfoContainer().Progress_step_infinite;
        SetStageInfo(stageInfoContainer_so.StageInfoList[Mathf.Clamp(progress, 0, stageInfoContainer_so.StageInfoList.Count - 1)]);
    }

    /// <summary>
    /// <b>로딩 시 StageManager에 스테이지 정보를 넣기 위한 함수</b> <br></br>
    /// </summary>
    /// <param name="_stageInfo_so">스테이지 데이터 so파일</param>
    public void SetStageInfo(StageInfo_so _stageInfo_so)
    {
        stageInfo_so = _stageInfo_so;
        stageInfo = new StageInfo(_stageInfo_so);

        eventTimer.ClearPhase();
        foreach(EventPhase_so phase in stageInfo.Phases)
            eventTimer.AddPhaseToPhaseList_And_CreatePhaseSubroutine(phase);
        //conditionChecker.SetPara(stageInfo.Para);
    }

    /// <summary>
    /// <para/> <b>로딩 시 플레이어의 스펠을 불러옴</b>
    /// </summary>
    public void LoadPlayerSpell()
    {
        if (!Player.Instance.spellManager)
            return;
        
        Player.Instance.spellManager.ClearSpell();
        List<StringNString> codes = playerInfoContainer_so.Spell_activated;
        List<GameObject> cloneList = new List<GameObject>();
        GameObject holder_obj = new GameObject();
        foreach (StringNString code in codes)
        {
            GameObject spell_prefab = SpellPrefabContainer_so.Search(code.string1);
            GameObject spell_clone = Instantiate(spell_prefab, holder_obj.transform);
            Debug.Log(spell_prefab);
            if (!string.Equals(code.string2, ""))
                foreach (GameObject clone in cloneList)
                    if (string.Equals(clone.GetComponent<Spell>().GetCode(), code.string2))
                        spell_clone.transform.parent = clone.transform;
            cloneList.Add(spell_clone);
        }
        Player.Instance.spellManager.SetSpell(holder_obj);
        Player.Instance.spellManager.gameObject.SetActive(true);
    }

    /// <summary>
    /// 임시 파라미터 설정 함수
    /// 후에 로딩 담당 컴포넌트가 대체함
    /// </summary>
    //private void SetTestPara()
    //{
    //    stageInfo.Para.Clear();

    //    Condition condition1 = new Condition();
    //    condition1.Sort = ConditionSort.Time;
    //    condition1.TargetNum = 1;

    //    List<Vector2> vecList = new List<Vector2>() {
    //        new Vector2( 5f,  5f),
    //        new Vector2( 5f, -5f),
    //        new Vector2(-5f,  5f),
    //        new Vector2(-5f, -5f)
    //    };

    //    ExtraParams p1 = new ExtraParams();
    //    p1.Id = 0;
    //    p1.VecList.Add(new Vector2(3f, 3f));

    //    ExtraParams p2 = new ExtraParams();
    //    p2.Id = 0;
    //    p2.VecList.AddRange(vecList);

    //    stageInfo.Para.Add(new EventParams(1, "Spawn Enemy At Vector By ID", condition1, p1));
    //    stageInfo.Para.Add(new EventParams(2, "Spawn Enemy At Vector List By ID", condition1, p2));
    //}


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
