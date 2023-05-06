using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy_Enemy : Enemy
{
    private Transform target_transform;
    private Movement movement;

    protected override void Start()
    {
        base.Start();
        movement = GetComponent<Movement>();
    }

    protected override void Update()
    {
        base.Update();
        movement.MoveToPosition_transform(Player.instance.transform.position, stat.Speed);
    }
}
