using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class Dash_Core : Spell_Core
{
    [SerializeField] private float dash_speed;
    [SerializeField] private float dash_duration;

    [SerializeField] private AfterImage afterImage;

    public override void Awake()
    {
        base.Awake();
        afterImage = GetComponent<AfterImage>();
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

    protected override async void InstantiateDelayFunction()
    {
        if (!isCooltime && (Input.GetKey(triggerKey) || trigger))
        {
            trigger = false;
            isCooltime = true;
            await Dash_routine();
            isCooltime = false;
        }
    }

    private async Task Dash_routine()
    {
        float end = Time.time + stat_spell.Spell_CoolTime;
        while ((Time.time < end - (stat_spell.Spell_CoolTime - dash_duration)) && !cts.Token.IsCancellationRequested)
        {
            afterImage.SetImage(owner.gameObject, owner.GetComponent<SpriteRenderer>().flipX);
            afterImage.IsActive = true;
            owner.transform.position = Vector2.MoveTowards(owner.transform.position, (Vector2)owner.transform.position + dir_toMove, dash_speed * Time.deltaTime);

            await Task.Yield();
        }
        while ((end - (stat_spell.Spell_CoolTime - dash_duration) <= Time.time && Time.time < end) && !cts.Token.IsCancellationRequested)
        {
            afterImage.IsActive = false;

            await Task.Yield();
        }
    }

    protected override GameObject InstantiateProjectile(DelegateParameter para)
    {
        return null;
    }

    public override void TriggerEnterEndFunction(DelegateParameter para)
    {
        
    }

    public override void ShootingFunction(DelegateParameter para)
    {
        
    }
}
