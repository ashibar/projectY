using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UnitStat_CtS : CSV_to_SO
{
    [MenuItem("Utilities/Generate UnitStat SO asset")]
    private static void Init()
    {
        UnitStat_CtS instance = (UnitStat_CtS)EditorWindow.GetWindow(typeof(UnitStat_CtS));
        instance.Show();
    }

    protected override void GenerateSOasset()
    {
        base.GenerateSOasset();
    }

    protected override void SetSortText()
    {
        base.SetSortText();
        sortToCreate_string = "Unit Stat";
    }

    protected override void InputValues(string[] allLines)
    {
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
