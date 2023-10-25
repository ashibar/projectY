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
}
