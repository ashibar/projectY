using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Spell_Element : Spell
{
    [SerializeField] public StringNNumber level = new StringNNumber("None", 0);
    [SerializeField] protected Projectile_AnimationModule animationModule;
    [SerializeField] protected Color projectile_color = Color.white;

    public Projectile_AnimationModule AnimationModule { get => animationModule; set => animationModule = value; }

    public override void Awake()
    {
        base.Awake();
        animationModule = GetComponentInChildren<Projectile_AnimationModule>();
        animationModule.SpriteChange_routine();
    }

    // 전달할 대리자 함수

    public virtual void TriggerEnterTickFunction(Collider2D collision, GameObject projectile)
    {

    }

    public virtual void TriggerEnterEndFunction(Collider2D collision, GameObject projectile, Stat stat_processed, Stat_Spell stat_spell)
    {
        
    }

    public virtual void ShootingFunction(CancellationToken cts_t, GameObject projectile, Stat stat_processed, Stat_Spell stat_spell, Vector2 _dir_toShoot, Projectile_AnimationModule anim_module)
    {
        
    }

    public virtual void DestroyFunction(GameObject projectile)
    {

    }
}
