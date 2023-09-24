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

    public override void TriggerEnterEndFunction(Collider2D collision, GameObject projectile)
    {
        if (collision.tag == "Enemy")
            Destroy(projectile);
    }

    public override void ShootingFunction(CancellationToken cts_t, GameObject projectile, Stat_Spell stat, Vector2 _dir_toShoot)
    {
        //ป๙วร
        if (cts_t.IsCancellationRequested) return;
        //Debug.Log("shooting function");
        Vector3 pos = projectile.transform.position;
        projectile.transform.position = Vector3.MoveTowards(pos, pos + (Vector3)_dir_toShoot, stat.Spell_Speed * Time.deltaTime);
    }
}
