using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomPropertyDrawer(typeof(MinMax<>))]
public class MinMaxDrawer : PropertyDrawer
{
    private readonly string[] Labels = { "Min", "Max" };

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var min = property.FindPropertyRelative("_min");
        var max = property.FindPropertyRelative("_max");

        switch (min.propertyType)
        {
            case SerializedPropertyType.Integer:
                DrawInt(position, property.displayName, min, max);
                break;
            case SerializedPropertyType.Float:
                DrawFloat(position, property.displayName, min, max);
                break;
            default:
                EditorGUI.LabelField(position, "Cant draw min max property for type: " + min.propertyType);
                break;
        }
    }

    private void DrawInt(Rect position, string label, SerializedProperty min, SerializedProperty max)
    {
        int[] array = { min.intValue, max.intValue };
        DrawFields(position, ref array, label, EditorGUI.IntField);
        min.intValue = array[0];
        max.intValue = array[1];
    }

    private void DrawFloat(Rect position, string label, SerializedProperty min, SerializedProperty max)
    {
        float[] array = { min.floatValue, max.floatValue };
        DrawFields(position, ref array, label, EditorGUI.FloatField);
        min.floatValue = array[0];
        max.floatValue = array[1];
    }

    protected void DrawFields<T>(Rect rect, ref T[] values, string mainLabel, Func<Rect, GUIContent, T, T> fieldDrawer)
    {
        var labelRect = new Rect(rect.x, rect.y, EditorGUIUtility.labelWidth, rect.height);
        var fieldRect = new Rect(rect.x + labelRect.width, rect.y, (rect.width - labelRect.width - EditorGUIUtility.standardVerticalSpacing) / values.Length, rect.height);

        EditorGUI.LabelField(labelRect, mainLabel);

        var cachedLabelWidth = EditorGUIUtility.labelWidth;
        for (int i = 0; i < values.Length; i++)
        {
            var label = new GUIContent(Labels[i]);
            var size = EditorStyles.label.CalcSize(label);
            EditorGUIUtility.labelWidth = size.x + EditorGUIUtility.standardVerticalSpacing;
            values[i] = fieldDrawer(fieldRect, new GUIContent(Labels[i]), values[i]);
            fieldRect.x += fieldRect.width + EditorGUIUtility.standardVerticalSpacing;
        }
        EditorGUIUtility.labelWidth = cachedLabelWidth;
    }

}