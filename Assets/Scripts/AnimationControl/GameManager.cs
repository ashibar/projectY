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
        //���� isAction�� �ٽ� ����Ǹ�
        if (isAction)
        {
            //false������ ����
            isAction = false;
        }
        else
        {
            //������
            isAction = true;            
            scanObject = ScanObj;
            TalkText.text = "this name is" + scanObject.name;
        }
        //��ȭâ ���� ���� ��
        talkPanel.SetActive(isAction);

        // ��ȭ�� ĳ���� ��ġ �����ϴ¹� �÷��̾� ��ũ��Ʈ���� isAction���� ������ ĳ���͸� ���߰� �ؾߵ�.
    }
}
