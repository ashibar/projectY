using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(Condition))]
public class Condition_ClassDrawer : PropertyDrawer
{
    private int itemNum = 0;
    
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        
        Rect enumRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        Rect fieldRect = new Rect(position.x + EditorGUI.indentLevel * 15, position.y + EditorGUIUtility.singleLineHeight, position.width, EditorGUIUtility.singleLineHeight);

        EditorGUI.PropertyField(enumRect, property.FindPropertyRelative("sort"));
        // enum 값에 따라 다른 내용을 표시
        ConditionSort enumValue = (ConditionSort)property.FindPropertyRelative("sort").enumValueIndex;        

        

        //SerializedProperty targetNum = property.FindPropertyRelative("targetNum");
        //SerializedProperty targetFlag = property.FindPropertyRelative("targetFlag");
        //SerializedProperty flagValue = property.FindPropertyRelative("flagValue");
        //SerializedProperty targetPos = property.FindPropertyRelative("targetPos");
        //SerializedProperty targetAreaID = property.FindPropertyRelative("targetAreaID");

        //int intValue = targetNum.intValue;
        //string stringValue = targetFlag.stringValue;
        //bool boolValue = flagValue.boolValue;
        //Vector2 vecValue = targetPos.vector2Value;

        //EditorGUI.indentLevel++;


        switch (enumValue)
        {
            case ConditionSort.None:
                itemNum = 0;
                break;
            case ConditionSort.Time:
                itemNum = 1;
                EditorGUI.PropertyField(FieldRect(position, 1), property.FindPropertyRelative("targetNum"));
                break;
            case ConditionSort.Trigger:
                itemNum = 2;
                EditorGUI.PropertyField(FieldRect(position, 1), property.FindPropertyRelative("targetFlag"));
                EditorGUI.PropertyField(FieldRect(position, 2), property.FindPropertyRelative("flagValue"));
                break;
            case ConditionSort.MoveToPos:
                itemNum = 3;
                EditorGUI.PropertyField(FieldRect(position, 1), property.FindPropertyRelative("targetTag"));
                EditorGUI.PropertyField(FieldRect(position, 2), property.FindPropertyRelative("targetPos"));
                EditorGUI.PropertyField(FieldRect(position, 3), property.FindPropertyRelative("targetNum"));
                break;
            case ConditionSort.Number:
                itemNum = 4;
                EditorGUI.PropertyField(FieldRect(position, 1), property.FindPropertyRelative("targetAreaID"));
                break;
            
                // 다른 enum 값에 대한 처리
        }
        property.serializedObject.ApplyModifiedProperties();
        //EditorGUI.indentLevel--;
        //SerializedProperty element = property.GetArrayElementAtIndex(0);
        //EditorGUI.PropertyField(new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight, position.width, EditorGUIUtility.singleLineHeight), property, false);
        EditorGUI.EndProperty();
        
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight * (3 + itemNum); // 필요한 높이로 조정
    }

    private Rect FieldRect(Rect rect, int no)
    {
        return new Rect(rect.x + EditorGUI.indentLevel * 15, rect.y + EditorGUIUtility.singleLineHeight * no, rect.width, EditorGUIUtility.singleLineHeight);
    }
}
