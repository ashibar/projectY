using ReadyMadeReality;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace ReadyMadeReality
{
    public class RenderText : MonoBehaviour
    {
        [SerializeField] private float renderDelay = 0.1f;

        [SerializeField] private DialogArea dialogArea;
        [SerializeField] private TextMeshProUGUI dialogText;

        [SerializeField] private List<DialogInfo> dialog_list;
        // Start is called before the first frame update

        [SerializeField] private bool isCooltime;
        [SerializeField] private bool isInput;
        [SerializeField] private bool isAuto;
        [SerializeField] private bool isWait;

        [SerializeField] private string teststring = "abc@def\nghi@jkl";
        [SerializeField] private int cnt;

        private CancellationTokenSource cts;

        private void Awake()
        {
            dialogText = GetComponentInChildren<TextMeshProUGUI>();
            dialogArea = GetComponentInParent<DialogArea>();
        }

        private async void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isInput = true;
            }
            
            if (!isCooltime)
            {
                isCooltime = true;
                string s = ReadBuffer();
                await Render_routine(s);
                isCooltime = false;
            }

            isInput = false;
        }

        private string ReadBuffer()
        {
            if (isAuto)
            {
                if (isInput)
                {
                    if (isWait)
                    {
                        isWait = false;
                        isInput = false;
                        if (teststring[cnt] == '@')
                        {
                            isWait = true;
                            cnt++;
                            return "";
                        }
                        else if (teststring[cnt] == '\n')
                        {
                            return '\n'.ToString();
                        }
                        else
                        {
                            return teststring[cnt++].ToString();
                        }
                    }
                    
                }
                else
                {
                    if (teststring[cnt] == '@')
                    {
                        isWait = true;
                        return teststring[cnt++].ToString();
                    }
                    else if (teststring[cnt] == '\n')
                    {
                        return '\n'.ToString();
                    }
                    else
                    {
                        return teststring[cnt++].ToString();
                    }
                }
            }
            else
            {

            }
            return "";
        }

        private async Task Render_routine(string s)
        {
            float end = Time.time + renderDelay;

            dialogText.text += s;

            while (Time.time < end && !cts.Token.IsCancellationRequested)
            {
                await Task.Delay(1);
            }
        }

        private string AddText(char c)
        {
            return c.ToString();
        }

        private void OnDestroy()
        {
            cts?.Cancel();
        }
    } 
}
