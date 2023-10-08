using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <para/><b>■■ Listener Template ■■</b>
/// <para/>담당자 : 
/// <para/>요약 : 
/// <para/>비고 : 
/// <para/>업데이트 내역 :
/// <para/> - (23.00.00) : 스크립트 생성
/// <para/>이벤트 :
/// <para/> - Event A : intvalue(참조할 인덱스)
/// <para/> - Event B : name(참조할 테그)
/// <para/> - Event C : boolvalue(활성화 여부)
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

        // 또는

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
