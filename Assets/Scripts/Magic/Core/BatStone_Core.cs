using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BatStone_Core : Spell_Core
{
    public override void Awake()
    {
        base.Awake();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override Quaternion SetAngle()
    {
        float angle = Mathf.Atan2(dir_toShoot.y, dir_toShoot.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        return rotation;
    }

    protected override GameObject InstantiateProjectile(DelegateParameter para)
    {
        return Instantiate(para.projectile, transform.position, para.rotation, Holder.projectile_holder);
    }

    public override void TriggerEnterEndFunction(DelegateParameter para)
    {
        //float damage = stat_processed.Damage * stat_spell.Spell_DMG;
        //collision.GetComponent<Unit>().stat.Hp_current -= stat_processed.Damage * stat_spell.Spell_DMG;
        //damageTextRenderModule.Damagesend(projectile, (int)damage);
        //Destroy(projectile);
    }

    public override bool TriggerEnterStackProcess(DelegateParameter para)
    {
        if (para.collider_stack.Count > 0)
        {
            for (int i = 0; i < Mathf.Min((int)(stat_spell.Spell_Amount_Tic), para.collider_stack.Count); i++)
            {
                float damage = para.stat_processed.Damage * para.stat_spell.Spell_DMG;
                //Debug.Log(string.Format("{0}, {1}, {2}", stat_processed.Damage, stat_spell.Spell_DMG, damage));
                DamageCalculation dc = UnitManager.Instance.damageCalculation;
                para.collider_stack[i].GetComponent<Unit>().stat.Hp_current -= dc.Calculate(owner, para.collider_stack[i].GetComponent<Unit>(), para.stat_processed, para.stat_spell, Color.white);
                //damageTextRenderModule.Damagesend(para.projectile, (int)damage);
                Destroy(para.projectile);
            }
            para.collider_stack.Clear();
            return true;
        }
        return false;
    }

    public override void ShootingFunction(DelegateParameter para)
    {
        if (para.cts_t.IsCancellationRequested) return;
        Vector3 pos = para.projectile.transform.position;
        para.projectile.transform.position = Vector3.MoveTowards(pos, pos + (Vector3)para.dir_toShoot, para.stat_spell.Spell_Speed * Time.deltaTime);
        para.projectile.GetComponent<SpriteRenderer>().sprite = para.anim_module.GetSprite();
    }
}
