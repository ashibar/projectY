using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageEndCheck : MonoBehaviour
{
    [SerializeField] private float curTime;
    [SerializeField] private float endTime;
    [SerializeField] private bool startFlag;
    [SerializeField] private float lastTime;
    public float CurTime { get => curTime; set => curTime = value; }
    public float EndTime { get => endTime; set => endTime = value; }
    public bool StartFlag { get => startFlag; set => startFlag = value; }

    public void SetActive(bool value, bool reset = false)
    {
        if (value)
        {
            startFlag = true;
            if (reset)
                lastTime = Time.time;
        }
        else
        {
            startFlag = false;
        }
    }

    private void Update()
    {
        if (startFlag)
        {
            curTime = Time.time - lastTime;
        }
    }
}
