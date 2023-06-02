using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageInfo
{
    public List<EventInfo_so> eventList_so = new List<EventInfo_so>();

    private List<EventInfo> eventList = new List<EventInfo>();
    public List<EventInfo> EventList { get => eventList; set => eventList = value; }
    
    public StageInfo(StageInfo_so stageinfo_so)
    {
        this.eventList_so = stageinfo_so.eventList_so;
        SortSeqential();
    }


    // 로딩시 evenList_so 값을 받았을때 순차적 이벤트인지 구분하기 위해 실행
    public void SortSeqential()
    {
        // 비어있으면 탈출
        if (eventList_so.Count == 0)
            return;

        eventList.Clear();
        foreach (EventInfo_so so in eventList_so)
            eventList.Add(new EventInfo(so));

    }
}
