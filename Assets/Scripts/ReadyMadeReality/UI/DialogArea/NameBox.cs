using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ReadyMadeReality
{
    public class NameBox : MonoBehaviour
    {
        private TextMeshProUGUI tmpro;
        private Image nameBoxImage;

        private void Awake()
        {
            tmpro = GetComponentInChildren<TextMeshProUGUI>();
            nameBoxImage = GetComponentInChildren<Image>();
        }

        public void SetName(List<DialogInfo> list, int _cnt)
        {
            tmpro.text = list[_cnt].Text_name;
            nameBoxImage.color = list[_cnt].NameColor;
            gameObject.SetActive(list[_cnt].EnableNameBox);
        }
    }

}