using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Parts_Dash : Parts_OnUpdate
{
    [SerializeField] private float dash_speed;
    [SerializeField] private float dash_duration;

    protected async override Task Update_Function(Applier_parameter para, float duration)
    {
        float end = Time.time + para.Stat.Spell_CoolTime;
        while (Time.time < end - (para.Stat.Spell_CoolTime - dash_duration))
        {
            // 매 프레임마다 실행될 것
            if (isInterrupted)
                await Task.FromResult(false);
            para.Owner.transform.position = Vector2.MoveTowards(para.Owner.transform.position, (Vector2)para.Owner.transform.position + para.Dir_toMove, dash_speed);

            await Task.Yield();
        }
        while (end - (para.Stat.Spell_CoolTime - dash_duration) <= Time.time && Time.time < end)
        {
            // 매 프레임마다 실행될 것
            if (isInterrupted)
                await Task.FromResult(false);
            
            await Task.Yield();
        }
        // duration 후에 실행될 것
    }
}
