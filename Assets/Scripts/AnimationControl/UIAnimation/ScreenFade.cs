using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFade : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetScreenFade(bool isIN)
    {
        if (isIN)
        {
            animator.SetTrigger("isIn");
        }
        else
        {
            animator.SetTrigger("isOut");
        }
    }
}
