using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (instance == null) // instance�� ����ִ�
            {
                var obj = FindObjectOfType<UIManager>();
                if (obj != null)
                {
                    instance = obj;                                             // ��ü ã�ƺôµ�? �ֳ�? �װ� ����
                }
                else
                {
                    var newObj = new GameObject().AddComponent<UIManager>(); // ��ü ã�ƺôµ�? ����? ���θ�����
                    instance = newObj;
                }
            }
            return instance; // �Ⱥ���ֳ�? �׳� �״�� ������
        }
    }

    [SerializeField] private ScreenFade screenFade;
    [SerializeField] private Gameover gameover;

    private void Awake()
    {
        var objs = FindObjectsOfType<UIManager>();
        if (objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Update()
    {
        EventReciever();
        EventListener();
        gameoverListener();
    }

    [SerializeField] private List<EventMessage> messageBuffer = new List<EventMessage>();
    private void EventReciever()
    {
        int error = StageManager.Instance.SearchMassage(5, messageBuffer);
        if (error == -1)
            return;
    }

    private void EventListener()
    {
        bool isError = false;
        foreach (EventMessage m in messageBuffer)
        {
            switch (m.ActionSTR)
            {
                case "Fade Out":
                    screenFade.SetScreenFade(false);
                    break;
                case "Fade In":
                    screenFade.SetScreenFade(true);
                    break;
                default:
                    isError = true;
                    break;
            }

            if (!isError)
            {
                messageBuffer.Remove(m);
            }
        }

    }
    Player player = null;
    private void gameoverListener()
    {
        player = Player.instance;
        if (player.stat.Hp_current <= 0)
        {
            gameover.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
