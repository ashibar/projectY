using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultEventInfo", menuName = "Scriptable Object/EventInfo", order = int.MaxValue)]
public class EventInfo_so : ScriptableObject
{
    [SerializeField] private int id;
    [SerializeField] private EventSort sort;
    [SerializeField] private Condition condition;
    [SerializeField] private bool isLoop;
    [SerializeField] private float durationToStart;
    [SerializeField] private EventMessage message;

    public int Id { get => id; set => id = value; }
    public EventSort Sort { get => sort; set => sort = value; }
    public bool IsLoop { get => isLoop; set => isLoop = value; }
    public Condition Condition { get => condition; set => condition = value; }
    public float DurationToStart { get => durationToStart; set => durationToStart = value; }
    public EventMessage Message { get => message; set => message = value; }
}
