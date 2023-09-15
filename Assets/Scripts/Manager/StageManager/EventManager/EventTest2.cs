using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTest2 : MonoBehaviour, IEventListener
{
    public List<StringNTrigger> triggers = new List<StringNTrigger>();
    //public StringNTrigger trigger = new StringNTrigger();
    // Start is called before the first frame update
    void Start()
    {
        //EventManager.Instance.AddListener(EventCode.c, this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEvent(string event_type, Component sender, Condition condition, params object[] param)
    {
        //if (event_type== EventCode.c)
        //{
        //    Debug.Log("이벤트 C를 받았습니다.");
        //}
    }

    public void SubscribeEvent()
    {
        throw new System.NotImplementedException();
    }
}
