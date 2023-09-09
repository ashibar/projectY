using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLogo : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetAnim(string value)
    {
        animator.SetTrigger(value);
    }
}
