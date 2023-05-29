using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ũ��Ʈ �̸� : SpawnManager
/// ����� : �̿��
/// ��� : ������ ���� ��ũ��Ʈ
/// ��� : verbose üũ�� p�� ���� �׽�Ʈ�� ������Ʈ ����
/// ������Ʈ ���� :
///     - (23.03.25) : ��ũ��Ʈ ����
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

    [SerializeField]
    private List<Spawner> spawner = new List<Spawner>();

    [SerializeField]
    private float range_test = 10;

    [SerializeField]
    private bool verbose = false;
    [SerializeField]
    private bool iscoroutineRunning = false;


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
        spawner.AddRange(GetComponentsInChildren<Spawner>());
    }

    private void Update()
    {
        TestSpawn();
        EventReciever();
        if (!iscoroutineRunning)
        {
            StartCoroutine(mobspawn());
        }
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
        foreach (EventMessage m in messageBuffer)
        {
            switch (m.ActionSTR)
            {
                case "Active Spawner":
                    // 0���ൿ;
                    if (!iscoroutineRunning)
                    {
                        StartCoroutine(mobspawn());
                    }
                    break;
                case "InActive Spawner":
                    // 1���ൿ;
                    Bossspawn();
                    break;

            }
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
    IEnumerator mobspawn()
    {
        iscoroutineRunning = true;
        for (int i = 0; i < spawner[0].amount; i++)
        {
            spawner[0].Spawn_Enemy_AtPosition(0, spawner[0].spawnMain[0].SpawnRangePoint(15));
        }

        yield return new WaitForSeconds(spawner[0].spawn_cooltime); //���߿��ٸ�  ���������� while ������ �޾� üũ�ؼ� ���� ųī��Ʈ�� �ð�?
        iscoroutineRunning = false;
    }
    private void Bossspawn()
    {
        if (verbose) 
        {
            spawner[0].Spawn_Enemy_AtPosition(0, new Vector2(0, 10));
            //ĳ�������� ��ܿ� ���� ��ȯ
        }
    }
}
