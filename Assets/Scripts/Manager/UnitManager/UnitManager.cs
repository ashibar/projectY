using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> clones = new List<GameObject>();
    private Unit unit;
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
    private void Start()
    {
        if (unit == null)
        {
            unit = Player.instance;
        }
        clones.Add(unit.gameObject);
    }
    private void Update()
    {
        EventReciever();
        EventListener();
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

    [SerializeField] private List<EventMessage> messageBuffer = new List<EventMessage>();
    private void EventReciever()
    {
        int error = StageManager.Instance.SearchMassage(5, messageBuffer);
        if (error == -1)
            return;
    }

    private void EventListener()
    {
        bool isError = false;
        foreach (EventMessage m in messageBuffer)
        {
            switch (m.ActionSTR)
            {
                case "Fade Out":
                    
                    break;
                case "Fade In":
                    
                    break;
                default:
                    isError = true;
                    break;
            }

            if (!isError)
            {
                messageBuffer.Remove(m);
            }
        }

    }
}
