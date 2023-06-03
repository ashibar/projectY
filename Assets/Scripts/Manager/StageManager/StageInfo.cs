using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageInfo
{
    [SerializeField] private string stageName;
    [SerializeField] private List<GameObject> rewards = new List<GameObject>();
    [SerializeField] private StageSort stageSort;
    public List<EventInfo_so> eventList_so = new List<EventInfo_so>();
    public List<GameObject> spawners = new List<GameObject>();

    private List<EventInfo> eventList = new List<EventInfo>();
    public List<EventInfo> EventList { get => eventList; set => eventList = value; }
    public StageSort StageSort { get => stageSort; set => stageSort = value; }

    public StageInfo(StageInfo_so stageinfo_so)
    {
        this.eventList_so = stageinfo_so.eventList_so;
        this.spawners = stageinfo_so.spawners;
        this.stageName = stageinfo_so.StageName;
        this.rewards = stageinfo_so.Rewards;
        this.stageSort = stageinfo_so.StageSort;
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
