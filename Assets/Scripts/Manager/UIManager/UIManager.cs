using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// <para/><b>��� UnitManager ���</b>
/// <para/>����� : �̿��
/// <para/>��� : ���� �������̽� ����
/// <para/>��� : 
/// <para/>������Ʈ ���� : 
/// <para/> - (23.10.17) : ��๮ ����
/// <para/>
/// </summary>

public class UIManager : MonoBehaviour, IEventListener
{
    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (instance == null) // instance�� ����ִ�
            {
                var obj = FindObjectOfType<UIManager>();
                if (obj != null)
                {
                    instance = obj;                                             // ��ü ã�ƺôµ�? �ֳ�? �װ� ����
                }
                else
                {
                    var newObj = new GameObject().AddComponent<UIManager>(); // ��ü ã�ƺôµ�? ����? ���θ�����
                    instance = newObj;
                }
            }
            return instance; // �Ⱥ���ֳ�? �׳� �״�� ������
        }
    }

    // ���� UI ���
    // Manager/Camera_SoundManager prefab��� �� �̹� �Ҵ�Ǿ� ������, ���� �߰� �� �߰� ��ġ�� �ʿ���.
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

    // �������� ���� ����
    // Awake �� LoadDataSingleton���� �ε�
    [SerializeField] private StageInfoContainer_so stageInfoContainer;

    // �ܺ� ������ ���� ū ������ UI
    public ResultWindow ResultWindow { get => resultWindow; set => resultWindow = value; }
    public TopIndicator TopIndicator { get => topIndicator; set => topIndicator = value; }

    /// <summary>
    /// ������ �̺�Ʈ �ڵ� ����Ʈ
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
    /// ��ũ��Ʈ ù Ȱ��ȭ �� ���� ���� ����Ǵ� �Լ�.
    /// </summary>
    private void Awake()
    {
        // �̱����� ���� ���� ���� ������ ������Ʈ ���� �ϰ�� ��� ����
        var objs = FindObjectsOfType<UIManager>();
        if (objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }

        // �������� ������ LoadDataSingleton���� ���� �ҷ���
        stageInfoContainer = LoadDataSingleton.Instance.StageInfoContainer();
        
        // ���� ������Ʈ�� �˻��� ����� �ҷ���
        resultWindow = GetComponentInChildren<ResultWindow>(true);
        
        // �̺�Ʈ �Ŵ����� ����
        SubscribeEvent();
    }

    /// <summary>
    /// �� �����Ӹ��� �����ϴ� �Լ�.
    /// </summary>
    private void Update()
    {
        //EventReciever();
        //EventListener();
        //gameoverListener();
    }

    /// <summary>
    /// �̺�Ʈ �Ŵ����� ����
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
        // ��밡���� ���·� ����ȯ
        ExtraParams para = (ExtraParams)param[0];

        // �̺�Ʈ �ڵ忡 ���� �б�
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
    /// [OnEvent] ȭ�� �˰� ó�� <br></br>
    /// �Ű����� X
    /// </summary>
    /// <param name="para"></param>
    private void FadeOut(ExtraParams para)
    {
        screenFade.SetScreenFade(false);
    }

    /// <summary>
    /// [OnEvent] �˰� �� ȭ�� ���� <br></br>
    /// �Ű����� X
    /// </summary>
    /// <param name="para"></param>
    private void FadeIn(ExtraParams para)
    {
        screenFade.SetScreenFade(true);
    }

    /// <summary>
    /// [OnEvent] Ű���� Ʃ�丮�� �ε������� ��� <br></br>
    /// �Ŀ� Ʃ�丮�� ������Ʈ�� ���� ��ų ���� <br></br>
    /// �Ű����� X
    /// </summary>
    /// <param name="para"></param>
    private void KeyBoardIndicator(ExtraParams para)
    {
        indicator_keyboard.SetAnim("Out");
    }

    /// <summary>
    /// [OnEvent] ��� ���� ������ ��� �ִ� �̹��� ���<br></br>
    /// �Ŀ� Ʃ�丮�� ������Ʈ�� ���� ��ų ���� <br></br>
    /// bool : Ȱ��ȭ ����
    /// </summary>
    /// <param name="para"></param>
    private void SetCenterIndicator(ExtraParams para)
    {
        indicator_centerImage.gameObject.SetActive(para.Boolvalue);
    }

    /// <summary>
    /// [OnEvent] Scene ��ȯ <br></br>
    /// Name : ��ȯ�� Scene �̸� <br></br>
    /// Int : StageContainer�� CurID����(�� ��ȣ)
    /// </summary>
    /// <param name="para"></param>
    private void ForceLoad(ExtraParams para)
    {
        LoadingSceneController.LoadScene(para.Name, para.Intvalue);
    }

    /// <summary>
    /// [OnEvent] Ʃ�丮�� �� ���� �� �ΰ� ���� �ִϸ��̼� ��� <br></br>
    /// �Ű����� X
    /// </summary>
    /// <param name="para"></param>
    private void LogoAppears(ExtraParams para)
    {
        tutorialLogo.SetAnim("isAppear");
    }

    /// <summary>
    /// [OnEvent] ���콺 Ʃ�丮�� �ε������� ��� <br></br>
    /// �Ŀ� Ʃ�丮�� ������Ʈ�� ���� ��ų ���� <br></br>
    /// �Ű����� X
    /// </summary>
    /// <param name="para"></param>
    private void SetMouseIndicator(ExtraParams para)
    {
        indicator_mouse.SetAnim("Out");
    }

    /// <summary>
    /// [OnEvent] ������ ���� ���â�� ��� <br></br>
    /// bool : true�� �� ��ü���� �ִϸ��̼��� �����ϰ� ���� ���
    /// </summary>
    /// <param name="para"></param>
    private void ActiveResultWindow(ExtraParams para)
    {
        if (resultWindow == null)
            return;

        bool isRewardOnly = para.Boolvalue;
        
        // ���â ������Ʈ Ȱ��ȭ
        resultWindow.gameObject.SetActive(true);
        
        // ���â �ִϸ��̼� ��ü ���
        if (!isRewardOnly)
            resultWindow.Active_Full();
        // ���â ���� �ִϸ��̼Ǹ� ���
        else
            resultWindow.Active_RewardOnly();
    }

    /// <summary>
    /// [OnEvent] ���� �й� ������ ��� <br></br>
    /// �Ű����� X
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
    /// [OnEvent] ���� ���۽� �� ���� ��� <br></br>
    /// �Ű����� X, LoadDataSingleton���� stageInfoContainer�� �ҷ��� ���
    /// </summary>
    /// <param name="para"></param>
    private void ActiveStageWindow(ExtraParams para)
    {
        if (stageinfoText == null)
            return;

        // LoadDataSingleton���κ��� �ҷ��� stageInfoContainer���� �ʹ�ȣ(id), stageName(���̸�), stageCondition(Ŭ���� ����)�� ������
        int id = stageInfoContainer.CurID;
        string stageName = stageInfoContainer.StageInfoList[id].StageName;
        string stageCondition = stageInfoContainer.StageInfoList[id].StageSort.ToString();
        switch (stageInfoContainer.StageInfoList[id].StageSort)
        {
            case StageSort.None:
                stageCondition = ""; break;
            case StageSort.Timer:
                stageCondition = "���ѽð����� ����"; break;
            case StageSort.targetDestroy:
                stageCondition = "��ǥ ����"; break;
            case StageSort.infinite:
                stageCondition = "��Ƴ�������"; break;
        }

        // ������ ������ stageinfoText��⿡ ���� �� ����
        stageinfoText.gameObject.SetActive(true);
        stageinfoText.Active(stageName, stageCondition);
    }

    /// <summary>
    /// [OnEvent] ��ܿ� �ִ� ��κ��� UI�� Ȱ��ȭ ���� ���� <br></br>
    /// int : ��Ŭ���� ����(stageInfoContainer.StageInfoList[i].StageSort)�� StageSort.targetDestroy(��ǥ�� ���� �� �˻�)�� ��� �� ������ ����
    /// </summary>
    /// <param name="para"></param>
    private void ActiveTopIndicator(ExtraParams para)
    {
        // UnitManager�� ������ ���� ���� �˻��ϴ� ����̸�, Event Maker�� Condition���� AllUnit �Ű������� �˻簡��
        UnitManager.Instance.MaxDestroyed = para.Intvalue;
        topIndicator.Sort = stageInfoContainer.StageInfoList[stageInfoContainer.CurID].StageSort;
        topIndicator.SetActive();
    }

    /// <summary>
    /// [OnEvent] �̸����� �ε������͸� ã�� Ȱ��ȭ ���� ���� <br></br>
    /// Name : ã�� �ε��������� ������Ʈ �̸�
    /// bool : Ȱ��ȭ ����
    /// VecList[0] : ǥ�õ� ��ġ
    /// </summary>
    /// <param name="para"></param>
    private void SetActiveIndicator(ExtraParams para)
    {
        string indicator_name = para.Name;
        bool isActive = para.Boolvalue;
        Vector2 pos = para.VecList.Count > 0 ? para.VecList[0] : Vector2.zero;

        // �ε������� ����Ʈ���� �̸��� �˻�, �� ���� ó���� �ε������Ϳ� ����� IndicatorŬ������ ����
        foreach (GameObject ind in indicatorList)
        {
            if (string.Equals(ind.gameObject.name, indicator_name))
            {
                ind.GetComponent<Indicator>().SetActive(isActive, pos);
            }
        }
    }

    /// <summary>
    /// [OnEvent] ��� UIȰ��ȭ ���� ����
    /// bool : Ȱ��ȭ ����
    /// </summary>
    /// <param name="para"></param>
    private void SetActiveUI(ExtraParams para)
    {
        topIndicator.gameObject.SetActive(para.Boolvalue);
        hpIndicator.gameObject.SetActive(para.Boolvalue);
    }
    
    /// <summary>
    /// [OnEvent] ȭ�� �߽ɿ� UI���
    /// Name : ����� �ؽ�Ʈ
    /// int : ��Ʈ ������ (�⺻�� 70, 0�Է� �� ����)
    /// float : ����� ���ӽ�ų �ð� (�⺻�� 3��, 0�Է� �� ����)
    /// bool : true�� ���� ��Ȱ��ȭ
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
