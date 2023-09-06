using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StringPopUpTest_Window : EditorWindow
{
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

    [MenuItem("Utilities/Pop Up Test")]
    private static void Init()
    {
        StringPopUpTest_Window instance = (StringPopUpTest_Window)EditorWindow.GetWindow(typeof(StringPopUpTest_Window));
        instance.Show();
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Select an option:");

        PopUpCode();

        EditorGUILayout.LabelField(selectedStr);
    }

    private void PopUpCode()
    {
        sortIndex = EditorGUILayout.Popup("Sort", sortIndex, sort.ToArray());
        sortStr = sort[sortIndex];

        switch (sortStr)
        {
            case "None":
                RenderList(new List<string> { "None" }); break;
            case "Stage Manager":
                RenderList(StageManager.event_code); break;
            case "Spawn Manager":
                RenderList(SpawnManager.event_code); break;
            case "Unit Manager":
                RenderList(UnitManager.event_code); break;
            case "UI Manager":
                RenderList(UIManager.event_code); break;
            case "RMR":
                RenderList(ReadyMadeReality.RMR.event_code); break;
            default:
                RenderList(new List<string> { "None" }); break;
        }
    }

    private void RenderList(List<string> list)
    {
        if (listIndex >= list.Count) listIndex = 0;
        listIndex = EditorGUILayout.Popup("List", listIndex, list.ToArray());
        selectedStr = list[listIndex];
    }
}
