using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;
using UnityEngine.UI;

namespace ReadyMadeReality
{
    public class DialogBox : MonoBehaviour
    {
        public NameBox nameBox;
        private TextBox textBox;
        private RectTransform rt;
        private Image image;

        private void Awake()
        {
            nameBox = GetComponentInChildren<NameBox>();
            textBox = GetComponentInChildren<TextBox>();
            rt = GetComponent<RectTransform>();
            image = GetComponent<Image>();
        }

        public void SetActive(bool value)
        {
            textBox.SetActive(value);
        }

        public void Init_dialog(List<DialogInfo> list)
        {
            textBox.Init_Dialog(list);
        }

        public void RenderText(List<DialogInfo> list, int _cnt)
        {
            textBox.Input_Outer();
            //nameBox.SetName(list, _cnt);
        }

        public void FormByMode(DialogMode mode)
        {
            switch (mode)
            {
                case DialogMode.Normal:
                    rt.offsetMin = new Vector2(0, rt.offsetMin.y);
                    rt.offsetMax = new Vector2(-0, -rt.offsetMax.y);
                    rt.sizeDelta = new Vector2(rt.sizeDelta.x, 400);
                    image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);
                    break;
                case DialogMode.Battle:
                    rt.offsetMin = new Vector2(100, rt.offsetMin.y);
                    rt.offsetMax = new Vector2(-100, -rt.offsetMax.y);
                    rt.sizeDelta = new Vector2(rt.sizeDelta.x, 300);
                    image.color = new Color(image.color.r, image.color.g, image.color.b, 0.6f);
                    break;
            }
        }

        public void SetAuto(bool isAuto)
        {
            textBox.SetAuto(isAuto);
        }
    } 
}
