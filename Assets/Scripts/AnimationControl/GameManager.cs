using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject talkPanel;
    public TextMeshProUGUI TalkText;
    public GameObject scanObject;
    public bool isAction;

    public void Action(GameObject ScanObj)
    {
        //만약 isAction이 다시 실행되면
        if (isAction)
        {
            //false값으로 변경
            isAction = false;
        }
        else
        {
            //값저장
            isAction = true;            
            scanObject = ScanObj;
            TalkText.text = "this name is" + scanObject.name;
        }
        //대화창 켜짐 꺼짐 값
        talkPanel.SetActive(isAction);

        // 대화시 캐릭터 위치 고정하는법 플레이어 스크립트에서 isAction값을 받으면 캐릭터를 멈추게 해야됨.
    }
}
