using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReadyMadeReality
{
    [System.Serializable]
    public class DialogInfo
    {
        [SerializeField] private int left_portrait_id;
        [SerializeField] private int right_portrait_id;
        [SerializeField] private string text_name;
        [SerializeField] private string text_value;
        [SerializeField] private Color32 nameColor = new Color32();
        [SerializeField] private NameColorPreset colorPreset = NameColorPreset.Custom;
        [SerializeField] private bool enableNameBox = true;

        public DialogInfo()
        {
            this.left_portrait_id = 0;
            this.right_portrait_id = 0;
            this.text_name = "";
            this.text_value = "";
            this.nameColor = new Color();
            this.colorPreset = NameColorPreset.Custom;
            this.enableNameBox = true;
        }

        public DialogInfo(DialogInfo info)
        {
            this.left_portrait_id = info.Left_portrait_id;
            this.right_portrait_id = info.Right_portrait_id;
            this.text_name = info.Text_name;
            this.text_value = info.Text_value;
            this.nameColor = info.NameColor;
            this.colorPreset = info.ColorPreset;
            this.enableNameBox = info.EnableNameBox;
        }

        public int Left_portrait_id { get => left_portrait_id; set => left_portrait_id = value; }
        public int Right_portrait_id { get => right_portrait_id; set => right_portrait_id = value; }
        public string Text_name { get => text_name; set => text_name = value; }
        public string Text_value { get => text_value; set => text_value = value; }
        public Color32 NameColor { get => nameColor; set => nameColor = value; }
        public NameColorPreset ColorPreset { get => colorPreset; set => colorPreset = value; }
        public bool EnableNameBox { get => enableNameBox; set => enableNameBox = value; }
    }

    public enum NameColorPreset
    {
        Custom,
        Red,
        Orange,
        Green,
        Blue
    } 
}
