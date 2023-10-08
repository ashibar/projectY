using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(StringNTrigger))]
public class StringNTrigger_ClassDrawer : PropertyDrawer {
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        SerializedProperty string_value = property.FindPropertyRelative("triggerName");
        SerializedProperty bool_value = property.FindPropertyRelative("triggerValue");

        Rect labelRect = new Rect(position.x, position.y, position.width * 0.1f, position.height);
        Rect halfRect = new Rect(position.x + position.width * 0.1f, position.y, position.width * 0.4f, position.height);

        EditorGUI.LabelField(labelRect, "Name");
        EditorGUI.PropertyField(halfRect, string_value, GUIContent.none);
        labelRect.x += position.width * 0.5f + 10f;
        halfRect.x += position.width * 0.5f + 10f;
        EditorGUI.LabelField(labelRect, "Value");
        EditorGUI.PropertyField(halfRect, bool_value, GUIContent.none);
        
        EditorGUI.EndProperty();
    }
}
