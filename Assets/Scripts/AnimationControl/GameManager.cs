using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject talkPanel;
    public Text TalkText;
    public GameObjext scanObject;
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
            isAction = ture;            
            scanObject = ScanObj;
            TalkText.text = "this name is" + scanObject.name;
        }
        //��ȭâ ���� ���� ��
        talkPanel.SetActive(isAction);

        // ��ȭ�� ĳ���� ��ġ �����ϴ¹� �÷��̾� ��ũ��Ʈ���� isAction���� ������ ĳ���͸� ���߰� �ؾߵ�.
    }
}
