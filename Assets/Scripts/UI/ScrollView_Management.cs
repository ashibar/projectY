using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollView_Management : MonoBehaviour
{
    [SerializeField] public RectTransform content_rt;

    private void Awake()
    {
        content_rt = GetComponentsInChildren<RectTransform>()[3];
    }
}
