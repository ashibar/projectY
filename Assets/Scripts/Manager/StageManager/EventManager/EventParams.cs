using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EventParams
{
    [SerializeField] public int no;
    [SerializeField] public EventCode eventcode;
    [SerializeField] public Condition condition;
    [SerializeField] public ExtraParams extraParams;

    public EventParams(int no)
    {
        this.no = no;
        this.eventcode = EventCode.None;
        this.condition = new Condition();
        this.extraParams = new ExtraParams();
    }

    public EventParams(int no, EventCode eventcode, Condition condition)
    {
        this.no = no;
        this.eventcode = eventcode;
        this.condition = condition;
        this.extraParams = new ExtraParams();
    }

    public EventParams(int no, EventCode eventcode, Condition condition, ExtraParams para)
    {
        this.no = no;
        this.eventcode = eventcode;
        this.condition = condition;
        this.extraParams = para;
    }
}
