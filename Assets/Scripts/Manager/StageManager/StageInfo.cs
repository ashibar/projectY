using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageInfo
{
    [SerializeField] private string stageName;
    [SerializeField] private List<GameObject> rewards = new List<GameObject>();
    [SerializeField] private StageSort stageSort;
    [SerializeField] private List<EventPhase_so> phases = new List<EventPhase_so>();
    [SerializeField] private List<EventParams> para = new List<EventParams>();
    public List<EventInfo_so> eventList_so = new List<EventInfo_so>();
    public List<GameObject> spawners = new List<GameObject>();

    private List<EventInfo> eventList = new List<EventInfo>();
    public List<EventInfo> EventList { get => eventList; set => eventList = value; }
    public StageSort StageSort { get => stageSort; set => stageSort = value; }
    public List<EventParams> Para { get => para; set => para = value; }
    public List<EventPhase_so> Phases { get => phases; set => phases = value; }

    public StageInfo(StageInfo_so stageinfo_so)
    {
        this.eventList_so = stageinfo_so.eventList_so;
        this.spawners = stageinfo_so.spawners;
        this.stageName = stageinfo_so.StageName;
        this.rewards = stageinfo_so.Rewards;
        this.stageSort = stageinfo_so.StageSort;
        SortSeqential();
        this.para = stageinfo_so.Para;
        this.phases= stageinfo_so.Phases;
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
