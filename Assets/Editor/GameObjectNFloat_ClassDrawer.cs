using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(GameObjectNFloat))]
public class GameObjectNFloat_ClassDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        SerializedProperty obj = property.FindPropertyRelative("obj");
        SerializedProperty value = property.FindPropertyRelative("value");

        Rect labelRect = new Rect(position.x, position.y, position.width * 0.1f, position.height);
        Rect halfRect = new Rect(position.x + position.width * 0.1f, position.y, position.width * 0.4f, position.height);

        EditorGUI.LabelField(labelRect, "Object");
        EditorGUI.PropertyField(halfRect, obj, GUIContent.none);
        labelRect.x += position.width * 0.5f + 10f;
        halfRect.x += position.width * 0.5f + 10f;
        EditorGUI.LabelField(labelRect, "Value");
        EditorGUI.PropertyField(halfRect, value, GUIContent.none);

        EditorGUI.EndProperty();
    }
}
