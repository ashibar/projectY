using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EventInfo
{
    [SerializeField] protected int id;
    [SerializeField] protected EventSort sort;
    [SerializeField] protected bool isLoop;
    [SerializeField] protected bool isSequential;
    [SerializeField] protected bool isRequires;
    [SerializeField] private Condition condition;
    [SerializeField] protected float durationToStart;
    [SerializeField] protected EventMessage message;

    [SerializeField] protected StageManager manager;
    [SerializeField] protected bool isinterrupted;
    [SerializeField] protected bool isRequired;

    public EventInfo(EventInfo_so info)
    {
        this.id = info.Id;
        this.sort = info.Sort;
        this.isLoop = info.IsLoop;
        this.isSequential = info.IsSequential;
        this.isRequires = info.IsRequires;
        this.condition = info.Condition;
        this.durationToStart = info.DurationToStart;
        this.message = info.Message;
    }


    public int Id { get => id; set => id = value; }
    public EventSort Sort { get => sort; set => sort = value; }
    public bool IsLoop { get => isLoop; set => isLoop = value; }
    public bool IsSequential { get => isSequential; set => isSequential = value; }
    public bool IsRequires { get => isRequires; set => isRequires = value; }
    public Condition Condition { get => condition; set => condition = value; }
    public float DurationToStart { get => durationToStart; set => durationToStart = value; }
    public EventMessage Message { get => message; set => message = value; }
    public StageManager Manager { get => manager; set => manager = value; }
    public bool Isinterrupted { get => isinterrupted; set => isinterrupted = value; }
    public bool IsRequired { get => isRequired; set => isRequired = value; }
}
