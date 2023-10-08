using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// 스크립트 이름 : Test
/// 담당자 : 이용욱
/// 요약 : 디버그용 스크립트. 자료형으로 접근해 SetText함수 사용
/// 비고 :
/// 업데이트 내역 :
///     - (23.04.05) : 디버그 함수 생성
/// </summary>

public class Test : MonoBehaviour
{
    public static Action<string> SetText;
    
    private TextMeshProUGUI text;

    private void Awake()
    {
        SetText = SetDebugText;
        text = GetComponent<TextMeshProUGUI>();
    }

    public void SetDebugText(string value)
    {
        text.text = value;
        Debug.Log("abcd");
    }
}
