using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionChecker : MonoBehaviour
{
    [SerializeField] private List<EventInfo> events;
    [SerializeField] private List<string> flags;
    [SerializeField] private List<ConditionArea> areas;

    public List<EventInfo> Events { get => events; set => events = value; }
    public List<string> Flags { get => flags; set => flags = value; }

    private void Awake()
    {
        areas.AddRange(GetComponentsInChildren<ConditionArea>());
    }

    private void Update()
    {
        ConditionCheck();
    }

    private void ConditionCheck()
    {
        foreach (EventInfo e in events)
        {
            switch (e.Condition.Sort)
            {
                case ConditionSort.None:
                    SendToTimer(e);
                    return;
                case ConditionSort.Trigger:
                    foreach (string f in flags)
                    {
                        if (string.Equals(f, e.Condition.TargetFlag))
                        {
                            flags.Remove(f);
                            SendToTimer(e);
                            return;
                        }
                    }
                    break;
                case ConditionSort.MoveToArea:
                    if (areas[e.Condition.TargetAreaID].TargetExists)
                    {
                        SendToTimer(e);
                        return;
                    }
                    break;
            }
        }
        
    }
    private void SendToTimer(EventInfo eventInfo)
    {
        StageManager.Instance.EventTimer.Events.Add(eventInfo);
        events.Remove(eventInfo);
    }
}
