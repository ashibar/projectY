using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionArea : MonoBehaviour
{
    [SerializeField] private Unit target;
    [SerializeField] private bool targetExists;

    public bool TargetExists { get => targetExists; set => targetExists = value; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == target.tag)
        {
            targetExists = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == target.tag)
        {
            targetExists = false;
        }
    }
}
