using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using Unity.Burst.Intrinsics;
using UnityEngine;

/// <summary>
/// <para/><b>■■ EventTimer ■■</b>
/// <para/>담당자 : 이용욱
/// <para/>요약 : 이벤트에 주어진 대기 시간 체크 후 실행
/// <para/>비고 : 
/// <para/>업데이트 내역 : 
/// <para/> - (23.08.22) : 요약문 생성
/// <para/>
/// </summary>

public class EventTimer : MonoBehaviour
{
    [SerializeField] private List<EventInfo> events = new List<EventInfo>();
    public List<EventInfo> Events { get => events; set => events = value; }

    [SerializeField] private List<EventPhase_so> phase = new List<EventPhase_so>();
    //[SerializeField] private List<EventParams> para = new List<EventParams>();
    [SerializeField] private List<EventParams> eventParam_pos = new List<EventParams>();

    [SerializeField] private UnitManager unitManager;

    [SerializeField] private bool isCooltime;
    [SerializeField] private int eventIndex = 0;
    [SerializeField] private int interruptedIndex = -1;
    [SerializeField] private bool isInterrupted;

    private List<CancellationTokenSource> cts = new List<CancellationTokenSource>();

    private void Start()
    {
        unitManager = UnitManager.Instance;
        Debug.Log("?");
        //CTS(0);
    }

    // async 함수 탈출 토큰 설정
    private async void CTS(int id)
    {
        cts.Add(new CancellationTokenSource());
        await Task_Function_(id, cts[id].Token);
    }

    private void Update()
    {
        //Async_Function();
        //CheckPosition();
        //Async_Function_();
    }
      

    //private async void Async_Function()
    //{
    //    // 무결성
    //    if (para == null)
    //        return;

    //    // 업데이트
    //    if (!isCooltime && eventIndex < para.Count)
    //    {
    //        isCooltime = true;
    //        interruptedIndex = await Task_Function();
    //        isCooltime = false;
    //    }

    //    if (interruptedIndex != -1)
    //        Debug.Log("interruptedIndex = " + interruptedIndex.ToString());
    //}
    // 이벤트의 메시지 입력 함수
    //private async Task<int> Task_Function()
    //{
                
    //    float end = Time.time + (float)para[eventIndex].condition.TargetNum;
    //    while (Time.time < end)
    //    {
    //        await Task.Yield();
    //        if (isInterrupted)
    //        {
    //            await Task.FromResult(0);
    //        }
    //    }
    //    Debug.Log(string.Format("{0} : {1}, {2}, {3}", eventIndex, para[eventIndex].eventcode, para[eventIndex].condition.Sort, (para[eventIndex].extraParams != null) ? para[eventIndex].extraParams : null));
    //    if (para[eventIndex].condition.Sort != ConditionSort.MoveToPos)
    //        PostNotification(para[eventIndex]);            
    //    else
    //        eventParam_pos.Add(eventParam_pos[eventIndex]);
        

    //    return -1;
    //}


    /// <summary>
    /// 
    /// </summary>
    /// <param name="no"></param>
    /// <param name="_cts"></param>
    /// <returns></returns>
    private async Task<int> Task_Function_(int no, CancellationToken _cts)
    {
        // 페이즈 무결성
        while (no >= phase.Count)
        {
            await Task.Yield();
        }


        List<EventParams> para = phase[no].Events;

        float start = Time.time;
        float accumulated;
        while (!_cts.IsCancellationRequested)
        {
            // 파라미터 무결성
            if (para == null)
            {
                await Task.Yield();
                continue;
            }

            accumulated = 0;
            foreach (EventParams p in para)
            {                
                // 위치
                if (p.condition.Sort == ConditionSort.MoveToPos)
                {
                    p.condition.IsSatisfied = CheckPosition(p);
                }
                // 누적 시간
                else if (p.condition.IsContinued)
                {
                    accumulated += p.condition.TargetNum;
                    if (p.condition.IsSatisfied) continue;
                    if (Time.time - start >= accumulated)
                        p.condition.IsSatisfied = PostNotification(p);
                }
                // 절대 시간
                else
                {
                    if (p.condition.IsSatisfied) continue;
                    if (Time.time - start >= p.condition.TargetNum)
                        p.condition.IsSatisfied = PostNotification(p);
                }
                
            }
            
            await Task.Yield();
            
        }

        return -1;
    }

    private bool PostNotification(EventParams parm)
    {
        Debug.Log(string.Format("{0} : {1}, {2}, {3}", eventIndex, parm.eventcode, parm.condition.Sort, (parm.extraParams != null) ? parm.extraParams : null));
        EventManager.Instance.PostNotification(parm.eventcode, this, parm.condition, parm.extraParams);
        return true;
    }
        
    private bool CheckPosition(EventParams p)
    {
        if (unitManager == null) unitManager = UnitManager.Instance;
        if (unitManager.Clones == null) return false;
        List<GameObject> clones = unitManager.Clones;
        if (clones.Count == 0) return false;
        if (p.condition.IsSatisfied) return true;

        int num = 0;
        foreach (GameObject go in clones)
        {
            if (go == null) continue;
            if (!string.Equals(p.condition.TargetTag, go.tag)) continue;
            if (Vector2.Distance(go.transform.position, p.condition.TargetPos) < 0.1f)
            {
                num++;
            }
        }
        if (num >= p.condition.TargetNum)
        {
            p.condition.IsSatisfied = true;
            Debug.Log(string.Format("{0} : {1}, {2}, {3}", eventIndex, p.eventcode, p.condition.Sort, (p.extraParams != null) ? p.extraParams : null));
            EventManager.Instance.PostNotification(p.eventcode, this, p.condition, p.extraParams);
            return true;
            //eventIndex++;
            //eventParam_pos.RemoveAt(i);
            //Debug.Log(i);
        }
        else
            return false;
    }


    // 파괴시 모든 이벤트 중단
    private void OnDestroy()
    {
        foreach(CancellationTokenSource c in cts)
            c?.Cancel();
        //isInterrupted = true;
    }

    // ** 외부 참조 스크립트 **
    //public void SetPara(List<EventParams> para)
    //{
    //    this.para = para;
    //}

    //public void AddPara(EventParams para)
    //{
    //    this.para.Add(para);
    //}

    public void AddPhase(EventPhase_so phase)
    {
        EventPhase_so so = phase;
        foreach (EventParams p in so.Events)
            p.condition.IsSatisfied = false;
        this.phase.Add(phase);
        CTS(this.phase.Count - 1);
    }    


    // 더미 코드
    //private async void Async_Function()
    //{
    //    // 무결성
    //    if (events == null)
    //        return;

    //    // 업데이트
    //    if (!isCooltime && eventIndex < events.Count)
    //    {
    //        isCooltime = true;
    //        interruptedIndex = await Task_Function();
    //        isCooltime = false;
    //    }

    //    if (interruptedIndex != -1)
    //        Debug.Log("interruptedIndex = " + interruptedIndex.ToString());
    //}
    //// 이벤트의 메시지 입력 함수
    //private async Task<int> Task_Function()
    //{
    //    float end = Time.time + events[eventIndex].DurationToStart;
    //    while (Time.time < end)
    //    {
    //        if (events[eventIndex].Isinterrupted)
    //            await Task.FromResult(0);
    //        await Task.Yield();
    //    }
    //    Debug.Log(eventIndex + ", " + events[eventIndex].Id + ", " + events[eventIndex].DurationToStart);
    //    StageManager.Instance.messageBuffer.Add(events[eventIndex].Message);
    //    eventIndex++;

    //    return -1;
    //}

}
