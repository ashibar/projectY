using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sender_template : MonoBehaviour
{
    [SerializeField] private float count = 0;
    
    public void Event_A_Sender(int value)
    {
        ExtraParams para = new ExtraParams();
        para.Intvalue = value;
        EventManager.Instance.PostNotification("Event A", this, null, para);
    }

    public void Event_B_Sender(string value)
    {
        ExtraParams para = new ExtraParams();
        para.Name = value;
        EventManager.Instance.PostNotification("Event B", this, null, para);
    }

    public void Event_C_Sender()
    {
        ExtraParams para = new ExtraParams();
        count += 1;
        para.Name = "TestN";
        para.Floatvalue = count;
        EventManager.Instance.PostNotification("Set Number", this, null, para);
    }
}
