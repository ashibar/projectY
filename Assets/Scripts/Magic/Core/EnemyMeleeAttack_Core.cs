using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyMeleeAttack_Core : Spell_Core
{
    public override void Awake()
    {
        base.Awake();
    }

    protected override void Update()
    {
        
    }

    public async void MeleeDamange(Collision2D collision)
    {
        //Debug.Log("");
        if (!isCooltime && !cts.Token.IsCancellationRequested)
        {
            isCooltime = true;
            await MeleeDamage_Task(collision, stat_spell.Spell_CoolTime);
            isCooltime = false;
        }
    }
    private async Task MeleeDamage_Task(Collision2D collision, float duration)
    {
        float end = Time.time + duration;
        DamageCalculation dc = UnitManager.Instance.damageCalculation;
        collision.gameObject.GetComponent<Unit>().stat.Hp_current -= dc.Calculate(null, collision.gameObject.GetComponent<Unit>(), owner.stat, stat_spell, Color.red);
        collision.gameObject.GetComponent<Unit>().ActiveBlink();
        //stat_spell.Spell_DMG * owner.stat.Damage
        Debug.Log("damage");
        while (Time.time < end && !cts.Token.IsCancellationRequested)
        {
            await Task.Yield();
        }
    }

    private void OnDestroy()
    {
        cts?.Cancel();
    }
}
