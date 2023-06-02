using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Awake()
    {
        animator = Player.Instance.GetComponent<Animator>();
    }

    public void SetAnimation(string action)
    {
        switch (action)
        {
            case "Move":
                animator.SetTrigger("isMove");
                break;
            case "Walk":
                Debug.Log("Walk");
                animator.SetTrigger("isWalk");
                break;
            case "Run":
                animator.SetTrigger("isRun");
                break;
            case "Idle":
                Debug.Log("Idle");
                animator.SetTrigger("isIdle");
                break;
        }
    }
}
