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
        
        ////���ʿ� ������Ǵ�?�žƴѰ�
        ////�޾ƿ��� �Լ�? �� �� ������ ÷�� �޾ƿ͵�
        ////�Ǵ°žƴѰ��� Ȯ�� ��
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
        //�������� ����? ���ɳѱ����¸𸣰ٴµ�
        //���� �̷��� ���ڷι޾Ƽ� �Ѱܵ��ǰ�
        //��Ʈ������??��
        //��
        //Vector3 cameraCenter = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        //Debug.Log(cameraCenter);
        //// ��鿡��

        ////�׷� ķ���� �߾���ǥ���޴°�?

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

    //�̺κ����׷� �����տ� �Ѱܼ� ������
    //�Ǵ�?��
}
