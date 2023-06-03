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
            if (instance == null) // instance가 비어있다
            {
                var obj = FindObjectOfType<UIManager>();
                if (obj != null)
                {
                    instance = obj;                                             // 전체 찾아봤는데? 있네? 그걸 넣자
                }
                else
                {
                    var newObj = new GameObject().AddComponent<UIManager>(); // 전체 찾아봤는데? 없네? 새로만들자
                    instance = newObj;
                }
            }
            return instance; // 안비어있네? 그냥 그대로 가져와
        }
    }


    [SerializeField] private ScreenFade screenFade;
    [SerializeField] private Gameover gameover;
    [SerializeField] private Indicator indicator_keyboard;
    [SerializeField] private Indicator indicator_centerImage;
    [SerializeField] private ResultWindow resultWindow;
    [SerializeField] private TopIndicator topIndicator;

    public ResultWindow ResultWindow { get => resultWindow; set => resultWindow = value; }
    public TopIndicator TopIndicator { get => topIndicator; set => topIndicator = value; }

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
        EventMessage temp = new EventMessage();
        if (messageBuffer.Count <= 0)
            return;

        foreach (EventMessage m in messageBuffer)
        {
            switch (m.ActionSTR)
            {
                case "Fade Out":
                    screenFade.SetScreenFade(false);
                    messageBuffer.Remove(m);
                    return;
                case "Fade In":
                    screenFade.SetScreenFade(true);
                    messageBuffer.Remove(m);
                    return;
                case "KeyBoard Indicator":
                    indicator_keyboard.SetAnim("Out");
                    messageBuffer.Remove(m);
                    return;
                case "Center Indicator":
                    indicator_centerImage.gameObject.SetActive(string.Equals(m.TargetSTR, "true"));
                    messageBuffer.Remove(m);
                    return;
                case "Force Load":
                    LoadingSceneController.LoadScene("BattleScene", 1);
                    messageBuffer.Remove(m);
                    return;
                default:
                    isError = true;
                    break;
            }

            
        }

        if (!isError)
        {
            messageBuffer.Remove(temp);
        }
    }
    Player player = null;
    private void gameoverListener()
    {
        player = Player.Instance;
        if (player.stat.Hp_current <= 0)
        {
            gameover.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
