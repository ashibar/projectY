using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ScriptableObjectModifier : MonoBehaviour
{
    [SerializeField] private List<EventPhase_so> phases = new List<EventPhase_so>();

    public void Press_Load()
    {
        List<EventPhase_so> scriptableObjects = new List<EventPhase_so>();

        //// ���� �����ͺ��̽����� ��� ScriptableObject ã��
        string[] guids = AssetDatabase.FindAssets("t:EventPhase_so");
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            EventPhase_so scriptableObject = AssetDatabase.LoadAssetAtPath<EventPhase_so>(path);

            // ScriptableObject�̸� ����Ʈ�� �߰�
            if (scriptableObject != null)
            {
                scriptableObjects.Add(scriptableObject);
            }
        }

        phases.Clear();
        phases.AddRange(scriptableObjects);
    }

    public void Press_Modify()
    {
        foreach (EventPhase_so phase in phases)
        {
            foreach (EventParams e in phase.Events)
            {
                if (e.conditions == null)
                    e.conditions = new List<Condition>();
                else
                    e.conditions.Clear();

                if (e.condition != null)
                    e.conditions.Add(e.condition);

            }
            EditorUtility.SetDirty(phase);
        }
    }
}
