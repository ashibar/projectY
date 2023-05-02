using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_AI : MonoBehaviour
{
    private Movement movement;

    protected virtual void Awake()
    {
        movement = GetComponentInParent<Movement>();
    }
    public virtual void ai_process()
    {
        // ai가 할 행동
    }
}
