using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class None_Element : Spell_Element
{
    public override void Awake()
    {
        base.Awake();
        level.numberName = "None";
        level.numberValue = 1;
        projectile_color = new Color32(184, 223, 248, 255);
    }

    public override void ShootingFunction(CancellationToken cts_t, GameObject projectile, Stat stat_processed, Stat_Spell stat_spell, Vector2 _dir_toShoot, Projectile_AnimationModule anim_module)
    {
        base.ShootingFunction(cts_t, projectile, stat_processed, stat_spell, _dir_toShoot, anim_module);
        SpriteRenderer sr = projectile.GetComponent<SpriteRenderer>();
        sr.color = projectile_color;
    }
}
