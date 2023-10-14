using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(StringNString))]
public class StringNString_ClassDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        SerializedProperty string1 = property.FindPropertyRelative("string1");
        SerializedProperty string2 = property.FindPropertyRelative("string2");

        Rect labelRect = new Rect(position.x, position.y, position.width * 0.1f, position.height);
        Rect halfRect = new Rect(position.x + position.width * 0.1f, position.y, position.width * 0.4f, position.height);

        EditorGUI.LabelField(labelRect, "string1");
        EditorGUI.PropertyField(halfRect, string1, GUIContent.none);
        labelRect.x += position.width * 0.5f + 10f;
        halfRect.x += position.width * 0.5f + 10f;
        EditorGUI.LabelField(labelRect, "string2");
        EditorGUI.PropertyField(halfRect, string2, GUIContent.none);

        EditorGUI.EndProperty();
    }
}
