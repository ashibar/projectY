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

    protected override GameObject InstantiateProjectile(Quaternion rotation)
    {
        return Instantiate(projectile_origin[0], transform.position, rotation, Holder.projectile_holder);
    }

    public override void TriggerEnterEndFunction(Collider2D collision, GameObject projectile, Stat stat_processed, Stat_Spell stat_spell)
    {
        float damage = stat_processed.Damage * stat_spell.Spell_DMG;
        collision.GetComponent<Unit>().stat.Hp_current -= stat_processed.Damage * stat_spell.Spell_DMG;
        damageTextRenderModule.Damagesend(projectile, (int)damage);
        Destroy(projectile);
    }

    public override void ShootingFunction(CancellationToken cts_t, GameObject projectile, Stat stat_processed, Stat_Spell stat_spell, Vector2 _dir_toShoot, Projectile_AnimationModule anim_module)
    {
        if (cts_t.IsCancellationRequested) return;
        Vector3 pos = projectile.transform.position;
        projectile.transform.position = Vector3.MoveTowards(pos, pos + (Vector3)_dir_toShoot, stat_spell.Spell_Speed * Time.deltaTime);
        projectile.GetComponent<SpriteRenderer>().sprite = anim_module.GetSprite();
    }
}
