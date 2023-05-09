using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageInfo
{
    public List<EventInfo_so> eventList_so = new List<EventInfo_so>();

    private List<EventInfo> eventList = new List<EventInfo>();
    public List<EventInfo> event_sync = new List<EventInfo>();
    public List<EventInfo> event_seq = new List<EventInfo>();
    
    public StageInfo(List<EventInfo_so> eventList_so)
    {
        this.eventList_so = eventList_so;
    }

    // �ε��� evenList_so ���� �޾����� ������ �̺�Ʈ���� �����ϱ� ���� ����
    public void SortSeqential()
    {
        // ��������� Ż��
        if (eventList_so.Count == 0)
            return;

        eventList.Clear();
        event_sync.Clear();
        event_seq.Clear();
        foreach (EventInfo_so so in eventList_so)
            eventList.Add(new EventInfo(so));

        foreach (EventInfo info in eventList)
        {
            if (info.IsSequential)
            {
                event_seq.Add(info);
            }
            else
            {
                event_sync.Add(info);
            }
        }
    }
}
