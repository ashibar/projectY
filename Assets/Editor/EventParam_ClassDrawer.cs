using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(EventParams))]
public class EventParam_ClassDrawer : PropertyDrawer
{
    

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        EditorGUI.PropertyField(position, property, label, true);

        //int no = property.FindPropertyRelative("no").intValue;

        //List<EventParams> list = fieldInfo.GetValue(property.serializedObject.targetObject) as List<EventParams>;

        //Rect objRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight, position.width, EditorGUIUtility.singleLineHeight);

        //if (list != null)
        //{
        //    if (list[no].paras != null)
        //    {
        //        for (int i = 0; i < list[no].paras.Length; i++)
        //        {
        //            object p = list[no].paras[i];
        //            if (p is int)
        //                EditorGUI.LabelField(FieldRect(position, i + 1), string.Format("Int     {0}" , (int)p));
        //            else if (p is float)
        //                EditorGUI.LabelField(FieldRect(position, i + 1), string.Format("float   {0}", (float)p));
        //            else if (p is string)
        //                EditorGUI.LabelField(FieldRect(position, i + 1), string.Format("string  {0}", (string)p));
        //            else if (p is List<Vector2>)
        //            {
        //                List<Vector2> vecs = (List<Vector2>)p;
        //                for (int j = 0; j < vecs.Count; j++)
        //                    EditorGUI.LabelField(FieldRect(position, i + j + 1), string.Format("vector2 ({0}, {1})", vecs[j].x, vecs[j].y));
        //            }                        
        //            else
        //                EditorGUI.LabelField(FieldRect(position, i + 1), "Unknown Type");
        //        } 
        //    }
        //    else
        //    {
        //        EditorGUI.LabelField(FieldRect(position, 1), "para is null");
        //        EditorGUI.LabelField(FieldRect(position, 2), list[no].no.ToString());
        //    }
        //}
        //else
        //{
        //    EditorGUI.LabelField(FieldRect(position, 1), "list is null");
        //}

        //if (target != null)
        //{
        //    para = target.paras;
        //    Debug.Log(para[0]);
        //    // object[]의 내용을 편집
        //    //for (int i = 0; i < para.Length; i++)
        //    //{
        //    //    para[i] = EditorGUILayout.ObjectField("Element " + i, (UnityEngine.Object)para[i], typeof(UnityEngine.Object), true);
        //    //}
        //}
        //else
        //{
        //    EditorGUI.LabelField(position, "target is null");
        //}
        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        // 기존 UI 요소의 높이와 추가 UI 요소의 높이 합산한 값 반환
        return EditorGUI.GetPropertyHeight(property, label, true) + EditorGUIUtility.standardVerticalSpacing + EditorGUIUtility.singleLineHeight;
    }

    private Rect FieldRect(Rect rect, int no)
    {
        return new Rect(rect.x + 30, rect.y + EditorGUIUtility.singleLineHeight * (no + 4.5f), rect.width, EditorGUIUtility.singleLineHeight);
    }
}
