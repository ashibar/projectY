using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultStageInfo", menuName = "Scriptable Object/StageInfo", order = int.MaxValue)]
public class StageInfo_so : ScriptableObject
{
    public List<EventInfo_so> eventList = new List<EventInfo_so>();

    public List<EventInfo> event_sync;
    public List<EventInfo> event_seq;
}
