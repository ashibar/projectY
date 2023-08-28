using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace ReadyMadeReality
{
    public class CSV_to_SO_DialogInfo : EditorWindow
    {
        [SerializeField] protected Object csv_file;
        [SerializeField] protected Object SO_file_folder;

        [SerializeField] protected string sortToCreate_string = "DialogInfo CSV to SO";
        [SerializeField] protected string log = "";

        [MenuItem("Utilities/Generate DialogInfo SO asset")]
        private static void Init()
        {
            CSV_to_SO_DialogInfo instance = (CSV_to_SO_DialogInfo)EditorWindow.GetWindow(typeof(CSV_to_SO_DialogInfo));
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
            GUILayout.Label(sortToCreate_string + " CSV File to Convert", new GUIStyle(GUI.skin.label) { fontStyle = FontStyle.Bold });
            GUILayout.FlexibleSpace();
            csv_file = EditorGUILayout.ObjectField(csv_file, typeof(Object), true);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Folder to Create Assets", new GUIStyle(GUI.skin.label) { fontStyle = FontStyle.Bold });
            GUILayout.FlexibleSpace();
            SO_file_folder = EditorGUILayout.ObjectField(SO_file_folder, typeof(Object), true);
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

        private void GenerateSOasset()
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
            string[] allLines = File.ReadAllLines(AssetDatabase.GetAssetPath(csv_file), Encoding.GetEncoding(51949));

            InputValues(allLines);

            log = "Convert Complete!";

        }

        private void InputValues(string[] allLines)
        {

            string assetName = Path.GetFileNameWithoutExtension(AssetDatabase.GetAssetPath(csv_file));
            DialogInfo_so instance = ScriptableObject.CreateInstance<DialogInfo_so>();
            AssetDatabase.CreateAsset(instance, AssetDatabase.GetAssetPath(SO_file_folder) + "/" + assetName + ".asset");
            List<DialogInfo> tmpList = new List<DialogInfo>();

            for (int i = 1; i < allLines.Length; i++)
            {
                string[] split = allLines[i].Split(',');
                DialogInfo tmpInfo = new DialogInfo();
                tmpInfo.Text_name = split[1];
                tmpInfo.Text_value = split[2];
                tmpInfo.Left_portrait_id = int.Parse(split[3]);
                tmpInfo.Right_portrait_id = int.Parse(split[4]);
                tmpInfo.EnableNameBox = bool.Parse(split[5]);
                tmpInfo.ColorPreset = StringToPresetEnum(split[6], split[7]);

                tmpList.Add(tmpInfo);
            }

            instance.DialogList.AddRange(tmpList);
            EditorUtility.SetDirty(instance);
        }

        private NameColorPreset StringToPresetEnum(string custom, string preset)
        {
            NameColorPreset tmp = NameColorPreset.Custom;
            switch (preset)
            {
                case "Custom":
                    //후에 추가
                    break;
                case "Red":
                    tmp = NameColorPreset.Red; break;
                case "Orange":
                    tmp = NameColorPreset.Orange; break;
                case "Green":
                    tmp = NameColorPreset.Green; break;
                case "Blue":
                    tmp = NameColorPreset.Blue; break;
                default:
                    break;
            }

            return tmp;
        }

        private List<int> DuplicationInspection(string[] allLines)
        {
            List<int> ids = new List<int>();
            Debug.Log("?");
            for (int i = 2; i < allLines.Length; i++)
            {
                string[] split = allLines[i].Split(' ');
                ids.Add((int)char.GetNumericValue((split[0][0])));
            }

            List<string> assetName = LoadAssets(AssetDatabase.GetAssetPath(SO_file_folder));

            foreach (string path in assetName)
            {
                if (path.ToCharArray()[0] != 'E') continue;

                for (int i = 0; i < ids.Count; i++)
                {
                    if (ids[i] == (int)char.GetNumericValue(path[6]))
                    {
                        ids.RemoveAt(i);
                    }
                }
            }



            return ids;
        }

        private List<string> LoadAssets(string folderPath)
        {
            List<string> assetPaths = new List<string>();

            string[] files = Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories);

            foreach (string file in files)
            {
                if (!file.EndsWith(".meta"))
                {
                    string dataPath = Application.dataPath;
                    string relativePath = "Assets" + file.Replace(dataPath, "").Replace("\\", "/");
                    assetPaths.Add(relativePath);
                }
            }
            List<string> assetName = new List<string>();

            foreach (string path in assetPaths)
                assetName.Add(Path.GetFileName(path));

            return assetName;
        }
    } 
}
