using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CutSceneRender : MonoBehaviour
{
    [SerializeField] public Image leftimage;
    [SerializeField] public Image rightimage;
    [SerializeField] public TextMeshProUGUI name_who_talktext;
    [SerializeField] public TextMeshProUGUI logtext;

    [SerializeField] public List<LogPair> logpair = new List<LogPair>();
    [SerializeField] public int lognum = 0;
    [SerializeField] public int endnum;

   
    public void LogRender()
    {
        leftimage.sprite = logpair[lognum].sprite_list[0]; //left이미지 컷신에 등장
        rightimage.sprite = logpair[lognum].sprite_list[1];

        name_who_talktext.text = logpair[lognum].name_who_talk;
        logtext.text = logpair[lognum].log;
    }
    
    public void NextLog()
    {
        if(lognum < endnum)
        {
            lognum++;
            LogRender();
        }
        else
        {
            gameObject.SetActive(false);
        }
        
    }

    private void Start()
    {
        endnum = logpair.Count - 1;
        LogRender();
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))  //스페이스를 누르면 
        {
            NextLog();              //NextLog함수 실행
        }
    }
}
