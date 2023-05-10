using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultEventInfo", menuName = "Scriptable Object/EventInfo", order = int.MaxValue)]
public class EventInfo_so : ScriptableObject
{
    [SerializeField] private int id;
    [SerializeField] private EventSort sort;
    [SerializeField] private bool isLoop;
    [SerializeField] private bool isSequential;
    [SerializeField] private bool isRequires;
    [SerializeField] private float durationToStart;
    [SerializeField] private int message;

    public int Id { get => id; set => id = value; }
    public EventSort Sort { get => sort; set => sort = value; }
    public bool IsLoop { get => isLoop; set => isLoop = value; }
    public bool IsSequential { get => isSequential; set => isSequential = value; }
    public bool IsRequires { get => isRequires; set => isRequires = value; }
    public float DurationToStart { get => durationToStart; set => durationToStart = value; }
    public int Message { get => message; set => message = value; }
}
