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

    public override void ShootingFunction(DelegateParameter para)
    {
        base.ShootingFunction(para);
        SpriteRenderer sr = para.projectile.GetComponent<SpriteRenderer>();
        sr.color = projectile_color;
    }
}
