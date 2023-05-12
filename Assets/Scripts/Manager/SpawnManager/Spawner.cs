using System.Collections;
using System.Collections.Generic;
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
    [SerializeField]
    private List<GameObject> original = new List<GameObject>();

    [SerializeField]
    private List<GameObject> clones = new List<GameObject>();

    [SerializeField]
    public List<SpawnMain> spawnMain = new List<SpawnMain>(); 

    // 외부에서 스포너의 오리지널 프리팹을 추가하고자 할때 사용하는 함수
    public virtual void AddOriginal(List<GameObject> original_list)
    {
        original.AddRange(original_list);
    }

    // 특정 위치에 적 오브젝트를 소환하고 내부 clones리스트에 오브젝트를 추가
    public virtual void Spawn_Enemy_AtPosition(int id, Vector2 pos)
    {
        if (!Integrity_Check(id)) return;

        GameObject clone = Instantiate(original[id], pos, Quaternion.identity);
        clone.GetComponent<Enemy>().spawner_pointer = this;
        clone.name = clones.Count.ToString();

        clones.Add(clone);
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
