using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[System.Serializable]
public class DelegateParameter
{
    public CancellationToken cts_t;
    public Projectile_AnimationModule anim_module;
    public List<Collider2D> collider_stack;
    public Collider2D collision;
    public GameObject projectile;
    public Stat stat_processed;
    public Stat_Spell stat_spell;
    public Vector2 dir_toShoot;
    public Quaternion rotation;
}
