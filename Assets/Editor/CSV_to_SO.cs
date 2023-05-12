using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class CSV_to_SO : EditorWindow
{
    [SerializeField] protected Object csv_file;
    [SerializeField] protected Object SO_file_folder;

    [SerializeField] protected string sortToCreate_string = "Default";
    [SerializeField] protected string log = "";

    private static void Init()
    {
        CSV_to_SO instance = (CSV_to_SO)EditorWindow.GetWindow(typeof(CSV_to_SO));
        instance.Show();
    }

    private void OnEnable()
    {
        minSize = new Vector2(500, 210); // 최소 크기 설정
        maxSize = new Vector2(500, 210); // 최대 크기 설정
    }

    [System.Obsolete]
    protected virtual void OnGUI()
    {
        SetSortText();
        GUILayout.Label(sortToCreate_string, new GUIStyle(GUI.skin.label) { fontSize = 20, alignment = TextAnchor.MiddleCenter });

        GUILayout.Space(20);

        GUILayout.BeginHorizontal();
        GUILayout.Label(sortToCreate_string +" CSV File to Convert", new GUIStyle(GUI.skin.label) { fontStyle = FontStyle.Bold });
        GUILayout.FlexibleSpace();
        csv_file = EditorGUILayout.ObjectField(csv_file, typeof(Object));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Folder to Create Assets", new GUIStyle(GUI.skin.label) { fontStyle = FontStyle.Bold });
        GUILayout.FlexibleSpace();
        SO_file_folder = EditorGUILayout.ObjectField(SO_file_folder, typeof(Object));
        if (SO_file_folder && !AssetDatabase.IsValidFolder(AssetDatabase.GetAssetPath(SO_file_folder)))
        {
            log = "Error : " + SO_file_folder + "is not a folder";
            SO_file_folder = null;
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("File Path : ");
        if (csv_file)
            GUILayout.Label(Application.streamingAssetsPath + "/" + csv_file.name, EditorStyles.boldLabel);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Folder Path : ");
        if (SO_file_folder)
            GUILayout.Label(Application.streamingAssetsPath + "/" + SO_file_folder.name, EditorStyles.boldLabel);
        GUILayout.EndHorizontal();

        GUILayout.Space(30);

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        
        if (GUILayout.Button("Convert SO File", GUILayout.Width(120), GUILayout.Height(30)))
        {
            GenerateSOasset();
        }
        GUILayout.FlexibleSpace(); 
        EditorGUILayout.EndHorizontal();

        GUILayout.Label(log);
    }

    protected virtual void SetSortText()
    {

    }

    protected virtual void GenerateSOasset()
    {
        if (csv_file == null)
        {
            log = "Error : None CSV file detected";
            return;
        }
        if (SO_file_folder == null)
        {
            log = "Error : None Folder to Create";
            return;
        }
        string[] allLines = File.ReadAllLines(AssetDatabase.GetAssetPath(csv_file));

        InputValues(allLines);

        log = "Convert Complete!";

    }

    protected virtual void InputValues(string[] allLines)
    {
        //for (int i = 0; i < allLines.Length; i++)
        //{
        //    if (i == 0) continue;
        //    string[] split = allLines[i].Split(',');
        //    string assetName = split[0].ToString() + "_" + split[1] + ".asset";
        //    Stat_so instance = ScriptableObject.CreateInstance<Stat_so>();
        //    AssetDatabase.CreateAsset(instance, AssetDatabase.GetAssetPath(SO_file_folder) + "/" + assetName);
        //    Debug.Log(split[0] + ", " + split[1] + ", " + split[2]);
        //    instance.Id = int.Parse(split[0]);
        //    instance.Name_unit = split[1];
        //    instance.Hp = float.Parse(split[2]);
        //}
    }

    protected virtual List<int> DuplicationInspection(string[] allLines)
    {
        return null;
    }
}
