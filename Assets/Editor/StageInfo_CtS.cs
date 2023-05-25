using GluonGui.WorkspaceWindow.Views.WorkspaceExplorer;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class StageInfo_CtS : CSV_to_SO
{
    [MenuItem("Utilities/Generate StageInfo SO asset")]
    private static void Init()
    {
        StageInfo_CtS instance = (StageInfo_CtS)EditorWindow.GetWindow(typeof(StageInfo_CtS));
        instance.Show();
    }

    protected override void GenerateSOasset()
    {
        base.GenerateSOasset();
    }

    protected override void SetSortText()
    {
        base.SetSortText();
        sortToCreate_string = "Stage Info";
    }

    protected override void InputValues(string[] allLines)
    {
        
        string[] stageName = allLines[0].Split(',');
        StageInfo_so stage_instance = ScriptableObject.CreateInstance<StageInfo_so>();
        AssetDatabase.CreateAsset(stage_instance, AssetDatabase.GetAssetPath(SO_file_folder) + "/" + stageName[1] + ".asset");
        List<EventInfo_so> events = new List<EventInfo_so>();

        for (int i = 2; i < allLines.Length; i++)
        {
            
            string[] split = allLines[i].Split(',');
            string assetName = "Event " + split[0].ToString() + ".asset";
            EventInfo_so event_instance = ScriptableObject.CreateInstance<EventInfo_so>();
            AssetDatabase.CreateAsset(event_instance, AssetDatabase.GetAssetPath(SO_file_folder) + "/" + assetName);
            event_instance.Id = int.Parse(split[0]);
            event_instance.Sort = int.Parse(split[1]) == 0 ? EventSort.None : EventSort.Spawn;
            event_instance.IsLoop = int.Parse(split[2]) == 0 ? false : true;
            event_instance.IsSequential = int.Parse(split[3]) == 0 ? false : true;
            event_instance.IsRequires = int.Parse(split[4]) == 0 ? false : true;
            event_instance.DurationToStart = float.Parse(split[5]);
            event_instance.Message = new EventMessage(int.Parse(split[6]), split[7], split[8], float.Parse(split[9]));

            events.Add(event_instance);
        }

        stage_instance.eventList_so.AddRange(events);
    }

    protected override List<int> DuplicationInspection(string[] allLines)
    {
        List<int> ids = new List<int>();
        Debug.Log("?");
        for (int i = 2; i < allLines.Length; i++)
        {
            string[] split= allLines[i].Split(' ');
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
