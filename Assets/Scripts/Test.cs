using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// ��ũ��Ʈ �̸� : Test
/// ����� : �̿��
/// ��� : ����׿� ��ũ��Ʈ. �ڷ������� ������ SetText�Լ� ���
/// ��� :
/// ������Ʈ ���� :
///     - (23.04.05) : ����� �Լ� ����
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
