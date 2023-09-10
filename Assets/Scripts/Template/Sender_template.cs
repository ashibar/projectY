using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sender_template : MonoBehaviour
{
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

    public void Event_C_Sender(bool value)
    {
        ExtraParams para = new ExtraParams();
        para.Boolvalue = value;
        EventManager.Instance.PostNotification("Event C", this, null, para);
    }
}
