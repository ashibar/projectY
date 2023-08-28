using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class UnitForceMove : MonoBehaviour
{
    private bool isForceMove;
    public bool IsForceMove { get => isForceMove; set => isForceMove = value; }

    [SerializeField] private Unit unit;
    [SerializeField] private Vector2 targetPos;
    [SerializeField] private float speed;
    [SerializeField] private float timeOut = 20f;
    [SerializeField] private bool isInterrupted;

    private void Awake()
    {
        unit = GetComponentInParent<Unit>();
    }

    private void Start()
    {
        //SetForceMove(new Vector2(5, 5), 2);
    }

    private void Update()
    {
        if (isForceMove)
            ForceMove();
    }

    public void SetForceMove(Vector2 _targetPos, float _speed)
    {
        targetPos = _targetPos;
        speed = _speed;
        ForceMove_routine(_targetPos, _speed);
    }

    private void ForceMove()
    {
        Vector2 pos = unit.transform.position;
        Vector2 dir = (targetPos - pos).normalized;

        float distanceToTarget = (targetPos - pos).magnitude;

        // 등속 이동
        if (distanceToTarget > 0.01f)
        {
            if (speed == 0)
                unit.transform.position = Vector2.MoveTowards(pos, pos + dir, unit.stat.Speed * Time.deltaTime);
            else
                unit.transform.position = Vector2.MoveTowards(pos, pos + dir, speed * Time.deltaTime);
        }
        else
            isForceMove = false;
    }
    private async void ForceMove_routine(Vector2 _targetPos, float _speed)
    {
        Transform tr = unit.transform;

        Vector2 pos = tr.position;
        Vector2 dir = (_targetPos -pos).normalized;

        //Vector2 pos = unit.transform.position;
        //Vector2 dir = (_targetPos - pos).normalized;
        //float distanceToTarget = (_targetPos - pos).magnitude;

        float distanceToTarget = (_targetPos - pos).magnitude;

        float end = Time.time + timeOut;

        while (!isInterrupted && distanceToTarget > 0.01f && Time.time < timeOut)
        {
            pos = tr.position;
            dir = (_targetPos - pos).normalized;
            distanceToTarget = (_targetPos - pos).magnitude;
            Debug.Log(string.Format("{0}, {1}, {2}, {3}", pos, _targetPos, dir, distanceToTarget));
            if (speed == 0)
                unit.transform.position = Vector2.MoveTowards(pos, pos + dir, unit.stat.Speed * Time.deltaTime);
            else
                unit.transform.position = Vector2.MoveTowards(pos, pos + dir, _speed * Time.deltaTime);

            await Task.Yield();
        }

        if (isInterrupted)
        {
            isInterrupted = false;
        }
    }
    public void ForceStop()
    {
        isInterrupted = true;
    }

    private void OnDestroy()
    {
        isInterrupted = true;
    }
}
