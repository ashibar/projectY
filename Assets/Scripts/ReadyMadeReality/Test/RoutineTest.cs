using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class RoutineTest : MonoBehaviour
{
    [SerializeField] private List<string> dialogList = new List<string>() {
        "정말 아름다운 날이야.",
        "새들은 지저귀고,\n@꽃들은 피어나고...",
        "이런날엔,\n@너같은 꼬마들은...",
        "지 옥 에 서 불 타 고 있 어 야 해"
    };
    [SerializeField] private int dialog_cnt;

    [SerializeField] private int word_cnt;
    [SerializeField] private int word_max;
    [SerializeField] private int line_cnt;
    [SerializeField] private int line_max;
    [SerializeField] private int split_cnt;
    [SerializeField] private int split_max;
 
    [SerializeField] private List<List<string>> stringList = new List<List<string>>();
    [SerializeField] private float spd = 0.1f;

    [SerializeField] private string value = "a@b@c";
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private TextMeshProUGUI stateText;
    [SerializeField] private bool stopFlag;
    [SerializeField] private bool phaseEndFlag;
    [SerializeField] private bool dialogEndFlag;
    [SerializeField] private bool coolTimeFlag;
    [SerializeField] private bool isInterrupted;

    private void Start()
    {
        InitString();
    }

    private void Update()
    {
        InputFunction();

        NextDialogCheck();
        SplitCheck();
        BaseFunction();        

    }

    private void InitString()
    {
        // 초기화
        stateText.text = "";
        word_cnt = 0;
        word_max = 0;
        line_cnt = 0;
        line_max = 0;
        split_cnt = 0;
        split_max = 0;
        value = dialogList[dialog_cnt];
        stringList.Clear();

        // 분류
        List<string> tempSplit = new List<string>();
        string[] seps = new string[] { "\\n", "\n" };
        tempSplit.AddRange(value.Split('@'));
        //foreach (string s in tempSplit) Debug.Log("s : " + s);
        split_max = tempSplit.Count;
        foreach (string s in tempSplit)
        {
            stringList.Add(s.Split(seps, StringSplitOptions.None).ToList<string>());
        }
        Debug.Log(string.Format("{0}", stringList[0].Count));
    }

    private void InputFunction()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (stopFlag)
            {
                stopFlag = !stopFlag;
            }
            else
            {
                if (!dialogEndFlag)
                    RenderElse();
            }
        }
    }

    private void NextDialogCheck()
    {
        if (phaseEndFlag && !stopFlag && !dialogEndFlag)
        {
            dialog_cnt++;
            InitString();
            phaseEndFlag = false;
        }
    }

    private void SplitCheck()
    {
        if (split_max <= split_cnt)
        {
            if (stopFlag)
            {
                //Debug.Log("End");
                stopFlag = true;
                phaseEndFlag = true;
                return;
            }
        }
        else
        {
            LineCheck();
        }
    }

    private void LineCheck()
    {
        if (split_max <= split_cnt) return;
        line_max = stringList[split_cnt].Count;

        if (line_max <= line_cnt)
        {
            line_cnt = 0;
            split_cnt++;
            stopFlag = true;
            //LineCheck();
            return;
        }
        else
            WordCheck();
    }

    private void WordCheck()
    {
        if (line_max <= line_cnt) return;
        word_max = stringList[split_cnt][line_cnt].Length;

        if (word_max <= word_cnt)
        {
            word_cnt = 0;
            line_cnt++;
            if (line_max > line_cnt)
                stateText.text += '\n';
            if (dialog_cnt >= dialogList.Count - 1)
                dialogEndFlag = true;
            //WordCheck();
            return;
        }
        else
            return;
    }

    private async void BaseFunction()
    {
        if (!stopFlag && !coolTimeFlag && !dialogEndFlag)
        {
            coolTimeFlag = true;
            await Test_routine(spd);
            coolTimeFlag = false;

        }
    }

    private async Task Test_routine(float duration)
    {
        float end = Time.time + duration;

        countText.text = string.Format("{0}/{1} : {2}/{3} : {4}/{5}", split_cnt, split_max, line_cnt, line_max, word_cnt, word_max);
        stateText.text += stringList[split_cnt][line_cnt][word_cnt];
        word_cnt += 1;

        while (Time.time < end)
        {
            await Task.Yield();
        }
    }

    private void RenderElse()
    {
        // 나머지 출력
        for (int i = line_cnt; i < line_max; i++)
        {
            if (i == line_cnt)
                stateText.text += stringList[split_cnt][i][word_cnt..word_max];
            else
                stateText.text += stringList[split_cnt][i];

            if (i < line_max - 1)
                stateText.text += '\n';
        }

        word_cnt = 0;
        line_cnt = 0;
        split_cnt++;
        stopFlag = true;

        if (dialog_cnt >= dialogList.Count - 1)
            dialogEndFlag = true;
    }
}
