using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReadyMadeReality
{
    public class PortraitBox_Left : PortraitBox
    {
        public override void SetSprite(List<DialogInfo> list, int _cnt)
        {
            base.SetSprite(list, _cnt);
            image.sprite = portraitInfo_so.portraitList[list[_cnt].Left_portrait_id];
        }
    } 
}
