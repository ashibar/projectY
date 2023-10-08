using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ConsoleOutputReader : MonoBehaviour
{
    // 마지막으로 출력된 콘솔 메시지를 저장할 변수
    private string lastConsoleMessage = "";
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private bool verbose;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        // 콘솔 창의 로그 메시지를 이벤트로 구독
        Application.logMessageReceived += HandleLog;
    }

    private void HandleLog(string logText, string stackTrace, LogType logType)
    {
        // 로그 메시지가 출력될 때마다 호출되는 메서드
        // 여기에서 마지막으로 출력된 메시지를 업데이트
        if (logType == LogType.Error)
        {
            lastConsoleMessage = logText;
        }
    }

    private void Update()
    {
        // 마지막으로 출력된 콘솔 메시지를 사용하려면 다음과 같이 접근할 수 있습니다.
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
        // 스크립트가 파괴될 때 구독 해제
        Application.logMessageReceived -= HandleLog;
    }
}




