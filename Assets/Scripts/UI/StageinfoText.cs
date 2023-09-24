using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StageinfoText : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public float mapnumber;
    
    [SerializeField]
    private float alphaspeed;
    [SerializeField]
    private float textdestroytime;
    private Camera mainCamera;
    public GameObject sip;

    [SerializeField]
    private Transform canvas;

    [SerializeField]
    public string mapclear;
    
    

    private void Awake()
    {
        
        
        //���ʿ� ������Ǵ�?�žƴѰ�
        //�޾ƿ��� �Լ�? �� �� ������ ÷�� �޾ƿ͵�
        //�Ǵ°žƴѰ��� Ȯ�� ��
        alphaspeed = 2.0f;
        textdestroytime = 2.0f;

        
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
        
        Mapinfo(mapnumber,mapclear);
    }
    public void Mapinfo(float mapnumber,string mapclear)
    {
        //�������� ����? ���ɳѱ����¸𸣰ٴµ�
        //���� �̷��� ���ڷι޾Ƽ� �Ѱܵ��ǰ�
        //��Ʈ������??��
        //��
        Vector3 cameraCenter = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        Debug.Log(cameraCenter);
        // ��鿡��

        //�׷� ķ���� �߾���ǥ���޴°�?

        GameObject newTextObject = Instantiate(sip, canvas);
        newTextObject.name = "stagename";
        textMeshPro = newTextObject.GetComponent<TextMeshProUGUI>();


        textMeshPro.text = string.Format("{0}stage\n{1}", mapnumber,mapclear);
        



        Invoke(nameof(DestroyText), textdestroytime);

    }

    private void Start()
    {

    }
    
    private void DestroyText()
    {

        Destroy(gameObject);
    }

    //�̺κ����׷� �����տ� �Ѱܼ� ������
    //�Ǵ�?��
}
