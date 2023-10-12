using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// <para/><b>��� StageManager ���</b>
/// <para/>����� : �̿��
/// <para/>��� : �������� ������ �� ������� ����
/// <para/>��� : 
/// <para/>������Ʈ ���� : 
/// <para/> - (23.08.22) : ��๮ ����
/// <para/>
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

    public ConditionChecker ConditionChecker { get => conditionChecker; set => conditionChecker = value; }
    public EventTimer EventTimer { get => eventTimer; set => eventTimer = value; }
    public StageEndCheck StageEndCheck { get => stageEndCheck; set => stageEndCheck = value; }

    public List<EventMessage> messageBuffer = new List<EventMessage>();

    public static List<string> event_code = new List<string>
    {
        "Goto Next Phase",
        "Force Load",
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

        SubscribeEvent(); // ���߿� �ε��Ҷ� ��ü
        //SetStageInfo_(stageInfoContainer_so.StageInfoList[stageInfoContainer_so.CurID]);
    }

    private void Start()
    {
        //foreach (GameObject g in stageInfo.spawners)
        //{
        //    Instantiate(g, SpawnManager.Instance.spawner_holder);
        //}
        //SpawnManager.Instance.SetSpawner();

        SetStageInfo(stageInfoContainer_so.StageInfoList[stageInfoContainer_so.CurID]);
        if (UIManager.Instance.TopIndicator)
        {
            UIManager.Instance.TopIndicator.Sort = stageInfo.StageSort;
            UIManager.Instance.TopIndicator.SetActive();
        }
        SetTargetUnit();
        LoadPlayerSpell();
    }

    private void Update()
    {
        //if (UIManager.Instance.ResultWindow)
        //    TestStageClear();
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
            case "Force Load":
                ForceLoad((ExtraParams)param[0]); break;
        }
    }

    private void GotoNextPhase(ExtraParams par)
    {
        Debug.Log(par.NextPhase);
        eventTimer.AddPhase(par.NextPhase);
    }

    private void ForceLoad(ExtraParams para)
    {
        LoadingSceneController.LoadScene(para.Name, para.Intvalue);
    }

    /// <summary>
    /// <para/> <b>�ε� �� StageManager�� �������� ������ �ֱ� ���� �Լ�</b>
    /// </summary>
    /// <param name="_stageInfo_so">�������� ������ so����</param>
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
    /// <para/> <b>�ε� �� �÷��̾��� ������ �ҷ���</b>
    /// </summary>
    private void LoadPlayerSpell()
    {
        Player.Instance.spellManager.ClearSpell();
        List<StringNString> codes = playerInfoContainer_so.Spell_activated;
        List<GameObject> cloneList = new List<GameObject>();
        GameObject holder_obj = new GameObject();
        foreach (StringNString code in codes)
        {
            GameObject spell_prefab = SpellPrefabContainer_so.Search(code.string1);
            GameObject spell_clone = Instantiate(spell_prefab, holder_obj.transform);
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
    /// <b>�ٸ� ������ StageManager�� �޽������۸� �����ϱ� ���� �Լ�</b><br/>
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
