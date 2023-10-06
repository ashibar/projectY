using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ReadyMadeReality
{
    public class NameBox : MonoBehaviour
    {
        private RectTransform rt;
        private TextMeshProUGUI tmpro;
        private Image nameBoxImage;

        private void Awake()
        {
            rt = GetComponent<RectTransform>();
            tmpro = GetComponentInChildren<TextMeshProUGUI>();
            nameBoxImage = GetComponentInChildren<Image>();
        }

        public void SetName(DialogInfo_so dialogInfo_so, int _cnt)
        {
            tmpro.text = dialogInfo_so.DialogList[_cnt].Text_name;
            nameBoxImage.color = dialogInfo_so.DialogList[_cnt].NameColor;
            gameObject.SetActive(dialogInfo_so.DialogList[_cnt].EnableNameBox);
            SetPosition(dialogInfo_so.DialogList[_cnt].NameBoxPos);
        }

        private void SetPosition(NameBoxPosPreset nameBoxPosPreset)
        {
            switch (nameBoxPosPreset)
            {
                case NameBoxPosPreset.Left:
                    rt.anchoredPosition = new Vector2(300, 50);
                    rt.anchorMax = new Vector2(0, 1);
                    rt.anchorMin = new Vector2(0, 1);
                    rt.pivot = new Vector2(0, 1);
                    break;
                case NameBoxPosPreset.Middle:
                    rt.anchoredPosition = new Vector2(0, 50);
                    rt.anchorMax = new Vector2(0.5f, 1);
                    rt.anchorMin = new Vector2(0.5f, 1);
                    rt.pivot = new Vector2(0.5f, 1);
                    break;
                case NameBoxPosPreset.Right:
                    rt.anchoredPosition = new Vector2(-300, 50);
                    rt.anchorMax = new Vector2(1, 1);
                    rt.anchorMin = new Vector2(1, 1);
                    rt.pivot = new Vector2(1, 1);
                    break;

            }
        }
    }

}