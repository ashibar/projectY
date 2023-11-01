using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_AI : MonoBehaviour
{
    protected Unit unit;
    private Movement movement;
    [SerializeField] private bool isActive = true;

    protected virtual void Awake()
    {
        movement = GetComponentInParent<Movement>();
    }
    protected virtual void Start()
    {
        unit = GetComponent<Unit>();
    }
    protected virtual void Update()
    {
        if (isActive)
        {
            ai_process();
        }
    }
    public virtual void ai_process()
    {
        // ai가 할 행동
    }
    protected virtual void ai_movement(Vector3 targetpos,Vector2 dir)
    {
        
    }
}
