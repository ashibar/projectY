using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;
using System;

namespace ReadyMadeReality
{
    public class TextBox : MonoBehaviour
    {
        //[SerializeField] private string value;
        //[SerializeField] private float delay;
        //[SerializeField] private float timeOut = 10f;

        //[SerializeField] private int cnt = 0;
        //[SerializeField] private bool isCooltime;
        //[SerializeField] private bool isInterrupted;
        //[SerializeField] private int interruptCode;
        //[SerializeField] private bool isStopped;
        //[SerializeField] private bool isActive = false;

        //private float startTime;



        //private void Update()
        //{
        //    RenderText(value, delay);
        //}

        //private async void RenderText(string _value, float _delay)
        //{
        //    char[] chars = _value.ToCharArray();
        //    float end = startTime + timeOut;

        //    if (!isActive)
        //        return;

        //    if (cnt >= chars.Length || Time.time >= end)
        //    {
        //        isActive = false;
        //        dialogArea.AddCount();
        //        return;
        //    }

        //    if (!isCooltime)
        //    {
        //        isCooltime = true;
        //        interruptCode = await TextRender_routine(chars[cnt], _delay);
        //        isCooltime = false;
        //    }
        //}

        //private async Task<int> TextRender_routine(char c, float delay)
        //{
        //    float end = Time.time + delay;
        //    if (c == '<')
        //    {
        //        tmpro.text += "<br>";
        //        cnt += 4;
        //    }
        //    else
        //    {
        //        tmpro.text += c;
        //        cnt++;
        //    }

        //    while (Time.time < end)
        //    {
        //        if (isInterrupted)
        //        {
        //            await Task.FromResult(1);
        //        }

        //        await Task.Yield();
        //    }      

        //    return 0;
        //}


        //public async void Activate(List<DialogInfo> list, int _cnt)
        //{
        //    //await CheckIsActivation_routine();
        //    //await Initiate_rountine(list[_cnt].Text_value);
        //}

        //public int ListCount(int cnt)
        //{
        //    return isActive || isStopped ? cnt : cnt + 1;
        //}

        //private async Task CheckIsActivation_routine()
        //{
        //    await Task.Yield();
        //    if (isActive)
        //    {
        //        isActive = false;
        //        isInterrupted = true;
        //        isStopped = true;
        //    }
        //}
        //private async Task Initiate_rountine(string _value)
        //{
        //    if (isStopped)
        //    {
        //        tmpro.text = value;
        //        isStopped = false;
        //        dialogArea.AddCount();
        //        return;
        //    }
        //    else
        //    {            
        //        await Task.Yield();
        //        isInterrupted = false;
        //        await Task.Yield();
        //        value = _value;
        //        isActive = true;
        //        tmpro.text = "";
        //        cnt = 0;
        //        startTime = Time.time;
        //        interruptCode = 0;
        //    }        
        //}

        //private void OnDestroy()
        //{
        //    isInterrupted = true;
        //}

        [SerializeField] private List<DialogInfo> dialog_list;
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
        [SerializeField] private DialogArea dialogArea;
        [SerializeField] private TextMeshProUGUI dialogText;
        [SerializeField] private bool isActive;
        [SerializeField] private bool stopFlag;
        [SerializeField] private bool phaseEndFlag;
        [SerializeField] private bool dialogEndFlag;
        [SerializeField] private bool coolTimeFlag;
        [SerializeField] private bool isInterrupted;

        private void Awake()
        {
            dialogText = GetComponentInChildren<TextMeshProUGUI>();
            dialogArea = GetComponentInParent<DialogArea>();
        }

        private void Start()
        {

        }

        private void Update()
        {
            Input_Key();

            NextDialogCheck();
            SplitCheck();
            BaseFunction();

        }

        public void SetActive(bool value)
        {
            isActive = value;
        }

        public void Init_Dialog(List<DialogInfo> list)
        {
            dialog_list = list.ConvertAll(o => new DialogInfo(o));
            dialog_cnt = 0;
            stopFlag = false;
            phaseEndFlag = false;
            dialogEndFlag = false;
            coolTimeFlag = false;
            InitString();
        }

        private void InitString()
        {
            // 초기화
            Debug.Log("initiated");
            dialogText.text = "";
            word_cnt = 0;
            word_max = 0;
            line_cnt = 0;
            line_max = 0;
            split_cnt = 0;
            split_max = 0;
            value = dialog_list[dialog_cnt].Text_value;
            stringList.Clear();

            // 분류
            List<string> tempSplit = new List<string>();
            string[] seps = new string[] { "\\n", "\n", "<br>" };
            tempSplit.AddRange(value.Split('@'));
            //foreach (string s in tempSplit) Debug.Log("s : " + s);
            split_max = tempSplit.Count;
            foreach (string s in tempSplit)
            {
                stringList.Add(s.Split(seps, StringSplitOptions.None).ToList<string>());
            }
            Debug.Log(string.Format("{0}", stringList[0].Count));
        }

        private void Input_Key()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                InputFunction();
            }
        }

        public void Input_Outer()
        {
            InputFunction();
        }

        private void InputFunction()
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

        private void NextDialogCheck()
        {
            if (isActive && phaseEndFlag && !stopFlag && !dialogEndFlag)
            {
                dialog_cnt++;
                dialogArea.SyncCount(dialog_cnt);
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
                    dialogText.text += '\n';
                if (dialog_cnt >= dialog_list.Count - 1)
                    dialogEndFlag = true;
                //WordCheck();
                return;
            }
            else if (stringList[split_cnt][line_cnt][word_cnt] == '<')
            {
                string temp = stringList[split_cnt][line_cnt];
                int endId = temp[word_cnt..].IndexOf('>') + word_cnt;
                //Debug.Log(temp[word_cnt..(word_cnt + endId + 1)]);
                if (endId >= 0)
                {
                    dialogText.text += temp[word_cnt..(endId + 1)];
                    word_cnt = endId + 1;
                }
            }
            else
                return;
        }

        private async void BaseFunction()
        {
            if (!stopFlag && !coolTimeFlag && !dialogEndFlag)
            {
                coolTimeFlag = true;
                await Base_routine(spd);
                coolTimeFlag = false;

            }
        }

        private async Task Base_routine(float duration)
        {
            float end = Time.time + duration;

            //countText.text = string.Format("{0}/{1} : {2}/{3} : {4}/{5}", split_cnt, split_max, line_cnt, line_max, word_cnt, word_max);
            dialogText.text += stringList[split_cnt][line_cnt][word_cnt];
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
                    dialogText.text += stringList[split_cnt][i][word_cnt..word_max];
                else
                    dialogText.text += stringList[split_cnt][i];

                if (i < line_max - 1)
                    dialogText.text += '\n';
            }

            word_cnt = 0;
            line_cnt = 0;
            split_cnt++;
            stopFlag = true;

            if (dialog_cnt >= dialog_list.Count - 1)
                dialogEndFlag = true;
        }
    }

}