using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ReadyMadeReality
{
    public class PortraitBox : MonoBehaviour
    {
        protected Image image;
        protected RectTransform rt;

        private void Awake()
        {
            image = GetComponentInChildren<Image>();
            rt = GetComponent<RectTransform>();
        }

        public virtual void SetSprite(DialogInfo_so dialogInfo_so, int _cnt)
        {

        }

        public void FormByMode(DialogMode mode)
        {
            switch (mode)
            {
                case DialogMode.Normal:
                    rt.anchoredPosition = new Vector2(-100, 210);
                    rt.sizeDelta = new Vector2(600, 750);
                    break;
                case DialogMode.Battle:
                    rt.anchoredPosition = new Vector2(-100, 140);
                    rt.sizeDelta = new Vector2(360, 450);
                    break;
            }
        }
    } 
}
