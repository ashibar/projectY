using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ConsoleOutputReader : MonoBehaviour
{
    // ���������� ��µ� �ܼ� �޽����� ������ ����
    private string lastConsoleMessage = "";
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private bool verbose;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        // �ܼ� â�� �α� �޽����� �̺�Ʈ�� ����
        Application.logMessageReceived += HandleLog;
    }

    private void HandleLog(string logText, string stackTrace, LogType logType)
    {
        // �α� �޽����� ��µ� ������ ȣ��Ǵ� �޼���
        // ���⿡�� ���������� ��µ� �޽����� ������Ʈ
        if (logType == LogType.Error)
        {
            lastConsoleMessage = logText;
        }
    }

    private void Update()
    {
        // ���������� ��µ� �ܼ� �޽����� ����Ϸ��� ������ ���� ������ �� �ֽ��ϴ�.
        if (verbose)
        {
            text.gameObject.SetActive(true);
            text.text = lastConsoleMessage;
        }
        else
        {
            text.gameObject.SetActive(false);
        }        
    }

    private void OnDestroy()
    {
        // ��ũ��Ʈ�� �ı��� �� ���� ����
        Application.logMessageReceived -= HandleLog;
    }
}




