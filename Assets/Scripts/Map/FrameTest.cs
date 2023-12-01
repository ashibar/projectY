using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FrameTest : MonoBehaviour
{
    private TextMeshProUGUI text;
    [SerializeField] private Boss_base_Ai ai;

    private void Awake()
    {
        //Application.targetFrameRate = -1;
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (ai == null)
        {
            ai = FindObjectOfType<Boss_base_Ai>();
        }
        float fps = 1f / Time.unscaledDeltaTime;
        text.text = string.Format("DeltaTime : {0},  FixedTime : {1}, CurrentFrame : {2}, Multiplier : {3}", Time.deltaTime, Time.fixedDeltaTime, fps, 0);
    }
}
