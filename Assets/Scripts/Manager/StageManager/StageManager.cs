using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    // ���� ���
    [SerializeField] private ConditionChecker conditionChecker;
    [SerializeField] private EventTimer eventTimer;
    [SerializeField] private StageEndCheck stageEndCheck;
    // �������� ����
    [SerializeField] private StageInfoContainer_so stageInfoContainer_so;
    [SerializeField] private StageInfo_so stageInfo_so;
    [SerializeField] private StageInfo stageInfo;

    public ConditionChecker ConditionChecker { get => conditionChecker; set => conditionChecker = value; }
    public EventTimer EventTimer { get => eventTimer; set => eventTimer = value; }
    public StageEndCheck StageEndCheck { get => stageEndCheck; set => stageEndCheck = value; }

    public List<EventMessage> messageBuffer = new List<EventMessage>();


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
        
        SetStageInfo(stageInfoContainer_so.StageInfoList[stageInfoContainer_so.CurID]); // ���߿� �ε��Ҷ� ��ü
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

    // �ε� �� StageManager�� �������� ������ �ֱ� ���� �Լ�
    public void SetStageInfo(StageInfo_so _stageInfo_so)
    {
        stageInfo_so = _stageInfo_so;
        stageInfo = new StageInfo(_stageInfo_so);
        conditionChecker.Events.AddRange(stageInfo.EventList);
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

    public void GoNextStage()
    {
        stageInfoContainer_so.CurID += 1;
        int next = stageInfoContainer_so.CurID;
        LoadingSceneController.LoadScene("BattleScene", stageInfoContainer_so.CurID);
    }

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
}
