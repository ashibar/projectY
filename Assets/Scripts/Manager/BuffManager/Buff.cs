using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    [SerializeField] private Buff_SO buff_SO;
    
    [SerializeField] protected string buff_name;
    [SerializeField] protected string buff_type;
    [SerializeField] protected float buff_durationcrrent;
    [SerializeField] protected float buff_tick;               // 틱 주기
    [SerializeField] protected float buff_currenttime;
    [SerializeField] protected float buff_value;
    [SerializeField] protected float buff_redusespeed;

    [SerializeField] protected Stat stat;

    public BuffManager buffmanager;
    private bool resetFlag;

    private void Awake()
    {
                
    }
    
    private IEnumerator Update_Routine(float duration)
    {
        if (duration < 0.1f) duration = 0.1f;
        float end = Time.time + duration;
        float accumulate = Time.time;
        while (Time.time < end)
        {
            // 타이머 리셋
            if (resetFlag)
            {
                end = Time.time + duration;
                resetFlag = false;
            }
            // 틱주기마다 실행할 함수, 누적시간 갱신
            if (Time.time > buff_tick + accumulate)
            {
                Tick_Function();
                accumulate = Time.time;
            }            
            yield return null;
        }

        Destroy_Function();
    }

    protected virtual void Tick_Function()
    {
        
    }

    protected virtual void Destroy_Function()
    {
        buffmanager.BuffEndListener(this);
        Destroy(gameObject);
    }

    public virtual void BuffStat(Stat stat)
    {

    }

    public void SetBuff(Buff_SO buff)
    {
        //Debug.Log("problem?");
        this.buff_name = buff.Buff_Name;
        this.buff_type = buff.Buff_Type;
        this.buff_tick = buff.Buff_tick;
        this.buff_currenttime = buff.Buff_currentTime;
        this.buff_value = buff.Buff_value;
        this.buff_durationcrrent = buff.Buff_duration;
    }

    public string Buff_Name { get => buff_name; set => buff_name = value; }
    public string Buff_Type { get => buff_type; set => buff_type = value; }
    public float Buff_tick { get => buff_tick; set => buff_tick = value; }
    public float Buff_currenttime { get => buff_currenttime; set => buff_currenttime = value; }
    public float Buff_value { get => buff_value; set => buff_value = value;}
    public float Buff_durationcrrent { get => buff_durationcrrent; set => buff_durationcrrent = value;}

    public void Init(Stat target_stat, BuffManager manager)
    {
        stat = target_stat;
        buffmanager = manager;
        SetBuff(buff_SO);
        StartCoroutine(Update_Routine(buff_durationcrrent));
    }

    public void ResetTimer()
    {
        resetFlag = true;
    }    
}
