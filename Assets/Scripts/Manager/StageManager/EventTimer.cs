using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// <para/><b>��� EventTimer ���</b>
/// <para/>����� : �̿��
/// <para/>��� : �̺�Ʈ�� �־��� ��� �ð� üũ �� ����
/// <para/>��� : 
/// <para/>������Ʈ ���� : 
/// <para/> - (23.08.22) : ��๮ ����
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
        // ���Ἲ
        if (para == null)
            return;

        // ������Ʈ
        if (!isCooltime && eventIndex < para.Count)
        {
            isCooltime = true;
            interruptedIndex = await Task_Function();
            isCooltime = false;
        }

        if (interruptedIndex != -1)
            Debug.Log("interruptedIndex = " + interruptedIndex.ToString());
    }
    // �̺�Ʈ�� �޽��� �Է� �Լ�
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

    // �ı��� ��� �̺�Ʈ �ߴ�
    private void OnDestroy()
    {
        foreach (EventInfo e in events)
        {
            e.Isinterrupted = true;
        }
    }

    // ** �ܺ� ���� ��ũ��Ʈ **
    public void SetPara(List<EventParams> para)
    {
        this.para = para;
    }

    public void AddPara(EventParams para)
    {
        this.para.Add(para);
    }



    

    // ���� �ڵ�
    //private async void Async_Function()
    //{
    //    // ���Ἲ
    //    if (events == null)
    //        return;

    //    // ������Ʈ
    //    if (!isCooltime && eventIndex < events.Count)
    //    {
    //        isCooltime = true;
    //        interruptedIndex = await Task_Function();
    //        isCooltime = false;
    //    }

    //    if (interruptedIndex != -1)
    //        Debug.Log("interruptedIndex = " + interruptedIndex.ToString());
    //}
    //// �̺�Ʈ�� �޽��� �Է� �Լ�
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
