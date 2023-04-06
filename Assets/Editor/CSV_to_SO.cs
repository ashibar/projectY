using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEditor.PackageManager.UI;

public class CSV_to_SO : EditorWindow
{
    [SerializeField] private Object csv_file_folder;
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
        csv_file_folder = EditorGUILayout.ObjectField(csv_file_folder, typeof(Object));
        SO_file_folder = EditorGUILayout.ObjectField(SO_file_folder, typeof(Object));
        if (csv_file_folder)
            GUILayout.Label(Application.streamingAssetsPath + "/" + csv_file_folder.name, EditorStyles.boldLabel);
        if (SO_file_folder)
            GUILayout.Label(Application.streamingAssetsPath + "/" + SO_file_folder.name, EditorStyles.boldLabel);
    }

    private void OnValidate()
    {
        
    }

    public static void GenerateSOasset()
    {
        string[] allLines = File.ReadAllLines(Application.dataPath);
    }
}
