using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading;

public class StageinfoText : AsyncFunction_template
{
    //public TextMeshProUGUI textMeshPro;
    //public float mapnumber;

    ////[SerializeField]
    ////private float alphaspeed;
    //[SerializeField]
    //private float textdestroytime;
    //private Camera mainCamera;
    //public GameObject sip;

    //[SerializeField]
    //private Transform canvas;

    //[SerializeField]
    //public string mapclear;

    [SerializeField] TextMeshProUGUI stage_name_text;
    [SerializeField] TextMeshProUGUI stage_condition_text;

    [SerializeField] private Animator animator;

    private CancellationTokenSource cts = new CancellationTokenSource();

    private void Awake()
    {
        animator = GetComponent<Animator>();
        
        ////이쪽에 넣으면되는?거아닌가
        ////받아오는 함수? 네 아 정보는 첨에 받아와도
        ////되는거아닌가요 확인 네
        ////alphaspeed = 2.0f;
        //textdestroytime = 2.0f;        
        //if (mainCamera == null)
        //{
        //    mainCamera = Camera.main;
        //}        
        //Active(mapnumber,mapclear);
    }
    public async void Active(string mapName,string mapCondition)
    {
        stage_name_text.text = mapName;
        stage_condition_text.text = mapCondition;
        animator.SetTrigger("isAppears");

        await Wait(cts.Token, 3f);
        
        if (gameObject != null)
            gameObject.SetActive(false);
        //스테이지 조건? 어케넘길지는모르겟는데
        //섬멸 이런거 숫자로받아서 넘겨도되고
        //스트링으로??네
        //예
        //Vector3 cameraCenter = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        //Debug.Log(cameraCenter);
        //// 평면에서

        ////그럼 캠버스 중앙좌표를받는게?

        //GameObject newTextObject = Instantiate(sip, canvas);
        //newTextObject.name = "stagename";
        //textMeshPro = newTextObject.GetComponent<TextMeshProUGUI>();


        //textMeshPro.text = string.Format("{0}stage\n{1}", mapnumber,mapclear);
        



        //Invoke(nameof(DestroyText), textdestroytime);

    }

    private void OnDestroy()
    {
        cts?.Cancel();
    }

    //이부분은그럼 프리팹에 넘겨서 없에야
    //되는?에
}
