using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class CSV_to_SO : EditorWindow
{
    [SerializeField] private Object csv_file;
    [SerializeField] private Object SO_file_folder;

    [MenuItem("Utilities/Generate SO asset")]
    private static void Init()
    {
        CSV_to_SO instance = (CSV_to_SO)EditorWindow.GetWindow(typeof(CSV_to_SO));
        instance.Show();
    }

    [System.Obsolete]
    private void OnGUI()
    {
        csv_file = EditorGUILayout.ObjectField(csv_file, typeof(Object));
        SO_file_folder = EditorGUILayout.ObjectField(SO_file_folder, typeof(Object));
        if (csv_file)
            GUILayout.Label(Application.streamingAssetsPath + "/" + csv_file.name, EditorStyles.boldLabel);
        if (SO_file_folder)
            GUILayout.Label(Application.streamingAssetsPath + "/" + SO_file_folder.name, EditorStyles.boldLabel);
        
        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        
        if (GUILayout.Button("Create SO File", GUILayout.Width(120), GUILayout.Height(30)))
        {
            GenerateSOasset();
        }
        GUILayout.FlexibleSpace(); 
        EditorGUILayout.EndHorizontal();
    }

    private void GenerateSOasset()
    {
        string[] allLines = File.ReadAllLines(AssetDatabase.GetAssetPath(csv_file));
        
        for (int i = 0; i < allLines.Length; i++)
        {
            if (i == 0) continue;
            string[] split = allLines[i].Split(',');
            string assetName = split[0].ToString() + "_" + split[1] + ".asset";
            Stat_so instance = ScriptableObject.CreateInstance<Stat_so>();
            AssetDatabase.CreateAsset(instance, AssetDatabase.GetAssetPath(SO_file_folder) + "/" + assetName);
            Debug.Log(split[0] + ", " + split[1] + ", " + split[2]);
            instance.Id = int.Parse(split[0]);
            instance.Name_unit = split[1];
            instance.Hp = float.Parse(split[2]);
        }
        
    }
}
