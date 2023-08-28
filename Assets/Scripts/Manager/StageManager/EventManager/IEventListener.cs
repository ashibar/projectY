using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEventListener
{
    public void OnEvent(EventCode event_type, Component sender, Condition condition, params object[] param);
    public void SubscribeEvent();
}
