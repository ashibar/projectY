using ReadyMadeReality;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EventParam_Window : EditorWindow
{
    [SerializeField] private EventPhase_so phaseInfo;
    [SerializeField] private int index;

    [SerializeField] private List<string> sort = new List<string> {
        "None",
        "Stage Manager",
        "Spawn Manager",
        "Unit Manager",
        "UI Manager",
        "RMR",
    };
    [SerializeField] int sortIndex = 0;
    [SerializeField] int listIndex = 0;
    [SerializeField] string sortStr;
    [SerializeField] private string selectedStr = "";

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
        phaseInfo = (EventPhase_so)EditorGUILayout.ObjectField(phaseInfo, typeof(object), true);
        GUILayout.EndHorizontal();

        
        if (phaseInfo != null)
        {
            if (phaseInfo.Events.Count <= 0)
                phaseInfo.Events.Add(new EventParams(0));
                        
            GUILayout.BeginHorizontal();
            // 인덱스
            GUILayout.FlexibleSpace();
            index = EditorGUILayout.IntField("Event Index", Mathf.Clamp(index, 0, phaseInfo.Events.Count - 1), indexFieldStyle, indexFieldOption);
            if (GUILayout.Button("<", buttonOption))
            {
                GUI.FocusControl(null);
                index = index > 0 ? index - 1 : index;
            }
            if (GUILayout.Button(">", buttonOption))
            {
                GUI.FocusControl(null);
                index = index < phaseInfo.Events.Count - 1 ? index + 1 : index;
            }
            EditorGUILayout.LabelField("Max", smallFieldOption);
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.IntField(phaseInfo.Events.Count, indexFieldStyle, smallFieldOption);
            EditorGUI.EndDisabledGroup();
            if (GUILayout.Button("+", buttonOption))
            {
                GUI.FocusControl(null);
                if (index == phaseInfo.Events.Count - 1)
                    phaseInfo.Events.Add(new EventParams(phaseInfo.Events.Count - 1));
                else if (phaseInfo.Events.Count - 1 > 0)
                    phaseInfo.Events.Insert(index + 1, new EventParams(phaseInfo.Events.Count - 1));
                else
                    phaseInfo.Events.Add(new EventParams(0));
            }
            if (GUILayout.Button("-", buttonOption))
            {
                GUI.FocusControl(null);
                if (phaseInfo.Events.Count > 0)
                {
                    phaseInfo.Events.RemoveAt(index);
                    index = index >= phaseInfo.Events.Count ? phaseInfo.Events.Count - 1 : index;
                    for (int i = 0; i < phaseInfo.Events.Count; i++)
                        phaseInfo.Events[i].no = i;
                }
                else
                    Debug.Log("Already Empty");
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            PopUpCode(enumFieldOption);

            // Eventcode
            //GUILayout.BeginHorizontal();
            //GUILayout.FlexibleSpace();
            //phaseInfo.Events[index].eventcode = (EventCode)EditorGUILayout.EnumPopup("Event Code", phaseInfo.Events[index].eventcode, enumFieldOption);
            //GUILayout.EndHorizontal();

            // Condition
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label("Condtions", labelFieldOption);
            GUILayout.EndHorizontal();            
                        
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            phaseInfo.Events[index].condition.Sort = (ConditionSort)EditorGUILayout.EnumPopup("Condtition Sort", phaseInfo.Events[index].condition.Sort, enumFieldOption);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            phaseInfo.Events[index].condition.IsSatisfied = EditorGUILayout.Toggle("isSatisfied", phaseInfo.Events[index].condition.IsSatisfied, innerFieldOption);
            GUILayout.EndHorizontal();

            switch (phaseInfo.Events[index].condition.Sort)
            {
                case ConditionSort.None:
                    break;
                case ConditionSort.Time:
                    Con_Num(innerFieldOption);
                    Con_Tinue(innerFieldOption);
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

            if (phaseInfo.Events[index].extraParams != null)
            {
                Par_No(innerFieldOption);
                Par_Name(innerFieldOption);
                Par_Int(innerFieldOption);
                Par_Float(innerFieldOption);
                Par_Bool(innerFieldOption);
                Par_Phase(innerFieldOption);
                Par_Dialog(innerFieldOption);
                Par_VecList(innerFieldOption, innerVectorOption); 
            }

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Save", GUILayout.Width(120), GUILayout.Height(30)))
            {
                GUI.FocusControl(null);
                EditorUtility.SetDirty(phaseInfo);
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }




    }

    private void PopUpCode(GUILayoutOption[] options)
    {
        sortIndex = EditorGUILayout.Popup("Sort", phaseInfo.Events[index].eventindex, sort.ToArray(), options);
        GUILayout.EndHorizontal();
        sortStr = sort[sortIndex];

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        switch (sortStr)
        {
            case "None":
                RenderList(0, new List<string> { "None" }, options); break;
            case "Stage Manager":
                RenderList(1, StageManager.event_code, options); break;
            case "Spawn Manager":
                RenderList(2, SpawnManager.event_code, options); break;
            case "Unit Manager":
                RenderList(3, UnitManager.event_code, options); break;
            case "UI Manager":
                RenderList(4, UIManager.event_code, options); break;
            case "RMR":
                RenderList(5, ReadyMadeReality.RMR.event_code, options); break;
            default:
                RenderList(0, new List<string> { "None" }, options); break;
        }
        GUILayout.EndHorizontal();
    }

    private void RenderList(int no, List<string> list, GUILayoutOption[] options)
    {
        phaseInfo.Events[index].eventindex = no;
        if (listIndex >= list.Count) listIndex = 0;
        listIndex = EditorGUILayout.Popup("List", SearchIndex(phaseInfo.Events[index].eventcode, list), list.ToArray(), options);
        phaseInfo.Events[index].eventcode = list[listIndex];
    }

    private int SearchIndex(string str, List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
            if (string.Equals(str, list[i]))
                return i;
        return 0;
    }

    private void Con_Num(GUILayoutOption[] options)
    {
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        phaseInfo.Events[index].condition.TargetNum = EditorGUILayout.FloatField("Target Num", phaseInfo.Events[index].condition.TargetNum, options);
        GUILayout.EndHorizontal();
    }
    private void Con_Tinue(GUILayoutOption[] options)
    {
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        phaseInfo.Events[index].condition.IsContinued = EditorGUILayout.Toggle("isContinued", phaseInfo.Events[index].condition.IsContinued, options);
        GUILayout.EndHorizontal();
    }
    private void Con_Flag(GUILayoutOption[] options)
    {
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        phaseInfo.Events[index].condition.TargetFlag = EditorGUILayout.TextField("Target Flag", phaseInfo.Events[index].condition.TargetFlag, options);
        GUILayout.EndHorizontal();
    }
    private void Con_FlagValue(GUILayoutOption[] options)
    {
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        phaseInfo.Events[index].condition.FlagValue = EditorGUILayout.Toggle("Flag Value", phaseInfo.Events[index].condition.FlagValue, options);
        GUILayout.EndHorizontal();
    }
    private void Con_Tag(GUILayoutOption[] options) {
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        phaseInfo.Events[index].condition.TargetTag = EditorGUILayout.TextField("Target Tag", phaseInfo.Events[index].condition.TargetTag, options);
        GUILayout.EndHorizontal();
    }
    private void Con_Pos(GUILayoutOption[] options) {
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        EditorGUIUtility.wideMode = true;
        phaseInfo.Events[index].condition.TargetPos = EditorGUILayout.Vector2Field("Target Position", phaseInfo.Events[index].condition.TargetPos, options);
        GUILayout.EndHorizontal();
    }

    private void Par_No(GUILayoutOption[] options)
    {
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        phaseInfo.Events[index].extraParams.Id = EditorGUILayout.IntField("Id", phaseInfo.Events[index].extraParams.Id, options);
        GUILayout.EndHorizontal();
    }
    private void Par_Name(GUILayoutOption[] options)
    {
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        phaseInfo.Events[index].extraParams.Name = EditorGUILayout.TextField("Name", phaseInfo.Events[index].extraParams.Name, options);
        GUILayout.EndHorizontal();
    }
    private void Par_Int(GUILayoutOption[] options)
    {
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        phaseInfo.Events[index].extraParams.Intvalue = EditorGUILayout.IntField("Int Value", phaseInfo.Events[index].extraParams.Intvalue, options);
        GUILayout.EndHorizontal();
    }
    private void Par_Float(GUILayoutOption[] options)
    {
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        phaseInfo.Events[index].extraParams.Floatvalue = EditorGUILayout.FloatField("Float Value", phaseInfo.Events[index].extraParams.Floatvalue, options);
        GUILayout.EndHorizontal();
    }
    private void Par_Bool(GUILayoutOption[] options)
    {
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        phaseInfo.Events[index].extraParams.Boolvalue = EditorGUILayout.Toggle("Bool Value", phaseInfo.Events[index].extraParams.Boolvalue, options);
        GUILayout.EndHorizontal();
    }
    private void Par_Phase(GUILayoutOption[] options)
    {
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        phaseInfo.Events[index].extraParams.NextPhase = (EventPhase_so)EditorGUILayout.ObjectField("Next Phase" ,phaseInfo.Events[index].extraParams.NextPhase, typeof(object), true, options);
        GUILayout.EndHorizontal();
    }
    private void Par_Dialog(GUILayoutOption[] options)
    {
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        phaseInfo.Events[index].extraParams.Dialog_so = (DialogInfo_so)EditorGUILayout.ObjectField("Dialog so", phaseInfo.Events[index].extraParams.Dialog_so, typeof(object), true, options);
        GUILayout.EndHorizontal();
    }
    private void Par_VecList(GUILayoutOption[] options, GUILayoutOption[] options_inner)
    {
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        int past = phaseInfo.Events[index].extraParams.VecList.Count;
        int len = EditorGUILayout.IntField("Vector List", Mathf.Clamp(past, 0, 10), options);
        if (len != past)
        {
            phaseInfo.Events[index].extraParams.VecList.Clear();
            for (int i = 0; i < len; i++)
            {
                phaseInfo.Events[index].extraParams.VecList.Add(new Vector2());
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
        phaseInfo.Events[index].extraParams.VecList[no] = EditorGUILayout.Vector2Field(string.Format("vec {0}", no), phaseInfo.Events[index].extraParams.VecList[no], options);
        GUILayout.EndHorizontal();
    }
}
