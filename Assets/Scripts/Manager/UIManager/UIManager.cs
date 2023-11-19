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

    [SerializeField] private ScreenFade screenFade;
    [SerializeField] private Indicator indicator_keyboard;
    [SerializeField] private Indicator indicator_mouse;
    [SerializeField] private Indicator indicator_centerImage;
    [SerializeField] private ResultWindow resultWindow;
    [SerializeField] private GameoverWindow gameoverWindow;
    [SerializeField] private StageinfoText stageinfoText;
    [SerializeField] private TopIndicator topIndicator;
    [SerializeField] private TutorialLogo tutorialLogo;
    [SerializeField] private List<GameObject> indicatorList = new List<GameObject>();

    [SerializeField] private StageInfoContainer_so stageInfoContainer;

    public ResultWindow ResultWindow { get => resultWindow; set => resultWindow = value; }
    public TopIndicator TopIndicator { get => topIndicator; set => topIndicator = value; }

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
    };

    private void Awake()
    {
        var objs = FindObjectsOfType<UIManager>();
        if (objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }
        stageInfoContainer = LoadDataSingleton.Instance.StageInfoContainer();
        resultWindow = GetComponentInChildren<ResultWindow>(true);
        SubscribeEvent();
    }    

    private void Update()
    {
        //EventReciever();
        //EventListener();
        //gameoverListener();
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
            default:
                break;
        }
    }

    private void FadeOut(ExtraParams para)
    {
        screenFade.SetScreenFade(false);
    }

    private void FadeIn(ExtraParams para)
    {
        screenFade.SetScreenFade(true);
    }

    private void KeyBoardIndicator(ExtraParams para)
    {
        indicator_keyboard.SetAnim("Out");
    }

    private void SetCenterIndicator(ExtraParams para)
    {
        indicator_centerImage.gameObject.SetActive(para.Boolvalue);
    }

    private void ForceLoad(ExtraParams para)
    {
        LoadingSceneController.LoadScene(para.Name, para.Intvalue);
    }

    private void LogoAppears(ExtraParams para)
    {
        tutorialLogo.SetAnim("isAppear");
    }

    private void SetMouseIndicator(ExtraParams para)
    {
        indicator_mouse.SetAnim("Out");
    }

    private void ActiveResultWindow(ExtraParams para)
    {
        if (resultWindow == null)
            return;

        resultWindow.gameObject.SetActive(true);
        resultWindow.Active();
    }

    private void ActiveGameoverWindow(ExtraParams para)
    {
        if (gameoverWindow == null)
            return;

        gameoverWindow.gameObject.SetActive(true);
        gameoverWindow.Active();
    }

    private void ActiveStageWindow(ExtraParams para)
    {
        if (stageinfoText == null)
            return;

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
        }

        stageinfoText.gameObject.SetActive(true);
        stageinfoText.Active(stageName, stageCondition);
    }

    private void ActiveTopIndicator(ExtraParams para)
    {
        UnitManager.Instance.MaxDestroyed = para.Intvalue;
        topIndicator.Sort = stageInfoContainer.StageInfoList[stageInfoContainer.CurID].StageSort;
        topIndicator.SetActive();
    }

    private void SetActiveIndicator(ExtraParams para)
    {
        string indicator_name = para.Name;
        bool isActive = para.Boolvalue;
        Vector2 pos = para.VecList.Count > 0 ? para.VecList[0] : Vector2.zero;

        foreach (GameObject ind in indicatorList)
        {
            if (string.Equals(ind.gameObject.name, indicator_name))
            {
                ind.GetComponent<Indicator>().SetActive(isActive, pos);
            }
        }
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
