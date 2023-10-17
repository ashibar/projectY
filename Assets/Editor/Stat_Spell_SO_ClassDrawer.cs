using ReadyMadeReality;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;

[CustomEditor(typeof(Stat_Spell_so))]
public class Stat_Spell_SO_ClassDrawer : Editor
{
    public override void OnInspectorGUI()
    {
        Stat_Spell_so so = (Stat_Spell_so)target;
        GUILayoutOption[] textFieldOptions =
            {
            GUILayout.ExpandWidth(true),
            GUILayout.Height(20)
        };

        so.Spell_Name = EditorGUILayout.TextField("Spell Name", so.Spell_Name, textFieldOptions);
        so.Spell_Code = EditorGUILayout.TextField("Spell Name", so.Spell_Code, textFieldOptions);
        so.Spell_Type = (SpellType)EditorGUILayout.EnumPopup("Spell Type", so.Spell_Type, textFieldOptions);

        switch (so.Spell_Type)
        {
            case SpellType.Core:
                so.Spell_DMG = EditorGUILayout.FloatField("Damage", so.Spell_DMG, textFieldOptions);
                so.Spell_Speed = EditorGUILayout.FloatField("Projectile Speed", so.Spell_Speed, textFieldOptions);
                so.Spell_CoolTime = EditorGUILayout.FloatField("Instantiate CoolTime", so.Spell_CoolTime, textFieldOptions);
                so.Spell_Duration = EditorGUILayout.FloatField("Projectile Duration", so.Spell_Duration, textFieldOptions);
                so.Spell_ProjectileDelay = EditorGUILayout.FloatField("Projectile Delay", so.Spell_ProjectileDelay, textFieldOptions);
                so.Spell_Range_Duration = EditorGUILayout.FloatField("Range Duration", so.Spell_Range_Duration, textFieldOptions);
                so.Spell_Range_TicDMG = EditorGUILayout.FloatField("Range Tic Damage", so.Spell_Range_TicDMG, textFieldOptions);
                so.Spell_Multy_EA = EditorGUILayout.FloatField("Projectile Multiply", so.Spell_Multy_EA, textFieldOptions);
                so.Spell_Multy_Radius = EditorGUILayout.FloatField("Projectile Multiply Radius", so.Spell_Multy_Radius, textFieldOptions);
                so.Spell_Range_Area = EditorGUILayout.FloatField("Projectile Range Area", so.Spell_Range_Area, textFieldOptions);
                so.Spell_Amount_Tic = EditorGUILayout.FloatField("Amount of Unit to interact", so.Spell_Amount_Tic, textFieldOptions);
                break;
            case SpellType.Part:
                break;
            case SpellType.Element:
                break;
            case SpellType.Passive:
                break;
        }

        EditorUtility.SetDirty(target);
    }
    
    
    //public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    //{
    //    EditorGUI.BeginProperty(position, label, property);

    //    SerializedProperty spell_Type = property.FindPropertyRelative("spell_Type");
    //    SerializedProperty spell_Name = property.FindPropertyRelative("spell_Name");
    //    SerializedProperty spell_Code = property.FindPropertyRelative("spell_Code");
    //    SerializedProperty spell_DMG = property.FindPropertyRelative("spell_DMG");
    //    SerializedProperty spell_Speed = property.FindPropertyRelative("spell_Speed");
    //    SerializedProperty spell_CoolTime = property.FindPropertyRelative("spell_CoolTime");
    //    SerializedProperty spell_Duration = property.FindPropertyRelative("spell_Duration");
    //    SerializedProperty spell_ProjectileDelay = property.FindPropertyRelative("spell_ProjectileDelay");
    //    SerializedProperty spell_Range_Duration = property.FindPropertyRelative("spell_Range_Duration");
    //    SerializedProperty spell_Range_TicDMG = property.FindPropertyRelative("spell_Range_TicDMG");
    //    SerializedProperty spell_Multy_EA = property.FindPropertyRelative("spell_Multy_EA");
    //    SerializedProperty spell_Multy_Radius = property.FindPropertyRelative("spell_Multy_Radius");
    //    SerializedProperty spell_Range_Area = property.FindPropertyRelative("spell_Range_Area");
    //    SerializedProperty recty = property.FindPropertyRelative("recty");

    //    Rect rect = new Rect(position.x, position.y, position.width, position.height);

    //    EditorGUI.PropertyField(rect, spell_Type, new GUIContent("Spell Type")); rect.y += EditorGUIUtility.singleLineHeight;
    //    EditorGUI.PropertyField(rect, spell_Name, new GUIContent("Spell Name")); rect.y += EditorGUIUtility.singleLineHeight;
    //    recty.intValue = 2;
    //    EditorGUI.EndProperty();
    //}

    //public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    //{
    //    SerializedProperty recty = property.FindPropertyRelative("recty");

    //    return EditorGUIUtility.singleLineHeight * recty.intValue + EditorGUIUtility.standardVerticalSpacing;
    //}
}
