using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Parts_OnUpdate : Parts
{
    [SerializeField] private float cooltime;
    [SerializeField] private bool isCooltime = false;

    public float Cooltime { get => cooltime; set => cooltime = value; }

    public override void Applier(Applier_parameter para)
    {
        
        Async_Function(para);
    }

    protected async virtual void Async_Function(Applier_parameter para)
    {
        if (!isCooltime)
        {
            isCooltime = true;
            await Update_Function(para, cooltime);
            isCooltime = false;
        }
    }

    protected async virtual Task Update_Function(Applier_parameter para, float duration)
    {
        float end = Time.time + duration;
        while (Time.time < end)
        {
            // 매 프레임마다 실행될 것
            await Task.Yield();
        }
        // duration 후에 실행될 것
    }
}
