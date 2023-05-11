using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ũ��Ʈ �̸� : Spawner
/// ����� : �̿��
/// ��� : ������Ʈ ���� ��ũ��Ʈ
/// ��� : 
/// ������Ʈ ���� :
///     - (23.03.25) : ��ũ��Ʈ ����
/// </summary>

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> original = new List<GameObject>();

    [SerializeField]
    private List<GameObject> clones = new List<GameObject>();

    [SerializeField]
    public List<SpawnMain> spawnMain = new List<SpawnMain>(); 

    // �ܺο��� �������� �������� �������� �߰��ϰ��� �Ҷ� ����ϴ� �Լ�
    public virtual void AddOriginal(List<GameObject> original_list)
    {
        original.AddRange(original_list);
    }

    // Ư�� ��ġ�� �� ������Ʈ�� ��ȯ�ϰ� ���� clones����Ʈ�� ������Ʈ�� �߰�
    public virtual void Spawn_Enemy_AtPosition(int id, Vector2 pos)
    {
        if (!Integrity_Check(id)) return;

        GameObject clone = Instantiate(original[id], pos, Quaternion.identity);
        clone.GetComponent<Enemy>().spawner_pointer = this;
        clone.name = clones.Count.ToString();

        clones.Add(clone);
    }

    // Ŭ�� ����Ʈ���� Ŭ���� ã�� ����    
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

    // Ŭ�� Ž�� �Լ�
    // ���� ���� Ž������ ���濹��
    private int Clone_Comparer(GameObject clone)
    {
        for (int i = 0; i < clones.Count; i++)
        {
            if (clone == clones[i]) return i;
        }
        return -1;
    }

    // ���Ἲ �˻� �Լ�
    // ���� ��Ȳ���� false�� ��ȯ :
    //      - Original ����Ʈ�� ��� ����
    //      - id�� ����Ʈ �������� ���
    //      - id�� Gameobject�� null��
    // ��� ���� :
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
