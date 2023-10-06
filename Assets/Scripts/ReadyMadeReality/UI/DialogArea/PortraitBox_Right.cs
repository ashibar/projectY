using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReadyMadeReality
{
    public class PortraitBox_Right : PortraitBox
    {
        public override void SetSprite(DialogInfo_so dialogInfo_so, int _cnt)
        {
            base.SetSprite(dialogInfo_so, _cnt);
            image.sprite = dialogInfo_so.PortraitList.portraitList[dialogInfo_so.DialogList[_cnt].Right_portrait_id];
        }
    }

}