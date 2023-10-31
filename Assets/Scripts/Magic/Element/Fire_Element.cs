using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_Element : Spell_Element
{
    [SerializeField] private GameObject burn_buff_origin;
    
    public override void Awake()
    {
        base.Awake();
        level.numberName = "Fire";
        level.numberValue = 1;
        projectile_color = new Color32(184, 223, 248, 255);
    }

    public override void ShootingFunction(DelegateParameter para)
    {
        base.ShootingFunction(para);

    }

    public override void TriggerEnterStackProcess(DelegateParameter para)
    {
        base.TriggerEnterStackProcess(para);
        BuffManager target_buffManager = para.collision.GetComponent<Unit>().buffManager;
        GameObject clone = target_buffManager.AddBuff(burn_buff_origin);
        Debug.Log(string.Format("{0} is burning, {1}", para.collision, target_buffManager));
        clone.GetComponent<Buff>().Buff_value = para.stat_processed.Damage * para.stat_spell.Spell_DMG * 0.1f;
        clone.GetComponent<Buff>().Buff_durationcrrent = 5;
        clone.GetComponent<Buff>().Buff_tick = 1;

        clone.GetComponent<Buff>().Init(para.collision.GetComponent<Unit>().stat, target_buffManager);
    }
}
