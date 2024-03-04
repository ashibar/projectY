using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// <b>��� StageManager ���</b> <br></br>
/// ����� : �̿�� <br></br>
/// ��� : �������� ������ �� ������� ���� <br></br>
/// ��� :  <br></br>
/// ������Ʈ ���� :  <br></br>
///  - (23.08.22) : ��๮ ����  <br></br>
///  <br></br>
/// </summary>

public class StageManager : MonoBehaviour, IEventListener
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
    // ���� ���
    [SerializeField] private ConditionChecker conditionChecker;
    [SerializeField] private EventTimer eventTimer;
    [SerializeField] private StageEndCheck stageEndCheck;
    // �������� ����
    [SerializeField] private StageInfoContainer_so stageInfoContainer_so;
    [SerializeField] private StageInfo_so stageInfo_so;
    [SerializeField] private StageInfo stageInfo;
    // �÷��̾� ����
    [SerializeField] public PlayerInfoContainer playerInfoContainer_so;
    [SerializeField] public SpellPrefabContainer SpellPrefabContainer_so;

    // ���� ���    
    public ConditionChecker ConditionChecker { get => conditionChecker; set => conditionChecker = value; }
    public EventTimer EventTimer { get => eventTimer; set => eventTimer = value; }
    public StageEndCheck StageEndCheck { get => stageEndCheck; set => stageEndCheck = value; }

    // �����
    [SerializeField] private bool verbose;

    public static List<string> event_code = new List<string>
    {
        "Goto Next Phase",
        "Force Load",
        "Set Stage Info By ID",
        "Set Next Stage Of Infinite Mode",
    };

    /// <summary>
    /// <b>��ũ��Ʈ ù Ȱ��ȭ �� ���� ���� ����Ǵ� �Լ�.</b><br></br>
    /// <br></br>
    /// - �̱��� ó�� <br></br>
    /// - ������ �ε� <br></br>
    /// - ���� ��� �ε� <br></br>
    /// - �̺�Ʈ ���� <br></br>
    /// </summary>
    private void Awake()
    {
        // �̱��� ������Ʈ ����ȭ
        var objs = FindObjectsOfType<StageManager>();
        if (objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }

        // ����׾� ���� ���� ó��
        // ����׾��ΰ��� �����Ǿ��� �� StageInfoContainer�� ����׾� �������� ��ü
        if (SceneManager.GetActiveScene().name == "Debug_Scene")
            LoadDataSingleton.Instance.SetStageInfoContainer("Debug_Scene");
        
        // LoadDataSingleton���κ��� �����͸� �ҷ���
        stageInfoContainer_so = LoadDataSingleton.Instance.StageInfoContainer();
        playerInfoContainer_so = LoadDataSingleton.Instance.PlayerInfoContainer();
        SpellPrefabContainer_so = LoadDataSingleton.Instance.SpellPrefabContainer();
        
        // ���� ��� �ҷ���
        conditionChecker = GetComponentInChildren<ConditionChecker>();
        eventTimer = GetComponentInChildren<EventTimer>();
        stageEndCheck = GetComponentInChildren<StageEndCheck>();

        // ��ü �ð� ���� �ʱ�ȭ
        Time.timeScale = 1.0f;

        // �̺�Ʈ ����
        SubscribeEvent();
    }

    /// <summary>
    /// <b>��ũ��Ʈ ù Ȱ��ȭ �� �ι�°�� ����Ǵ� �Լ�.</b><br></br>
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

        // �ڡڡ�
        // StageInfoContainer�� EventTimer��⿡ ���� �� �̺�Ʈ ������ ���� �񵿱� �����ƾ ����
        SetStageInfo(stageInfoContainer_so.StageInfoList[stageInfoContainer_so.CurID]);

        // �ڡڡ�
        // LoadDataSingleton���κ��� ���� ����� ���� ������ �ҷ��� �� ���� �� ����
        LoadPlayerSpell();

        // ��� ü�¹� ǥ�ø� ���� �ֿ� �� ����
        SetTargetUnit();
    }

    private void Update()
    {
        
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
    /// <b>[OnEvent] ���� Phase�� ��ȯ</b> <br></br>
    /// NextPhase : ��ȯ�� ���� Phase(EventPhase_so) <br></br>
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
    /// <b>[OnEvent] ���̸��� �Ű������� �޾� ���� �ε�</b> <br></br>
    /// Name : �ε� �� �� �̸� <br></br>
    /// Int : �� ��ȣ�� �ʿ��� ���, �� ��ȣ
    /// </summary>
    /// <param name="para"></param>
    private void ForceLoad(ExtraParams para)
    {
        LoadingSceneController.LoadScene(para.Name, para.Intvalue);
    }

    /// <summary>
    /// <b>[OnEvent] �� ��ȣ�� �޾� BattleScene�� �̺�Ʈ Ÿ�̸� �����ƾ ����</b> <br></br>
    /// Int : �� ��ȣ
    /// </summary>
    /// <param name="para"></param>
    private void SetStageInfoByID(ExtraParams para)
    {
        SetStageInfo(stageInfoContainer_so.StageInfoList[Mathf.Clamp(para.Intvalue, 0, stageInfoContainer_so.StageInfoList.Count)]);
    }

    /// <summary>
    /// <b>[OnEvent] ���Ѹ�� �� �������� ����, �̺�Ʈ �����ƾ ����</b> <br></br>
    /// �Ű����� X
    /// PlayerInfoContainer�� progress_step_infinite���� ������ ����
    /// </summary>
    /// <param name="para"></param>
    private void SetNextStageOfInfiniteMode(ExtraParams para)
    {
        LoadDataSingleton.Instance.PlayerInfoContainer().Progress_step_infinite += 1;
        int progress = LoadDataSingleton.Instance.PlayerInfoContainer().Progress_step_infinite;
        SetStageInfo(stageInfoContainer_so.StageInfoList[Mathf.Clamp(progress, 0, stageInfoContainer_so.StageInfoList.Count - 1)]);
    }

    /// <summary>
    /// <b>�ε� �� StageManager�� �������� ������ �ֱ� ���� �Լ�</b> <br></br>
    /// </summary>
    /// <param name="_stageInfo_so">�������� ������ so����</param>
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
    /// <para/> <b>�ε� �� �÷��̾��� ������ �ҷ���</b>
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
    /// �ӽ� �Ķ���� ���� �Լ�
    /// �Ŀ� �ε� ��� ������Ʈ�� ��ü��
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
    /// �׽�Ʈ�� ���â ������ ��� �Լ� <br/>
    /// �Ŀ� ��ü ����
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
    /// ���â �����쿡�� ������ ���� �������� �̵� �Լ� <br/>
    /// �Ŀ� ��ü ����
    /// </summary>
    public void GoNextStage()
    {
        stageInfoContainer_so.CurID += 1;
        int next = stageInfoContainer_so.CurID;
        LoadingSceneController.LoadScene("BattleScene", stageInfoContainer_so.CurID);
    }

    /// <summary>
    /// ��� ü�¹� ǥ�ø� ���� �ֿ� �� ���� <br/>
    /// �Ŀ� ��ü �� �̵� ����
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

    // ���� �ڵ�

    // �������� ������ �̺�Ʈ ��ȸ �Լ�
    //[SerializeField] private bool isCooltime;
    //[SerializeField] private int eventIndex = 0;
    //[SerializeField] private int interruptedIndex = -1;
    //private async void Async_Function()
    //{
    //    // ���Ἲ
    //    if (stageInfo == null)
    //        return;

    //    // ������Ʈ
    //    if (!isCooltime && eventIndex < stageInfo.EventList.Count)
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
    //// �ı��� ��� �̺�Ʈ �ߴ�
    //private void OnDestroy()
    //{
    //    foreach (EventInfo e in stageInfo.EventList)
    //    {
    //        e.Isinterrupted = true;
    //    }
    //}

    // ���� �ʱ�ȭ �Լ�
    //public void SetStageInfo(StageInfo_so _stageInfo_so)
    //{
    //    stageInfo_so = _stageInfo_so;
    //    stageInfo = new StageInfo(_stageInfo_so);
    //    conditionChecker.Events.AddRange(stageInfo.EventList);
    //}
}
