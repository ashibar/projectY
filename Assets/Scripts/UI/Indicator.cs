using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private RectTransform rt;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rt = GetComponent<RectTransform>();
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

    public void SetActive(bool value, Vector2 pos)
    {
        gameObject.SetActive(value);
        rt.anchoredPosition = pos;
    }
}
