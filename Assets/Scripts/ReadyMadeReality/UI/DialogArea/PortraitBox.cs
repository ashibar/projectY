using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ReadyMadeReality
{
    public class PortraitBox : MonoBehaviour
    {
        protected Image image;
        [SerializeField] protected PortraitInfo_so portraitInfo_so;

        private void Awake()
        {
            image = GetComponentInChildren<Image>();
        }

        public virtual void SetSprite(List<DialogInfo> list, int _cnt)
        {

        }
    } 
}
