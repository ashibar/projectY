using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    [SerializeField] private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void SetAnim(string action)
    {
        switch (action)
        {
            case "Out":
                anim.SetTrigger("isOut");
                break;
            case "In":
                break;
        }
    }
}
