using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Parts_OnUpdate : Parts
{
    // ƽ �ֱ�
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

    // �� �ֱ⸶�� ����� �Լ�
    protected async virtual Task Update_Function(Applier_parameter para, float duration)
    {
        float end = Time.time + duration;
        while (Time.time < end)
        {
            // �� �����Ӹ��� ����� ��
            await Task.Yield();
        }
        // duration �Ŀ� ����� ��
    }
}
