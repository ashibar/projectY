using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimationTest : MonoBehaviour
{
    [SerializeField] private List<GameObject> obj_list;

    public bool first;
    public bool second;

    private void Update()
    {
        if (first)
        {
            first = false;
            obj_list[0].GetComponent<Animator>().SetTrigger("go");
        }
        if (second)
        {
            second = false;
            obj_list[1].GetComponent<Animator>().SetTrigger("go");
        }
    }

    
}
