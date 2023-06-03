using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 스크립트 이름 : SpawnManager
/// 담당자 : 이용욱
/// 요약 : 스포너 관리 스크립트
/// 비고 : verbose 체크후 p를 눌러 테스트용 오브젝트 생성
/// 업데이트 내역 :
///     - (23.03.25) : 스크립트 생성
/// </summary>

public class SpawnManager : MonoBehaviour
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
        
    }

    private void Start()
    {
        
    }

    public void SetSpawner()
    {
        spawner.AddRange(GetComponentsInChildren<Spawner>());
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
                case "Force Spawn": // TargetNum에 스포너 번호를 받음
                    spawner[(int)m.TargetNUM].SpawnForce();
                    messageBuffer.Remove(m);
                    return;
                case "SetActive Spawner": // TargetNum에 스포너 번호를 받으며, TargetSTR에 true/false로 활성/비활성 설정
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
    /*
     Radius를 변수로 뺌
     **/
    //IEnumerator mobspawn()
    //{
    //    iscoroutineRunning = true;
    //    for (int i = 0; i < spawner[0].amount; i++)
    //    {
    //        //
    //        spawner[0].Spawn_Enemy_AtPosition(0, spawner[0].spawnMain[0].SpawnRangePoint(radius));
 
    //    }

    //    yield return new WaitForSeconds(spawner[0].spawn_cooltime); //나중에다른  종료조건을 while 문으로 받아 체크해서 종료 킬카운트나 시간?
    //    iscoroutineRunning = false;
    //}
    //private void Bossspawn()
    //{
    //    if (verbose) 
    //    {
    //        spawner[0].Spawn_Enemy_AtPosition(0, new Vector2(0, 10));
    //        //캐릭터정면 상단에 보스 소환
    //    }
    //}
}
