using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace ReadyMadeReality
{
    public class SO_to_CSV_DialogInfo : EditorWindow
    {
        [SerializeField] protected DialogInfo_so asset_file;
        [SerializeField] protected Object CSV_file_folder;

        [SerializeField] protected string sortToCreate_string = "DialogInfo SO to CSV";
        [SerializeField] protected string log = "";

        [MenuItem("Utilities/Generate DialogInfo CSV file")]
        private static void Init()
        {
            SO_to_CSV_DialogInfo instance = (SO_to_CSV_DialogInfo)EditorWindow.GetWindow(typeof(SO_to_CSV_DialogInfo));
            instance.Show();
        }

        private void OnEnable()
        {
            minSize = new Vector2(500, 210); // 최소 크기 설정
            maxSize = new Vector2(500, 210); // 최대 크기 설정
        }

        private void OnGUI()
        {
            GUILayout.Label(sortToCreate_string, new GUIStyle(GUI.skin.label) { fontSize = 20, alignment = TextAnchor.MiddleCenter });

            GUILayout.Space(20);

            GUILayout.BeginHorizontal();
            GUILayout.Label(sortToCreate_string + " SO File to Convert", new GUIStyle(GUI.skin.label) { fontStyle = FontStyle.Bold });
            GUILayout.FlexibleSpace();
            asset_file = (DialogInfo_so)EditorGUILayout.ObjectField(asset_file, typeof(DialogInfo_so), true);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Folder to Create Assets", new GUIStyle(GUI.skin.label) { fontStyle = FontStyle.Bold });
            GUILayout.FlexibleSpace();
            CSV_file_folder = EditorGUILayout.ObjectField(CSV_file_folder, typeof(Object), true);
            if (CSV_file_folder && !AssetDatabase.IsValidFolder(AssetDatabase.GetAssetPath(CSV_file_folder)))
            {
                log = "Error : " + CSV_file_folder + "is not a folder";
                CSV_file_folder = null;
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("File Path : ");
            if (asset_file)
                GUILayout.Label(Application.streamingAssetsPath + "/" + asset_file.name, EditorStyles.boldLabel);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Folder Path : ");
            if (CSV_file_folder)
                GUILayout.Label(Application.streamingAssetsPath + "/" + CSV_file_folder.name, EditorStyles.boldLabel);
            GUILayout.EndHorizontal();

            GUILayout.Space(30);

            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            if (GUILayout.Button("Convert CSV File", GUILayout.Width(120), GUILayout.Height(30)))
            {
                GenerateCSVFile();
            }
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            GUILayout.Label(log);
        }

        private void GenerateCSVFile()
        {
            if (asset_file == null)
            {
                log = "Error : None SO file detected";
                return;
            }
            if (CSV_file_folder == null)
            {
                log = "Error : None Folder to Create";
                return;
            }

            InputValues(asset_file);

            log = "Convert Complete!";

        }

        private void InputValues(DialogInfo_so so)
        {
            List<DialogInfo> list = so.DialogList;

            string assetName = Path.GetFileNameWithoutExtension(AssetDatabase.GetAssetPath(asset_file));
            string filePath = Path.Combine(AssetDatabase.GetAssetPath(CSV_file_folder), assetName + ".csv");

            if (filePath.Length != 0)
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine("index,name,log,l_port_id,r_port_id,namebox,boxcolor,bc_pre");

                    string line = "";
                    for (int i = 0; i < list.Count; i++)
                    {
                        line = "";
                        line += i + ",";
                        line += list[i].Text_name + ",";
                        line += list[i].Text_value + ",";
                        line += list[i].Left_portrait_id + ",";
                        line += list[i].Right_portrait_id + ",";
                        line += (string.Equals(list[i].EnableNameBox.ToString(), "True") ? "TRUE" : "FALSE") + ",";
                        line += ",";
                        line += list[i].ColorPreset;
                        writer.WriteLine(line);
                    }
                    Debug.Log(line);
                    writer.Close();
                }

            }
        }
    } 
}
