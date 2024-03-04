using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// <para/><b>■■ UnitManager ■■</b>
/// <para/>담당자 : 이용욱
/// <para/>요약 : 유저 인터페이스 관리
/// <para/>비고 : 
/// <para/>업데이트 내역 : 
/// <para/> - (23.10.17) : 요약문 생성
/// <para/>
/// </summary>

public class UIManager : MonoBehaviour, IEventListener
{
    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (instance == null) // instance가 비어있다
            {
                var obj = FindObjectOfType<UIManager>();
                if (obj != null)
                {
                    instance = obj;                                             // 전체 찾아봤는데? 있네? 그걸 넣자
                }
                else
                {
                    var newObj = new GameObject().AddComponent<UIManager>(); // 전체 찾아봤는데? 없네? 새로만들자
                    instance = newObj;
                }
            }
            return instance; // 안비어있네? 그냥 그대로 가져와
        }
    }

    // 하위 UI 모듈
    // Manager/Camera_SoundManager prefab사용 시 이미 할당되어 있으나, 새로 추가 시 추가 조치가 필요함.
    [SerializeField] private ScreenFade screenFade;
    [SerializeField] private Indicator indicator_keyboard;
    [SerializeField] private Indicator indicator_mouse;
    [SerializeField] private Indicator indicator_centerImage;
    [SerializeField] private ResultWindow resultWindow;
    [SerializeField] private GameoverWindow gameoverWindow;
    [SerializeField] private StageinfoText stageinfoText;
    [SerializeField] private TopIndicator topIndicator;
    [SerializeField] private Transform hpIndicator;
    [SerializeField] private TutorialLogo tutorialLogo;
    [SerializeField] private CenterText centerText;
    [SerializeField] private List<GameObject> indicatorList = new List<GameObject>();

    // 스테이지 정보 변수
    // Awake 시 LoadDataSingleton에서 로드
    [SerializeField] private StageInfoContainer_so stageInfoContainer;

    // 외부 참조를 위한 큰 범주의 UI
    public ResultWindow ResultWindow { get => resultWindow; set => resultWindow = value; }
    public TopIndicator TopIndicator { get => topIndicator; set => topIndicator = value; }

    /// <summary>
    /// 구독할 이벤트 코드 리스트
    /// </summary>
    public static List<string> event_code = new List<string>
    {
        "Fade In",
        "Fade Out",
        "Key Board Indicator",
        "Set Center Indicator",
        "Force Load",
        "Logo Appears",
        "Set Mouse Indicator",
        "Active Result Window",
        "Active Gameover Window",
        "Active StageInfo Window",
        "Active Top Indicator",
        "Set Active Indicator",
        "Set Active UI",
        "Set Center Text",
    };

    /// <summary>
    /// 스크립트 첫 활성화 시 가장 먼저 실행되는 함수.
    /// </summary>
    private void Awake()
    {
        // 싱글톤을 위해 가장 먼저 생성된 오브젝트 제외 하고는 모두 제거
        var objs = FindObjectsOfType<UIManager>();
        if (objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }

        // 스테이지 정보를 LoadDataSingleton으로 부터 불러옴
        stageInfoContainer = LoadDataSingleton.Instance.StageInfoContainer();
        
        // 하위 오브젝트를 검색해 모듈을 불러옴
        resultWindow = GetComponentInChildren<ResultWindow>(true);
        
        // 이벤트 매니저에 구독
        SubscribeEvent();
    }

    /// <summary>
    /// 매 프레임마다 실행하는 함수.
    /// </summary>
    private void Update()
    {
        //EventReciever();
        //EventListener();
        //gameoverListener();
    }

    /// <summary>
    /// 이벤트 매니저에 구독
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
        // 사용가능한 형태로 형변환
        ExtraParams para = (ExtraParams)param[0];

        // 이벤트 코드에 따른 분기
        switch (event_type)
        {
            case "Fade Out":
                FadeOut(para); break;
            case "Fade In":
                FadeIn(para); break;
            case "Key Board Indicator":
                KeyBoardIndicator(para); break;
            case "Set Center Indicator":
                SetCenterIndicator(para); break;
            case "Force Load":
                ForceLoad(para); break;
            case "Logo Appears":
                LogoAppears(para); break;
            case "Set Mouse Indicator":
                SetMouseIndicator(para); break;
            case "Active Result Window":
                ActiveResultWindow(para); break;
            case "Active Gameover Window":
                ActiveGameoverWindow(para); break;
            case "Active StageInfo Window":
                ActiveStageWindow(para); break;
            case "Active Top Indicator":
                ActiveTopIndicator(para); break;
            case "Set Active Indicator":
                SetActiveIndicator(para); break;
            case "Set Active UI":
                SetActiveUI(para); break;
            case "Set Center Text":
                SetCenterText(para); break;
            default:
                break;
        }
    }

    /// <summary>
    /// [OnEvent] 화면 검게 처리 <br></br>
    /// 매개변수 X
    /// </summary>
    /// <param name="para"></param>
    private void FadeOut(ExtraParams para)
    {
        screenFade.SetScreenFade(false);
    }

    /// <summary>
    /// [OnEvent] 검게 된 화면 복구 <br></br>
    /// 매개변수 X
    /// </summary>
    /// <param name="para"></param>
    private void FadeIn(ExtraParams para)
    {
        screenFade.SetScreenFade(true);
    }

    /// <summary>
    /// [OnEvent] 키보드 튜토리얼 인디케이터 출력 <br></br>
    /// 후에 튜토리얼 오브젝트는 통일 시킬 예정 <br></br>
    /// 매개변수 X
    /// </summary>
    /// <param name="para"></param>
    private void KeyBoardIndicator(ExtraParams para)
    {
        indicator_keyboard.SetAnim("Out");
    }

    /// <summary>
    /// [OnEvent] 가운데 검은 마석을 들고 있는 이미지 출력<br></br>
    /// 후에 튜토리얼 오브젝트는 통일 시킬 예정 <br></br>
    /// bool : 활성화 여부
    /// </summary>
    /// <param name="para"></param>
    private void SetCenterIndicator(ExtraParams para)
    {
        indicator_centerImage.gameObject.SetActive(para.Boolvalue);
    }

    /// <summary>
    /// [OnEvent] Scene 전환 <br></br>
    /// Name : 전환할 Scene 이름 <br></br>
    /// Int : StageContainer의 CurID설정(맵 번호)
    /// </summary>
    /// <param name="para"></param>
    private void ForceLoad(ExtraParams para)
    {
        LoadingSceneController.LoadScene(para.Name, para.Intvalue);
    }

    /// <summary>
    /// [OnEvent] 튜토리얼 씬 종료 후 로고를 띄우는 애니메이션 출력 <br></br>
    /// 매개변수 X
    /// </summary>
    /// <param name="para"></param>
    private void LogoAppears(ExtraParams para)
    {
        tutorialLogo.SetAnim("isAppear");
    }

    /// <summary>
    /// [OnEvent] 마우스 튜토리얼 인디케이터 출력 <br></br>
    /// 후에 튜토리얼 오브젝트는 통일 시킬 예정 <br></br>
    /// 매개변수 X
    /// </summary>
    /// <param name="para"></param>
    private void SetMouseIndicator(ExtraParams para)
    {
        indicator_mouse.SetAnim("Out");
    }

    /// <summary>
    /// [OnEvent] 보상을 위한 결과창을 출력 <br></br>
    /// bool : true일 시 전체적인 애니메이션은 생략하고 보상만 출력
    /// </summary>
    /// <param name="para"></param>
    private void ActiveResultWindow(ExtraParams para)
    {
        if (resultWindow == null)
            return;

        bool isRewardOnly = para.Boolvalue;
        
        // 결과창 오브젝트 활성화
        resultWindow.gameObject.SetActive(true);
        
        // 결과창 애니메이션 전체 출력
        if (!isRewardOnly)
            resultWindow.Active_Full();
        // 결과창 보상 애니메이션만 출력
        else
            resultWindow.Active_RewardOnly();
    }

    /// <summary>
    /// [OnEvent] 게임 패배 윈도우 출력 <br></br>
    /// 매개변수 X
    /// </summary>
    /// <param name="para"></param>
    private void ActiveGameoverWindow(ExtraParams para)
    {
        if (gameoverWindow == null)
            return;

        gameoverWindow.gameObject.SetActive(true);
        gameoverWindow.Active();
    }

    /// <summary>
    /// [OnEvent] 게임 시작시 맵 정보 출력 <br></br>
    /// 매개변수 X, LoadDataSingleton에서 stageInfoContainer을 불러와 사용
    /// </summary>
    /// <param name="para"></param>
    private void ActiveStageWindow(ExtraParams para)
    {
        if (stageinfoText == null)
            return;

        // LoadDataSingleton으로부터 불러온 stageInfoContainer에서 맵번호(id), stageName(맵이름), stageCondition(클리어 조건)을 가져옴
        int id = stageInfoContainer.CurID;
        string stageName = stageInfoContainer.StageInfoList[id].StageName;
        string stageCondition = stageInfoContainer.StageInfoList[id].StageSort.ToString();
        switch (stageInfoContainer.StageInfoList[id].StageSort)
        {
            case StageSort.None:
                stageCondition = ""; break;
            case StageSort.Timer:
                stageCondition = "제한시간까지 생존"; break;
            case StageSort.targetDestroy:
                stageCondition = "목표 제거"; break;
            case StageSort.infinite:
                stageCondition = "살아남으세요"; break;
        }

        // 가져온 정보를 stageinfoText모듈에 전달 후 적용
        stageinfoText.gameObject.SetActive(true);
        stageinfoText.Active(stageName, stageCondition);
    }

    /// <summary>
    /// [OnEvent] 상단에 있는 대부분의 UI의 활성화 여부 결정 <br></br>
    /// int : 맵클리어 조건(stageInfoContainer.StageInfoList[i].StageSort)이 StageSort.targetDestroy(목표물 제거 수 검사)일 경우 그 수량을 정함
    /// </summary>
    /// <param name="para"></param>
    private void ActiveTopIndicator(ExtraParams para)
    {
        // UnitManager가 제저된 유닛 수를 검사하는 담당이며, Event Maker의 Condition에서 AllUnit 매개변수로 검사가능
        UnitManager.Instance.MaxDestroyed = para.Intvalue;
        topIndicator.Sort = stageInfoContainer.StageInfoList[stageInfoContainer.CurID].StageSort;
        topIndicator.SetActive();
    }

    /// <summary>
    /// [OnEvent] 이름으로 인디케이터를 찾아 활성화 여부 결정 <br></br>
    /// Name : 찾을 인디케이터의 오브젝트 이름
    /// bool : 활성화 여부
    /// VecList[0] : 표시될 위치
    /// </summary>
    /// <param name="para"></param>
    private void SetActiveIndicator(ExtraParams para)
    {
        string indicator_name = para.Name;
        bool isActive = para.Boolvalue;
        Vector2 pos = para.VecList.Count > 0 ? para.VecList[0] : Vector2.zero;

        // 인디케이터 리스트에서 이름을 검색, 그 이후 처리는 인디케이터에 내장된 Indicator클래스를 참조
        foreach (GameObject ind in indicatorList)
        {
            if (string.Equals(ind.gameObject.name, indicator_name))
            {
                ind.GetComponent<Indicator>().SetActive(isActive, pos);
            }
        }
    }

    /// <summary>
    /// [OnEvent] 모든 UI활성화 여부 결정
    /// bool : 활성화 여부
    /// </summary>
    /// <param name="para"></param>
    private void SetActiveUI(ExtraParams para)
    {
        topIndicator.gameObject.SetActive(para.Boolvalue);
        hpIndicator.gameObject.SetActive(para.Boolvalue);
    }
    
    /// <summary>
    /// [OnEvent] 화면 중심에 UI출력
    /// Name : 출력할 텍스트
    /// int : 폰트 사이즈 (기본값 70, 0입력 시 적용)
    /// float : 출력을 지속시킬 시간 (기본값 3초, 0입력 시 적용)
    /// bool : true는 강제 비활성화
    /// </summary>
    /// <param name="para"></param>
    private void SetCenterText(ExtraParams para)
    {
        centerText.ActiveText(para.Name, para.Floatvalue, para.Intvalue, para.Boolvalue);
    }

    // Dummy Code
    //[SerializeField] private List<EventMessage> messageBuffer = new List<EventMessage>();
    //private void EventReciever()
    //{
    //    int error = StageManager.Instance.SearchMassage(5, messageBuffer);
    //    if (error == -1)
    //        return;
    //}

    //private void EventListener()
    //{
    //    bool isError = false;
    //    EventMessage temp = new EventMessage();
    //    if (messageBuffer.Count <= 0)
    //        return;

    //    foreach (EventMessage m in messageBuffer)
    //    {
    //        switch (m.ActionSTR)
    //        {
    //            case "Fade Out":
    //                screenFade.SetScreenFade(false);
    //                messageBuffer.Remove(m);
    //                return;
    //            case "Fade In":
    //                screenFade.SetScreenFade(true);
    //                messageBuffer.Remove(m);
    //                return;
    //            case "KeyBoard Indicator":
    //                indicator_keyboard.SetAnim("Out");
    //                messageBuffer.Remove(m);
    //                return;
    //            case "Center Indicator":
    //                indicator_centerImage.gameObject.SetActive(string.Equals(m.TargetSTR, "true"));
    //                messageBuffer.Remove(m);
    //                return;
    //            case "Force Load":
    //                LoadingSceneController.LoadScene("BattleScene", 1);
    //                messageBuffer.Remove(m);
    //                return;
    //            default:
    //                isError = true;
    //                break;
    //        }


    //    }

    //    if (!isError)
    //    {
    //        messageBuffer.Remove(temp);
    //    }
    //}
    //Player player = null;
    //private void gameoverListener()
    //{
    //    player = Player.Instance;
    //    if (player.stat.Hp_current <= 0)
    //    {
    //        gameover.gameObject.SetActive(true);
    //        Time.timeScale = 0;
    //    }
    //}    
}
