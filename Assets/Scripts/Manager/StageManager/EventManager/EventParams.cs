using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EventParams
{
    [SerializeField] public int no;
    [SerializeField] public int eventindex;
    [SerializeField] public string eventcode;
    [SerializeField] public Condition condition;
    [SerializeField] public ExtraParams extraParams;

    public EventParams(int no)
    {
        this.no = no;
        this.eventindex = 0;
        this.eventcode = "None";
        this.condition = new Condition();
        this.extraParams = new ExtraParams();
    }

    public EventParams(int no, string eventcode, Condition condition)
    {
        this.no = no;
        this.eventindex = 0;
        this.eventcode = eventcode;
        this.condition = condition;
        this.extraParams = new ExtraParams();
    }

    public EventParams(int no, string eventcode, Condition condition, ExtraParams para)
    {
        this.no = no;
        this.eventindex = 0;
        this.eventcode = eventcode;
        this.condition = condition;
        this.extraParams = para;
    }
}
