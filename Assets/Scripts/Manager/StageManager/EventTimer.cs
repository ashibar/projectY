using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

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
    }

    private async void Async_Function()
    {
        // ���Ἲ
        if (events == null)
            return;

        // ������Ʈ
        if (!isCooltime && eventIndex < events.Count)
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
        float end = Time.time + events[eventIndex].DurationToStart;
        while (Time.time < end)
        {
            if (events[eventIndex].Isinterrupted)
                await Task.FromResult(0);
            await Task.Yield();
        }
        Debug.Log(eventIndex + ", " + events[eventIndex].DurationToStart);
        StageManager.Instance.messageBuffer.Add(events[eventIndex].Message);
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
}
