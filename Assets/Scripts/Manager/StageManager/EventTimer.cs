using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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

    [SerializeField] private bool isCooltime;
    [SerializeField] private int eventIndex = 0;
    [SerializeField] private int interruptedIndex = -1;


    private void Update()
    {
        Async_Function();
        //Async_Function_();
    }

    [SerializeField] private List<EventParams> para = new List<EventParams>();

    private async void Async_Function()
    {
        // 무결성
        if (para == null)
            return;

        // 업데이트
        if (!isCooltime && eventIndex < para.Count)
        {
            isCooltime = true;
            interruptedIndex = await Task_Function();
            isCooltime = false;
        }

        if (interruptedIndex != -1)
            Debug.Log("interruptedIndex = " + interruptedIndex.ToString());
    }
    // 이벤트의 메시지 입력 함수
    private async Task<int> Task_Function()
    {
        float end = Time.time + (float)para[eventIndex].condition.TargetNum;
        while (Time.time < end)
        {
            await Task.Yield();
        }
        Debug.Log(string.Format("{0} : {1}, {2}, {3}", eventIndex, para[eventIndex].eventcode, para[eventIndex].condition.TargetNum, (para[eventIndex].extraParams != null) ? para[eventIndex].extraParams : null));
        EventManager.Instance.PostNotification(para[eventIndex].eventcode, this, para[eventIndex].condition, para[eventIndex].extraParams);
        eventIndex++;

        return -1;
    }

    // 파괴시 모든 이벤트 중단
    private void OnDestroy()
    {
        foreach (EventInfo e in events)
        {
            e.Isinterrupted = true;
        }
    }

    // ** 외부 참조 스크립트 **
    public void SetPara(List<EventParams> para)
    {
        this.para = para;
    }

    public void AddPara(EventParams para)
    {
        this.para.Add(para);
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
