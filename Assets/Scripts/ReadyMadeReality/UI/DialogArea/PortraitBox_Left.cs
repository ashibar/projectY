using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReadyMadeReality
{
    public class PortraitBox_Left : PortraitBox
    {
        public override void SetSprite(DialogInfo_so dialogInfo_so, int _cnt)
        {
            base.SetSprite(dialogInfo_so, _cnt);
            image.sprite = dialogInfo_so.PortraitList.portraitList[dialogInfo_so.DialogList[_cnt].Left_portrait_id];
        }

        public override void FormByMode(DialogMode mode)
        {
            switch (mode)
            {
                case DialogMode.Normal:
                    rt.anchoredPosition = new Vector2(100, 210);
                    rt.sizeDelta = new Vector2(600, 750);
                    break;
                case DialogMode.Battle:
                    rt.anchoredPosition = new Vector2(100, 140);
                    rt.sizeDelta = new Vector2(360, 450);
                    break;
            }
        }
    } 
}
