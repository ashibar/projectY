using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

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
            GUILayoutOption[] OnlyLabelOption =
            {
            GUILayout.Width(110),
            GUILayout.Height(20)
        };

            EditorGUILayout.LabelField("Dialog Infomation", EditorStyles.boldLabel);
            so.PortraitList = (PortraitInfo_so)EditorGUILayout.ObjectField("Portrait List", so.PortraitList, typeof(PortraitInfo_so), true);
            so.IsAuto = EditorGUILayout.Toggle("Auto Mode", so.IsAuto);


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
                    GUILayout.BeginHorizontal();
                    so.DialogList[index].Sort = (DialogSort)EditorGUILayout.EnumPopup("Dialog Sort", so.DialogList[index].Sort);
                    GUILayout.EndHorizontal();

                    if (so.DialogList[index].Sort == DialogSort.Dialog)
                    {
                        DialogArea(so.DialogList[index], textFieldOptions);
                    }
                    if (so.DialogList[index].Sort == DialogSort.SelectWindow)
                    {
                        DialogArea(so.DialogList[index], textFieldOptions);
                        GUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("Event Phase List", OnlyLabelOption);
                        EditorGUILayout.LabelField("Max", smallFieldOption);
                        EditorGUI.BeginDisabledGroup(true);
                        EditorGUILayout.IntField(so.DialogList[index].PhaseList.Count, indexFieldStyle, smallFieldOption);
                        EditorGUI.EndDisabledGroup();
                        if (GUILayout.Button("+", buttonOption))
                        {
                            if (so.DialogList[index].PhaseList.Count > 0)
                                so.DialogList[index].PhaseList.Add(null);
                            else
                                so.DialogList[index].PhaseList.Add(null);
                        }
                        if (GUILayout.Button("-", buttonOption))
                        {
                            if (so.DialogList[index].PhaseList.Count > 0)
                            {
                                so.DialogList[index].PhaseList.RemoveAt(so.DialogList[index].PhaseList.Count - 1);
                            }
                            else
                                Debug.Log("Already Empty");
                        }
                        GUILayout.EndHorizontal();
                        for (int i = 0; i < so.DialogList[index].PhaseList.Count; i++)
                        {
                            GUILayout.BeginHorizontal();
                            so.DialogList[index].PhaseList[i] = (EventPhase_so)EditorGUILayout.ObjectField(string.Format("      Phase {0}", i), so.DialogList[index].PhaseList[i], typeof(EventPhase_so), true);
                            GUILayout.EndHorizontal();
                        }
                    }
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

        private void DialogArea(DialogInfo info, GUILayoutOption[] options)
        {
            GUILayout.BeginHorizontal();
            info.Text_name = EditorGUILayout.TextField("Name", info.Text_name);
            info.EnableNameBox = EditorGUILayout.Toggle(info.EnableNameBox);

            GUILayout.EndHorizontal();

            // 대화자 이름 텍스트 박스
            GUILayout.BeginHorizontal();
            info.NameColor = EditorGUILayout.ColorField("NameBox Color", info.NameColor);
            info.ColorPreset = (NameColorPreset)EditorGUILayout.EnumPopup(info.ColorPreset);
            info.NameColor = SetColor(info.ColorPreset, info.NameColor);
            info.NameBoxPos = (NameBoxPosPreset)EditorGUILayout.EnumPopup(info.NameBoxPos);
            //so.DialogList[index].NameBoxPos = EditorGUILayout.Popup(so.DialogList[index].NameBoxPos,
            GUILayout.EndHorizontal();

            // 대화 로그 텍스트
            EditorGUILayout.PrefixLabel("Log", EditorStyles.boldLabel);
            info.Text_value = EditorGUILayout.TextArea(info.Text_value, options);
        }
    } 
}
