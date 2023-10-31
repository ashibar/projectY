using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(SpawnInfo))]
public class SpawnInfo_Drawer : PropertyDrawer
{
    private int baseRecty = 3;
    
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        SerializedProperty unit_prefab = property.FindPropertyRelative("unit_prefab");
        SerializedProperty spawn_sort = property.FindPropertyRelative("spawn_sort");
        SerializedProperty recty = property.FindPropertyRelative("recty");
        SerializedProperty point = property.FindPropertyRelative("point");
        SerializedProperty list = property.FindPropertyRelative("position");
        SerializedProperty radius = property.FindPropertyRelative("radius");
        SerializedProperty angle1 = property.FindPropertyRelative("angle1");
        SerializedProperty angle2 = property.FindPropertyRelative("angle2");
        SerializedProperty amount = property.FindPropertyRelative("amount");
        SerializedProperty gap = property.FindPropertyRelative("gap");




        Rect rect1 = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        EditorGUI.PropertyField(rect1, unit_prefab, new GUIContent("Unit Prefab"));
        
        Rect rect2 = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing, position.width, EditorGUIUtility.singleLineHeight);
        int sort_index = EditorGUI.Popup(rect2, "Spawn Sort", SearchIndex(spawn_sort.stringValue, SpawnInfo.spawn_sort_preset), SpawnInfo.spawn_sort_preset.ToArray());
        spawn_sort.stringValue = SpawnInfo.spawn_sort_preset[sort_index];

        switch (spawn_sort.stringValue)
        {
            case "Point":
                recty.intValue = baseRecty + 1;
                Rect rect3 = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight * 2 + EditorGUIUtility.standardVerticalSpacing, position.width, EditorGUIUtility.singleLineHeight);
                EditorGUI.PropertyField(rect3, point, new GUIContent("Position"));
                break;
            case "Border":
                recty.intValue = baseRecty + 3;
                Rect rect4 = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight * 2 + EditorGUIUtility.standardVerticalSpacing, position.width, EditorGUIUtility.singleLineHeight);
                EditorGUI.PropertyField(rect4, radius, new GUIContent("Radius"));
                rect4.y += EditorGUIUtility.singleLineHeight;
                EditorGUI.PropertyField(rect4, angle1, new GUIContent("Angle Min"));
                rect4.y += EditorGUIUtility.singleLineHeight;
                EditorGUI.PropertyField(rect4, angle2, new GUIContent("Angle Max"));
                break;
            case "List":
                Rect rect5 = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight * 2 + EditorGUIUtility.standardVerticalSpacing, position.width, EditorGUIUtility.singleLineHeight);
                EditorGUI.PropertyField(rect5, list, new GUIContent("Position"));
                recty.intValue = baseRecty + 3 + list.arraySize;
                break;
            case "Circle":
                recty.intValue = baseRecty + 2;
                Rect rect6 = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight * 2 + EditorGUIUtility.standardVerticalSpacing, position.width, EditorGUIUtility.singleLineHeight);
                EditorGUI.PropertyField(rect6, radius, new GUIContent("Radius"));
                rect6.y += EditorGUIUtility.singleLineHeight;
                EditorGUI.PropertyField(rect6, amount, new GUIContent("Amount"));
                break;
            case "Lines":
                recty.intValue = baseRecty + 4;
                Rect rect7 = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight * 2 + EditorGUIUtility.standardVerticalSpacing, position.width, EditorGUIUtility.singleLineHeight);
                EditorGUI.PropertyField(rect7, angle1, new GUIContent("Line Angle"));
                rect7.y += EditorGUIUtility.singleLineHeight;
                EditorGUI.PropertyField(rect7, amount, new GUIContent("Amount"));
                rect7.y += EditorGUIUtility.singleLineHeight;
                EditorGUI.PropertyField(rect7, point, new GUIContent("Position"));
                rect7.y += EditorGUIUtility.singleLineHeight;
                EditorGUI.PropertyField(rect7, gap, new GUIContent("gap"));
                break;
            default:
                recty.intValue = baseRecty;
                break;
        }        

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        SerializedProperty recty = property.FindPropertyRelative("recty");
        
        return EditorGUIUtility.singleLineHeight * recty.intValue + EditorGUIUtility.standardVerticalSpacing;
    }

    private int SearchIndex(string str, List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
            if (string.Equals(str, list[i]))
                return i;
        return 0;
    }
}
