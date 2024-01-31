using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Default Event Phase", menuName = "Scriptable Object/Event Phase", order = int.MaxValue)]
public class EventPhase_so : ScriptableObject
{
    [SerializeField] private List<EventParams> events = new List<EventParams>();
    public List<EventParams> Events { get => events; set => events = value; }

    public void UnSatisfyAll()
    {
        foreach (EventParams e in events)
        {
            e.condition.IsSatisfied = false;
            e.isSatisfied = false;
        }
        
    }
}
