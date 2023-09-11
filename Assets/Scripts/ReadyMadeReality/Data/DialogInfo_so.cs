using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReadyMadeReality
{
    [CreateAssetMenu(fileName = "Default DialogList", menuName = "Scriptable Object/DialogList", order = 0)]
    public class DialogInfo_so : ScriptableObject
    {
        [SerializeField] private List<DialogInfo> dialogList = new List<DialogInfo>();
        [SerializeField] private PortraitInfo_so portraitList;

        public List<DialogInfo> DialogList { get => dialogList; set => dialogList = value; }
        public PortraitInfo_so PortraitList { get => portraitList; set => portraitList = value; }
    } 
}
