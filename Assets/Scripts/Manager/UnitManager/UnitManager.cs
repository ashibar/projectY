using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
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

    [SerializeField] private PlayerAnimationController playerAnimationController;
    [SerializeField] private List<GameObject> clones = new List<GameObject>();
    private Unit unit;
    public List<GameObject> Clones { get => clones; }

    private void Awake()
    {
        playerAnimationController = GetComponentInChildren<PlayerAnimationController>();
    }

    private void Start()
    {
        if (unit == null)
        {
            unit = Player.Instance;
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
        int error = StageManager.Instance.SearchMassage(3, messageBuffer);
        
        if (error == -1)
            return;
    }

    private void EventListener()
    {
        bool isError = false;
        EventMessage temp = new EventMessage();

        if (messageBuffer.Count == 0)
            return;
        else
            Debug.Log(messageBuffer.Count);

        foreach (EventMessage m in messageBuffer)
        {
            temp = m;
            switch (m.ActionSTR)
            {
                case "Player Move":
                    unit.GetComponentInChildren<UnitForceMove>().SetForceMove(unit, m.TargetPOS, m.TargetNUM);
                    unit.GetComponentInChildren<UnitForceMove>().IsForceMove = true;
                    messageBuffer.Remove(m);
                    return;
                case "Player Stop":
                    unit.GetComponentInChildren<UnitForceMove>().IsForceMove = false;
                    messageBuffer.Remove(m);
                    return;
                case "Player Move Input":
                    unit.GetComponent<Player>().playerMovement.IsMove = string.Equals(m.TargetSTR, "true");
                    Debug.Log(string.Equals(m.TargetSTR, "true"));
                    messageBuffer.Remove(m);
                    return;
                case "Player Animation":
                    playerAnimationController.SetAnimation(m.TargetSTR);
                    messageBuffer.Remove(m);
                    return;
                default:
                    isError = true;
                    break;
            }
        }

        if (!isError)
        {
            messageBuffer.Remove(temp);
        }
    }

    private void UnitForceMove(Unit _unit, Vector2 targetPos, float speed, EventMessage m)
    {
        Vector2 pos = _unit.transform.position;
        Vector2 dir = (targetPos - pos).normalized;

        float distanceToTarget = (targetPos - pos).magnitude;

        // 등속 이동
        if (distanceToTarget > 0f)
        {
            if (speed == 0)
                _unit.transform.position = Vector2.MoveTowards(pos, targetPos, _unit.stat.Speed * Time.deltaTime);
            else
                _unit.transform.position = Vector2.MoveTowards(pos, targetPos, speed * Time.deltaTime);
        }
        //else
            //messageBuffer.Remove(m);
    }
}
