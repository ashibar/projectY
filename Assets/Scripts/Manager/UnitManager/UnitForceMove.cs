using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitForceMove : MonoBehaviour
{
    private bool isForceMove;
    public bool IsForceMove { get => isForceMove; set => isForceMove = value; }

    [SerializeField] private Unit unit;
    [SerializeField] private Vector2 targetPos;
    [SerializeField] private float speed;

    private void Update()
    {
        if (isForceMove)
            ForceMove();
    }

    public void SetForceMove(Unit _unit, Vector2 _targetPos, float _speed)
    {
        unit = _unit;
        targetPos = _targetPos;
        speed = _speed;
    }

    private void ForceMove()
    {
        Vector2 pos = unit.transform.position;
        Vector2 dir = (targetPos - pos).normalized;

        float distanceToTarget = (targetPos - pos).magnitude;

        // 등속 이동
        if (distanceToTarget > 0f)
        {
            if (speed == 0)
                unit.transform.position = Vector2.MoveTowards(pos, targetPos, unit.stat.Speed * Time.deltaTime);
            else
                unit.transform.position = Vector2.MoveTowards(pos, targetPos, speed * Time.deltaTime);
        }
        //else
        //messageBuffer.Remove(m);
    }
}
