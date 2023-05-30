using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_AI : MonoBehaviour
{
    protected Unit unit;
    private Movement movement;

    protected virtual void Awake()
    {
        movement = GetComponentInParent<Movement>();
    }
    protected virtual void Start()
    {
        unit = GetComponentInParent<Unit>();
    }
    protected virtual void Update()
    {
        ai_process();
    }
    public virtual void ai_process()
    {
        // ai가 할 행동
    }
}
