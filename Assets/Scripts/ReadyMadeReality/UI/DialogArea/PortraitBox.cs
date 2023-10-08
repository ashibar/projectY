using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ReadyMadeReality
{
    public class PortraitBox : MonoBehaviour
    {
        protected Image image;
        
        private void Awake()
        {
            image = GetComponentInChildren<Image>();
        }

        public virtual void SetSprite(DialogInfo_so dialogInfo_so, int _cnt)
        {

        }
    } 
}
