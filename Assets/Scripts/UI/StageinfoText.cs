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
        
        
        //이쪽에 넣으면되는?거아닌가
        //받아오는 함수? 네 아 정보는 첨에 받아와도
        //되는거아닌가요 확인 네
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
        //스테이지 조건? 어케넘길지는모르겟는데
        //섬멸 이런거 숫자로받아서 넘겨도되고
        //스트링으로??네
        //예
        Vector3 cameraCenter = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        Debug.Log(cameraCenter);
        // 평면에서

        //그럼 캠버스 중앙좌표를받는게?

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

    //이부분은그럼 프리팹에 넘겨서 없에야
    //되는?에
}
