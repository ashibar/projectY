using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

/// <summary>
/// <para/><b>��� SpawnManager ���</b>
/// <para/>����� : �̿��
/// <para/>��� : ������ ���� ��ũ��Ʈ
/// <para/>��� : verbose üũ�� p�� ���� �׽�Ʈ�� ������Ʈ ����
/// <para/>������Ʈ ���� :
/// <para/> - (23.03.25) : ��ũ��Ʈ ����
/// </summary>

public class SpawnManager : MonoBehaviour, IEventListener
{
    private static SpawnManager instance;
    public static SpawnManager Instance
    {
        get
        {
            if (instance == null)
            {
                var obj = FindObjectOfType<SpawnManager>();
                if (obj != null)
                {
                    instance = obj;
                }
                else
                {
                    var newObj = new GameObject().AddComponent<SpawnManager>();
                    instance = newObj;
                }
            }
            return instance;
        }
    }

    public List<Spawner> Spawner { get => spawner; set => spawner = value; }

    public Transform spawner_holder;
    public Transform spawnMain_holder;

    [SerializeField]
    private List<Spawner> spawner = new List<Spawner>();

    [SerializeField]
    private float range_test = 10;

    [SerializeField]
    private bool verbose = false;
    private void Awake()
    {
        var objs = FindObjectsOfType<SpawnManager>();
        if (objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }
        SubscribeEvent();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        TestSpawn();
        EventReciever();
        EventListener();
        //if (!iscoroutineRunning)
        //{
        //    if (spawner.Count > 0)
        //        StartCoroutine(mobspawn());
        //}
    }

    /// <summary>
    /// <para><b>�̺�Ʈ ���� �Լ�.</b></para>
    /// </summary>
    public void SubscribeEvent()
    {
        EventManager.Instance.AddListener(EventCode.ForceSpawn, this);
        EventManager.Instance.AddListener(EventCode.SetActiveSpawner, this);
        EventManager.Instance.AddListener(EventCode.InActiveAll, this);
        EventManager.Instance.AddListener(EventCode.BossSpawn, this);
        EventManager.Instance.AddListener(EventCode.SpawnEnemyAtVectorByID, this);
        EventManager.Instance.AddListener(EventCode.SpawnEnemyAtVectorListByID, this);
        EventManager.Instance.AddListener(EventCode.SpawnEnemyAtVectorByName, this);
    }

    public void OnEvent(EventCode event_type, Component sender, Condition condition, params object[] param)
    {
        switch (event_type)
        {
            case EventCode.ForceSpawn: // TargetNum�� ������ ��ȣ�� ����
                ForceSpawn(param); break;
            case EventCode.SetActiveSpawner: // TargetNum�� ������ ��ȣ�� ������, TargetSTR�� true/false�� Ȱ��/��Ȱ�� ����
                SetActiveSpawner(param); break;
            case EventCode.InActiveAll:
                InActiveAll(param); break;
            case EventCode.BossSpawn:
                BossSpawn(param); break;
            case EventCode.SpawnEnemyAtVectorByID:
                SpawnAtVectorID(param); break;
            case EventCode.SpawnEnemyAtVectorListByID:
                SpawnAtVectorListID(param); break;
            case EventCode.SpawnEnemyAtVectorByName:
                SpawnAtVectorName(param); break;
        }
    }

    private void ForceSpawn(params object[] param)
    {
        int id = (int)param[0];
        spawner[id].SpawnForce();
    }

    private void SetActiveSpawner(params object[] param)
    {
        int id = (int)param[0];
        bool value = (bool)param[1];
        spawner[id].SetActive(value);
    }

    private void InActiveAll(params object[] param)
    {
        foreach (Spawner s in spawner)
            s.SetActive(false);
    }

    private void BossSpawn(params object[] param)
    {
        spawner[0].Spawn_Enemy_AtPosition(0, new Vector2(0, 6.5f));
        StageManager.Instance.SetTargetUnit();
    }

    [SerializeField] private PrefabSet_so enemyList;
    private void SpawnAtVectorID(params object[] param)
    {
        ExtraParams para = (ExtraParams)param[0];
        
        int id = para.Id;
        Vector2 pos = para.VecList[0];
        
        GameObject clone = Instantiate(enemyList.prefabs[id], pos, Quaternion.identity, Holder.enemy_holder);
        clone.name = UnitManager.Instance.Clones.Count.ToString();

        UnitManager.Instance.Clones.Add(clone);
    }

