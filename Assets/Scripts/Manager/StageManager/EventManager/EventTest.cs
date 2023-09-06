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

    private void Update()
    {
        //if (Input.GetKey(KeyCode.A))
        //    EventManager.Instance.PostNotification(EventCode.a, this, new Condition());
        //else if (Input.GetKey(KeyCode.B))
        //    EventManager.Instance.PostNotification(EventCode.b, this, new Condition());
        //else if (Input.GetKey(KeyCode.C))
        //    EventManager.Instance.PostNotification(EventCode.c, this, new Condition());
    }

    public void SubscribeEvent()
    {
        //EventManager.Instance.AddListener(EventCode.a, this);
        //EventManager.Instance.AddListener(EventCode.b, this);
    }

    public void OnEvent(string event_type, Component sender, Condition condition, params object[] param)
    {
        string s = string.Empty;

        //switch (event_type)
        //{
        //    case EventCode.a:
        //        s = "a"; break;
        //    case EventCode.b:
        //        s = "b"; break;
        //    case EventCode.c:
        //        s = "c"; break;
        //}

        //Debug.Log(s);
    }

    
}

