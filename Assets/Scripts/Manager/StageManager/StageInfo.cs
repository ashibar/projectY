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


    // �ε��� evenList_so ���� �޾����� ������ �̺�Ʈ���� �����ϱ� ���� ����
    public void SortSeqential()
    {
        // ��������� Ż��
        if (eventList_so.Count == 0)
            return;

        eventList.Clear();
        foreach (EventInfo_so so in eventList_so)
            eventList.Add(new EventInfo(so));

    }
}
