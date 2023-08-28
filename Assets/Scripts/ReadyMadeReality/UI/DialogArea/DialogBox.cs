using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;

namespace ReadyMadeReality
{
    public class DialogBox : MonoBehaviour
    {
        public NameBox nameBox;
        private TextBox textBox;

        private void Awake()
        {
            nameBox = GetComponentInChildren<NameBox>();
            textBox = GetComponentInChildren<TextBox>();
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
    } 
}
