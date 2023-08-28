using Codice.Client.BaseCommands.BranchExplorer;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EventParam_Window : EditorWindow
{
    [SerializeField] private StageInfo_so stageInfo;
    [SerializeField] private List<EventParams> events;

    [SerializeField] private int index;

    [MenuItem("Utilities/Event Maker")]
    private static void Init()
    {
        EventParam_Window instance = (EventParam_Window)EditorWindow.GetWindow(typeof(EventParam_Window));
        instance.Show();
    }

    private void OnEnable()
    {
        minSize = new Vector2(500, 500); // 최소 크기 설정
        maxSize = new Vector2(500, 800); // 최대 크기 설정
    }

    private void OnGUI()
    {
        GUIStyle indexFieldStyle = new GUIStyle(EditorStyles.textField);
        GUILayoutOption[] smallFieldOption =
        {
            GUILayout.Width(50),
            GUILayout.Height(20)
        };
        GUILayoutOption[] indexFieldOption =
        {
            GUILayout.Width(260),
            GUILayout.Height(20)
        };
        GUILayoutOption[] labelFieldOption =
        {
            GUILayout.Width(460),
            GUILayout.Height(20)
        };
        GUILayoutOption[] enumFieldOption =
        {
            GUILayout.Width(410),
            GUILayout.Height(20)
        };
        GUILayoutOption[] innerFieldOption =
        {
            GUILayout.Width(360),
            GUILayout.Height(20)
        };
        GUILayoutOption[] innerVectorOption =
        {
            GUILayout.Width(340),
            GUILayout.Height(20)
        };
        GUILayoutOption[] buttonOption =
        {
            GUILayout.Width(20),
            GUILayout.Height(20)
        };
        GUILayoutOption[] saveButtonOptions =
        {
            GUILayout.Width(150),
            GUILayout.Height(30)
        };

        GUILayout.Label("Event Maker", new GUIStyle(GUI.skin.label) { fontSize = 20, alignment = TextAnchor.MiddleCenter });
        GUILayout.Space(20);


        // 에셋
        GUILayout.BeginHorizontal();
        GUILayout.Label("StageInfo Asset", new GUIStyle(GUI.skin.label) { fontStyle = FontStyle.Bold });
        GUILayout.FlexibleSpace();
        stageInfo = (StageInfo_so)EditorGUILayout.ObjectField(stageInfo, typeof(object), true);
        GUILayout.EndHorizontal();
             

        
        
        if (stageInfo != null)
        {
            GUILayout.BeginHorizontal();
            // 인덱스
            GUILayout.FlexibleSpace();
            index = EditorGUILayout.IntField("Event Index", Mathf.Clamp(index, 0, stageInfo.Para.Count - 1), indexFieldStyle, indexFieldOption);
            if (GUILayout.Button("<", buttonOption))
            {
                index = index > 0 ? index - 1 : index;
            }
            if (GUILayout.Button(">", buttonOption))
            {
                index = index < stageInfo.Para.Count - 1 ? index + 1 : index;
            }
            EditorGUILayout.LabelField("Max", smallFieldOption);
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.IntField(stageInfo.Para.Count, indexFieldStyle, smallFieldOption);
            EditorGUI.EndDisabledGroup();
            if (GUILayout.Button("+", buttonOption))
            {
                if (index == stageInfo.Para.Count - 1)
                    stageInfo.Para.Add(new EventParams(stageInfo.Para.Count - 1));
                else if (stageInfo.Para.Count - 1 > 0)
                    stageInfo.Para.Insert(index, new EventParams(stageInfo.Para.Count - 1));
                else
                    stageInfo.Para.Add(new EventParams(0));
            }
            if (GUILayout.Button("-", buttonOption))
            {
                if (stageInfo.Para.Count > 0)
                {
                    stageInfo.Para.RemoveAt(index);
                    index = index >= stageInfo.Para.Count ? stageInfo.Para.Count - 1 : index;
                    for (int i = 0; i < stageInfo.Para.Count; i++)
                        stageInfo.Para[i].no = i;
                }
                else
                    Debug.Log("Already Empty");
            }
            GUILayout.EndHorizontal();

            // Eventcode
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            stageInfo.Para[index].eventcode = (EventCode)EditorGUILayout.EnumPopup("Event Code", stageInfo.Para[index].eventcode, enumFieldOption);
            GUILayout.EndHorizontal();

            // Condition
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label("Condtions", labelFieldOption);
            GUILayout.EndHorizontal();            
                        
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            stageInfo.Para[index].condition.Sort = (ConditionSort)EditorGUILayout.EnumPopup("Condtition Sort", stageInfo.Para[index].condition.Sort, enumFieldOption);
            GUILayout.EndHorizontal();

            switch (stageInfo.Para[index].condition.Sort)
            {
                case ConditionSort.None:
                    break;
                case ConditionSort.Time:
                    Con_Num(innerFieldOption);
                    break;
                case ConditionSort.Trigger:
                    Con_Flag(innerFieldOption);
                    Con_FlagValue(innerFieldOption);
                    break;
                case ConditionSort.MoveToPos:
                    Con_Tag(innerFieldOption);
                    Con_Pos(innerFieldOption);
                    Con_Num(innerFieldOption);
                    break;
                default:
                    break;
            }

            GUILayout.BeginHorizontal();
            GUILayout.EndHorizontal();

            // EventParams
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label("Event Params", labelFieldOption);
            GUILayout.EndHorizontal();

            if (stageInfo.Para[index].extraParams != null)
            {
                Par_No(innerFieldOption);
                Par_Name(innerFieldOption);
                Par_Int(innerFieldOption);
                Par_Float(innerFieldOption);
                Par_Bool(innerFieldOption);
                Par_VecList(innerFieldOption, innerVectorOption); 
            }

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Save", GUILayout.Width(120), GUILayout.Height(30)))
            {
                EditorUtility.SetDirty(stageInfo);
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }




    }

    private void Con_Num(GUILayoutOption[] options)
    {
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        stageInfo.Para[index].condition.TargetNum = EditorGUILayout.FloatField("Target Num", stageInfo.Para[index].condition.TargetNum, options);
        GUILayout.EndHorizontal();
    }
    private void Con_Flag(GUILayoutOption[] options)
    {
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        stageInfo.Para[index].condition.TargetFlag = EditorGUILayout.TextField("Target Flag", stageInfo.Para[index].condition.TargetFlag, options);
        GUILayout.EndHorizontal();
    }
    private void Con_FlagValue(GUILayoutOption[] options)
    {
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        stageInfo.Para[index].condition.FlagValue = EditorGUILayout.Toggle("Flag Value", stageInfo.Para[index].condition.FlagValue, options);
        GUILayout.EndHorizontal();
    }
    private void Con_Tag(GUILayoutOption[] options) {
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        stageInfo.Para[index].condition.TargetTag = EditorGUILayout.TextField("Target Tag", stageInfo.Para[index].condition.TargetTag, options);
        GUILayout.EndHorizontal();
    }
    private void Con_Pos(GUILayoutOption[] options) {
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        EditorGUIUtility.wideMode = true;
        stageInfo.Para[index].condition.TargetPos = EditorGUILayout.Vector2Field("Target Position", stageInfo.Para[index].condition.TargetPos, options);
        GUILayout.EndHorizontal();
    }

    private void Par_No(GUILayoutOption[] options)
    {
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        stageInfo.Para[index].extraParams.Id = EditorGUILayout.IntField("Id", stageInfo.Para[index].extraParams.Id, options);
        GUILayout.EndHorizontal();
    }
    private void Par_Name(GUILayoutOption[] options)
    {
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        stageInfo.Para[index].extraParams.Name = EditorGUILayout.TextField("Name", stageInfo.Para[index].extraParams.Name, options);
        GUILayout.EndHorizontal();
    }
    private void Par_Int(GUILayoutOption[] options)
    {
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        stageInfo.Para[index].extraParams.Intvalue = EditorGUILayout.IntField("Int Value", stageInfo.Para[index].extraParams.Intvalue, options);
        GUILayout.EndHorizontal();
    }
    private void Par_Float(GUILayoutOption[] options)
    {
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        stageInfo.Para[index].extraParams.Floatvalue = EditorGUILayout.FloatField("Float Value", stageInfo.Para[index].extraParams.Floatvalue, options);
        GUILayout.EndHorizontal();
    }
    private void Par_Bool(GUILayoutOption[] options)
    {
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        stageInfo.Para[index].extraParams.Boolvalue = EditorGUILayout.Toggle("Bool Value", stageInfo.Para[index].extraParams.Boolvalue, options);
        GUILayout.EndHorizontal();
    }
    private void Par_VecList(GUILayoutOption[] options, GUILayoutOption[] options_inner)
    {
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        int past = stageInfo.Para[index].extraParams.VecList.Count;
        int len = EditorGUILayout.IntField("Vector List", Mathf.Clamp(past, 0, 10), options);
        if (len != past)
        {
            stageInfo.Para[index].extraParams.VecList.Clear();
            for (int i = 0; i < len; i++)
            {
                stageInfo.Para[index].extraParams.VecList.Add(new Vector2());
            }
        }
        GUILayout.EndHorizontal();
        if (len > 0)
        {
            for (int i = 0; i < len; i++)
                Par_VecElement(i, options_inner);
        }

        
    }
    private void Par_VecElement(int no, GUILayoutOption[] options)
    {
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        EditorGUIUtility.wideMode = true;
        stageInfo.Para[index].extraParams.VecList[no] = EditorGUILayout.Vector2Field(string.Format("vec {0}", no), stageInfo.Para[index].extraParams.VecList[no], options);
        GUILayout.EndHorizontal();
    }
}
