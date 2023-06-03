using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 스크립트 이름 : Spawner
/// 담당자 : 이용욱
/// 요약 : 오브젝트 스폰 스크립트
/// 비고 : 
/// 업데이트 내역 :
///     - (23.03.25) : 스크립트 생성
/// </summary>

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> original = new List<GameObject>();
    [SerializeField] private List<GameObject> clones = new List<GameObject>();
    [SerializeField] public List<SpawnMain> spawnMain = new List<SpawnMain>();
    [SerializeField] public int amount;
    [SerializeField] public float radius;
    [SerializeField] public SpawnerSort spawnerSort = SpawnerSort.Point;

    private void Awake()
    {
        spawnMain.Clear();
        spawnMain.AddRange(SpawnManager.Instance.spawnMain_holder.GetComponentsInChildren<SpawnMain>());
    }

    protected virtual void Update()
    {
        if (IsActive)
            LoopBody();
    }

    // 외부에서 스포너의 오리지널 프리팹을 추가하고자 할때 사용하는 함수
    public virtual void AddOriginal(List<GameObject> original_list)
    {
        original.AddRange(original_list);
    }

    // ** 호출 즉시 소환 함수 영역 **
    // SpawnerSort설정값에 따라 작동
    public virtual void SpawnForce()
    {
        switch (spawnerSort)
        {
            case SpawnerSort.Point:
                Spawn_Enemy_AtPositionOnce(0, spawnMain[0].spawnpoint);
                break;
            case SpawnerSort.Circle:
                Spawn_Enemy_AtCircle();
                break;
            case SpawnerSort.Straight:
                break;
        }
    }
    // 특정 위치에 적 오브젝트를 소환하고 내부 clones리스트에 오브젝트를 추가
    public virtual void Spawn_Enemy_AtPosition(int id, Vector2 pos)
    {
        if (!Integrity_Check(id)) return;

        GameObject clone = Instantiate(original[id], pos, Quaternion.identity, Holder.enemy_holder);
        clone.GetComponent<Enemy>().spawner_pointer = this;
        clone.name = clones.Count.ToString();
        
        //clones.Add(clone);
        UnitManager.Instance.Clones.Add(clone);
    }

    // 위의 함수를 SpawnPoint에 지정된 모든 위치에 한번에 소환
    public virtual void Spawn_Enemy_AtPositionOnce(int id, Transform[] spawnPoint)
    {
        foreach (Transform t in spawnPoint)
            Spawn_Enemy_AtPosition(0, t.position);
    }

    // 특정 위치에서 반지름을 기준으로 원 테두리에 적 오브젝트 추가
    public virtual void Spawn_Enemy_AtCircle()
    {
        for (int i = 0; i < amount; i++)
            Spawn_Enemy_AtPosition(0, spawnMain[0].SpawnRangePoint(radius));
    }

    // 클론 리스트에서 클론을 찾아 제거    
    public virtual void Delete_FromCloneList(GameObject clone)
    {
        int id = Clone_Comparer(clone);
        
        if (id < 0)
        {
            Debug.Log("out of range or not in list: " + id.ToString());
            return;
        }
        else
        {
            clones.RemoveAt(id);
        }
        //Debug.Log("id : " + id.ToString());
    }



    // ** 루프 함수 영역 **

    [SerializeField] public float spawn_cooltime;
    [SerializeField] private bool isActive = false;
    [SerializeField] private bool isCooltime;
    [SerializeField] private bool isInterrupted;
    public bool IsActive { get => isActive; set => isActive = value; }
    public List<GameObject> Original { get => original; set => original = value; }
    public List<GameObject> Clones { get => clones; set => clones = value; }

    // SpawnerSort설정값에 따른 루프 함수 실행
    protected virtual async void LoopBody()
    {
        if (!isCooltime)
        {
            isCooltime = true;
            switch (spawnerSort)
            {
                case SpawnerSort.Point:
                    await Spawn_Enemy_AtPositionOnce_Loop(spawn_cooltime);
                    break;
                case SpawnerSort.Circle:
                    await Spawn_Enemy_AtCicle_Loop(spawn_cooltime);
                    break;
                case SpawnerSort.Straight:
                    await Task.Yield(); // 미구현
                    break;
            }
            isCooltime = false;
        }

        if (isInterrupted)
            isInterrupted = false;
    }

    protected virtual async Task Spawn_Enemy_AtPositionOnce_Loop(float duration)
    {
        float end = Time.time + duration;

        Spawn_Enemy_AtPositionOnce(0, spawnMain[0].spawnpoint);

        while (Time.time < end)
        {
            if (isInterrupted)
            {
                await Task.FromResult(0);
            }
            await Task.Yield();
        }
    }

    protected virtual async Task Spawn_Enemy_AtCicle_Loop(float duration)
    {
        float end = Time.time + duration;

        Spawn_Enemy_AtCircle();

        while (Time.time < end)
        {
            if (isInterrupted)
            {
                await Task.FromResult(0);
            }
            await Task.Yield();
        }
    }

    public void SetActive(bool value)
    {
        if (value)
        {
            isActive = true;
        }
        else
        {
            IsActive = false;
            isInterrupted = true;
        }
    }

    private void OnDestroy()
    {
        SetActive(false);
    }

    // ** 탐색 및 무결성 영역 **
    // 클론 탐색 함수
    // 추후 이진 탐색으로 변경예정
    private int Clone_Comparer(GameObject clone)
    {
        for (int i = 0; i < clones.Count; i++)
        {
            if (clone == clones[i]) return i;
        }
        return -1;
    }

    // 무결성 검사 함수
    // 다음 상황에서 false를 반환 :
    //      - Original 리스트가 비어 있음
    //      - id가 리스트 범위에서 벗어남
    //      - id의 Gameobject가 null임
    // 사용 예시 :
    //      if (!Integrity_Check(id)) return;
    private bool Integrity_Check(int id)
    {
        if (original.Count <= 0)
        {
            Debug.Log("Original list is Empty.");
            return false;
        }
        else if (original.Count <= id)
        {
            Debug.Log("ID is out of Original list's Range. Length : " + original.Count.ToString() + ", ID : " + id.ToString());
            return false;
        }
        else if (original[id] == null)
        {
            Debug.Log("Original in ID's Gameobject is Null, ID : " + id.ToString());
            return false;
        }
        else
            return true;
    }
}

public enum SpawnerSort
{
    Point,
    Circle,
    Straight
}