    private void SpawnAtVectorListID(params object[] param)
    {
        ExtraParams para = (ExtraParams)param[0];

        int id = para.Id;
        List<Vector2> posList = para.VecList;

        foreach (Vector2 pos in posList)
        {
            GameObject clone = Instantiate(enemyList.prefabs[id], pos, Quaternion.identity, Holder.enemy_holder);
            clone.name = UnitManager.Instance.Clones.Count.ToString();
            UnitManager.Instance.Clones.Add(clone);
        }
    }

    private void SpawnAtVectorName(params object[] param)
    {
        ExtraParams para = (ExtraParams)param[0];

        int id = para.Id;
        string name = para.Name;
        Vector2 pos = para.VecList[0];

        for (int i = 0; i < enemyList.prefabs.Count; i++)
            if (string.Equals(enemyList.prefabs[i].name, name))
            {
                id = i;
                break;
            }
        
        GameObject clone = Instantiate(enemyList.prefabs[id], pos, Quaternion.identity, Holder.enemy_holder);
        clone.name = UnitManager.Instance.Clones.Count.ToString();

        UnitManager.Instance.Clones.Add(clone);
    }

    private void TestSpawn()
    {
        if (verbose)
        {
            if (Input.GetKeyDown(KeyCode.P))
                spawner[0].Spawn_Enemy_AtPosition(0, new Vector2(Random.Range(-5, 5), Random.Range(-5, 5)));
            if (Input.GetKeyDown(KeyCode.O))
                spawner[0].Spawn_Enemy_AtPosition(0, new Vector2(4, 0));
            if (Input.GetKeyDown(KeyCode.I))
                spawner[0].Spawn_Enemy_AtPosition(0, spawner[0].spawnMain[0].spawntestpoint());
            if (Input.GetKeyDown(KeyCode.U))
                spawner[0].Spawn_Enemy_AtPosition(0, spawner[0].spawnMain[0].SpawnRangePoint(range_test));
        }
    }

    public void SetSpawner()
    {
        spawner.AddRange(GetComponentsInChildren<Spawner>());
    }

    private List<EventMessage> messageBuffer = new List<EventMessage>();
    private void EventReciever()
    {
        int error = StageManager.Instance.SearchMassage(2, messageBuffer);
        if (error == -1)
            return;
    }

    private void EventListener()
    {
        bool isError = false;
        EventMessage temp = new EventMessage();
        if (messageBuffer.Count <= 0)
            return;

        if (spawner.Count <= 0)
            return;

        foreach (EventMessage m in messageBuffer)
        {
            switch (m.ActionSTR)
            {
                case "Force Spawn": // TargetNum�� ������ ��ȣ�� ����
                    spawner[(int)m.TargetNUM].SpawnForce();
                    messageBuffer.Remove(m);
                    return;
                case "SetActive Spawner": // TargetNum�� ������ ��ȣ�� ������, TargetSTR�� true/false�� Ȱ��/��Ȱ�� ����
                    spawner[(int)m.TargetNUM].SetActive(string.Equals(m.TargetSTR, "true"));
                    messageBuffer.Remove(m);
                    return;
                case "InActive All":
                    foreach (Spawner s in spawner)
                        s.SetActive(false);
                    messageBuffer.Remove(m);
                    return;
                case "Boss Spawn":
                    spawner[0].Spawn_Enemy_AtPosition(0, new Vector2(0, 6.5f));
                    StageManager.Instance.SetTargetUnit();
                    messageBuffer.Remove(m);
                    return;
            }
        }

        if (!isError)
        {
            messageBuffer.Remove(temp);
        }
    }

    
    /*
Radius�� ������ ��
**/
    //IEnumerator mobspawn()
    //{
    //    iscoroutineRunning = true;
    //    for (int i = 0; i < spawner[0].amount; i++)
    //    {
    //        //
    //        spawner[0].Spawn_Enemy_AtPosition(0, spawner[0].spawnMain[0].SpawnRangePoint(radius));

    //    }

    //    yield return new WaitForSeconds(spawner[0].spawn_cooltime); //���߿��ٸ�  ���������� while ������ �޾� üũ�ؼ� ���� ųī��Ʈ�� �ð�?
    //    iscoroutineRunning = false;
    //}
    //private void Bossspawn()
    //{
    //    if (verbose) 
    //    {
    //        spawner[0].Spawn_Enemy_AtPosition(0, new Vector2(0, 10));
    //        //ĳ�������� ��ܿ� ���� ��ȯ
    //    }
    //}
}
