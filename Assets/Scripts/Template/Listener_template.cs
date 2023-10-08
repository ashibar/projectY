using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <para/><b>��� Listener Template ���</b>
/// <para/>����� : 
/// <para/>��� : 
/// <para/>��� : 
/// <para/>������Ʈ ���� :
/// <para/> - (23.00.00) : ��ũ��Ʈ ����
/// <para/>�̺�Ʈ :
/// <para/> - Event A : intvalue(������ �ε���)
/// <para/> - Event B : name(������ �ױ�)
/// <para/> - Event C : boolvalue(Ȱ��ȭ ����)
/// </summary>

public class Listener_template : MonoBehaviour, IEventListener
{
    public static List<string> event_code = new List<string>
    {
        "Event A",
        "Event B",
        "Event C",
        "Event D",
        "Event E",
    };

    private void Awake()
    {
        SubscribeEvent();
    }

    public void SubscribeEvent()
    {
        foreach (string code in event_code)
            EventManager.Instance.AddListener(code, this);

        // �Ǵ�

        //EventManager.Instance.AddListener("Event A", this);
        //EventManager.Instance.AddListener("Event B", this);
        //EventManager.Instance.AddListener("Event C", this);
    }

    public void OnEvent(string event_type, Component sender, Condition condition, params object[] param)
    {
        ExtraParams para = (ExtraParams)param[0];

        switch (event_type)
        {
            case "Event A":
                EventA(para); break;
            case "Event B":
                EventB(para); break;
            case "Event C":
                EventC(para); break;
        }
    }

    private void EventA(ExtraParams para)
    {
        Debug.Log(string.Format("{0}, {1}", "EventA" ,para.Intvalue));
    }

    private void EventB(ExtraParams para)
    {
        Debug.Log(string.Format("{0}, {1}", "EventB", para.Name));
    }

    private void EventC(ExtraParams para)
    {
        Debug.Log(string.Format("{0}, {1}", "EventC", para.Boolvalue));
    }
}
