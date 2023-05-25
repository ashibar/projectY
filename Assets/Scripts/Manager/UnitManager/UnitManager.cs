using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    private List<GameObject> clones = new List<GameObject>();
    public List<GameObject> Clones { get => clones;}
    private static UnitManager instance;
    public static UnitManager Instance
    {
        get
        {
            if(instance == null)
            {
                var obj = FindObjectOfType<UnitManager>();
                if(obj != null)
                {
                    instance = obj;
                }
                else
                {
                    var newObj = new GameObject().AddComponent<UnitManager>();
                    instance = newObj;
                }
            }
            return instance;
        }
    }

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
    }

    private int Clone_Comparer(GameObject clone)
    {
        for (int i = 0; i < clones.Count; i++)
        {
            if (clone == clones[i]) return i;
        }
        return -1;
    }
}
