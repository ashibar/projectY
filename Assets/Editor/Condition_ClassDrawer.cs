using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(Condition))]
public class Condition_ClassDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        
        Rect enumRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        Rect fieldRect = new Rect(position.x + EditorGUI.indentLevel * 15, position.y + EditorGUIUtility.singleLineHeight, position.width, EditorGUIUtility.singleLineHeight);

        EditorGUI.PropertyField(enumRect, property.FindPropertyRelative("sort"));

        // enum 값에 따라 다른 내용을 표시
        ConditionSort enumValue = (ConditionSort)property.FindPropertyRelative("sort").enumValueIndex;

        EditorGUI.indentLevel++;

        switch (enumValue)
        {
            case ConditionSort.Trigger:
                EditorGUI.PropertyField(fieldRect, property.FindPropertyRelative("targetTrigger"));
                break;
            case ConditionSort.MoveToPos:
                EditorGUI.PropertyField(fieldRect, property.FindPropertyRelative("targetPos"));
                break;
            case ConditionSort.String:
                EditorGUI.PropertyField(fieldRect, property.FindPropertyRelative("targetString"));
                break;
            case ConditionSort.targetNum:
                EditorGUI.PropertyField(fieldRect, property.FindPropertyRelative("targetNum"));
                break;
                // 다른 enum 값에 대한 처리
        }

        EditorGUI.indentLevel--;

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight * 2.5f; // 필요한 높이로 조정
    }
}
