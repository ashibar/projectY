using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTest : MonoBehaviour, IEventListener
{
    private void Awake()
    {
        SubscribeEvent();
    }

    private void Start()
    {
        
    }

    public void SubscribeEvent()
    {
        EventManager.Instance.AddListener(EventCode.a, this);
        EventManager.Instance.AddListener(EventCode.b, this);
        EventManager.Instance.AddListener(EventCode.c, this);
    }

    public void OnEvent(EventCode event_type, Component sender, Condition condition, params object[] param)
    {
        string s = string.Empty;

        switch (event_type)
        {
            case EventCode.a:
                s = "a"; break;
            case EventCode.b:
                s = "b"; break;
            case EventCode.c:
                s = "c"; break;
        }

        Debug.Log(s);
    }

    
}

