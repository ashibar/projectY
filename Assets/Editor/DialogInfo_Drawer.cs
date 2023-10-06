using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using Unity.VisualScripting;

namespace ReadyMadeReality
{
    [CustomEditor(typeof(DialogInfo_so))]
    public class DialogInfo_Drawer : Editor
    {
        private int index = 0;
        private int preview_id = 0;
        private Sprite preview_sprite;

        public override void OnInspectorGUI()
        {
            DialogInfo_so so = (DialogInfo_so)target;

            GUIStyle indexFieldStyle = new GUIStyle(EditorStyles.textField);
            indexFieldStyle.alignment = TextAnchor.MiddleRight;

            GUILayoutOption[] smallFieldOption =
            {
            GUILayout.Width(50),
            GUILayout.Height(20)
        };
            GUILayoutOption[] indexFieldOption =
            {
            GUILayout.Width(210),
            GUILayout.Height(20)
        };
            GUILayoutOption[] buttonOption =
            {
            GUILayout.Width(20),
            GUILayout.Height(20)
        };
            GUILayoutOption[] textFieldOptions =
            {
            GUILayout.ExpandWidth(true),
            GUILayout.Height(200)
        };
            GUILayoutOption[] saveButtonOptions =
            {
            GUILayout.Width(150),
            GUILayout.Height(30)
        };
            GUILayoutOption[] previewSpriteOptions =
            {
            GUILayout.Width(200),
            GUILayout.Height(250)
        };

            EditorGUILayout.LabelField("Dialog Infomation", EditorStyles.boldLabel);
            so.PortraitList = (PortraitInfo_so)EditorGUILayout.ObjectField("Portrait List", so.PortraitList, typeof(PortraitInfo_so), true);


            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Dialog List", EditorStyles.boldLabel);

            if (so.DialogList != null)
            {
                // 대화 로그 인덱스
                if (so.DialogList.Count <= 0) so.DialogList.Add(new DialogInfo());
                GUILayout.BeginHorizontal();
                index = EditorGUILayout.IntField("Dialog Index", Mathf.Clamp(index, 0, so.DialogList.Count - 1), indexFieldStyle, indexFieldOption);
                if (GUILayout.Button("<", buttonOption))
                {
                    index = index > 0 ? index - 1 : index;
                }
                if (GUILayout.Button(">", buttonOption))
                {
                    index = index < so.DialogList.Count - 1 ? index + 1 : index;
                }
                EditorGUILayout.LabelField("Max", smallFieldOption);
                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.IntField(so.DialogList.Count, indexFieldStyle, smallFieldOption);
                EditorGUI.EndDisabledGroup();
                if (GUILayout.Button("+", buttonOption))
                {
                    if (so.DialogList.Count > 0)
                        so.DialogList.Add(new DialogInfo(so.DialogList[so.DialogList.Count - 1]));
                    else
                        so.DialogList.Add(new DialogInfo());
                }
                if (GUILayout.Button("-", buttonOption))
                {
                    if (so.DialogList.Count > 0)
                    {
                        so.DialogList.RemoveAt(so.DialogList.Count - 1);
                        index = index >= so.DialogList.Count ? so.DialogList.Count - 1 : index;
                    }
                    else
                        Debug.Log("Already Empty");
                }
                GUILayout.EndHorizontal();

                if (so.DialogList.Count > 0)
                {
                    // 대화자 텍스트
                    GUILayout.BeginHorizontal();
                    so.DialogList[index].Text_name = EditorGUILayout.TextField("Name", so.DialogList[index].Text_name);
                    so.DialogList[index].EnableNameBox = EditorGUILayout.Toggle(so.DialogList[index].EnableNameBox);

                    GUILayout.EndHorizontal();

                    // 대화자 이름 텍스트 박스
                    GUILayout.BeginHorizontal();
                    so.DialogList[index].NameColor = EditorGUILayout.ColorField("NameBox Color", so.DialogList[index].NameColor);
                    so.DialogList[index].ColorPreset = (NameColorPreset)EditorGUILayout.EnumPopup(so.DialogList[index].ColorPreset);
                    so.DialogList[index].NameColor = SetColor(so.DialogList[index].ColorPreset, so.DialogList[index].NameColor);
                    so.DialogList[index].NameBoxPos = (NameBoxPosPreset)EditorGUILayout.EnumPopup(so.DialogList[index].NameBoxPos);
                    //so.DialogList[index].NameBoxPos = EditorGUILayout.Popup(so.DialogList[index].NameBoxPos,
                    GUILayout.EndHorizontal();

                    // 대화 로그 텍스트
                    EditorGUILayout.PrefixLabel("Log", EditorStyles.boldLabel);
                    so.DialogList[index].Text_value = EditorGUILayout.TextArea(so.DialogList[index].Text_value, textFieldOptions);
                }
            }
            // 왼쪽 초상화
            GUILayout.BeginHorizontal();
            so.DialogList[index].Left_portrait_id = EditorGUILayout.IntField("Left Portrait id", so.DialogList[index].Left_portrait_id, indexFieldStyle, indexFieldOption);
            //if (so.PortraitList != null)
            //    so.DialogList[index].Left_portrait_id = EditorGUILayout.ObjectField()
            GUILayout.EndHorizontal();

            // 오른쪽 초상화
            GUILayout.BeginHorizontal();
            so.DialogList[index].Right_portrait_id = EditorGUILayout.IntField("Right Portrait id", so.DialogList[index].Right_portrait_id, indexFieldStyle, indexFieldOption);
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Save", saveButtonOptions))
            {
                EditorUtility.SetDirty(target);
            }

            if (so.PortraitList != null)
            {
                preview_id = EditorGUILayout.IntField("Preview", Mathf.Clamp(preview_id, 0, so.PortraitList.portraitList.Count - 1), indexFieldOption);
                if (so.PortraitList.portraitList.Count > 0)
                {
                    preview_sprite = so.PortraitList.portraitList[Mathf.Clamp(preview_id, 0, so.PortraitList.portraitList.Count - 1)];
                    var texture = AssetPreview.GetAssetPreview(preview_sprite);
                    GUILayout.Label(texture, previewSpriteOptions);
                }
            }
            // 기본 Inspector 표시
            //DrawDefaultInspector();
        }

        private Color SetColor(NameColorPreset colorPreset, Color c)
        {
            Color nameColor = c;

            switch (colorPreset)
            {
                case NameColorPreset.Custom:
                    break;
                case NameColorPreset.Red:
                    nameColor = Color.red;
                    break;
                case NameColorPreset.Orange:
                    nameColor = new Color(1, 0.5f, 0);
                    break;
                case NameColorPreset.Green:
                    nameColor = Color.green;
                    break;
                case NameColorPreset.Blue:
                    nameColor = Color.blue;
                    break;
                default:
                    break;
            }

            return nameColor;
        }
    } 
}
