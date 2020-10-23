using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Vector4Bounds))]
public class CustomVector4BoundsDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //EditorGUI.BeginProperty(new Rect(0, 0, 500, 500), label, property);


        Rect rightXRect = EditorGUI.PrefixLabel(new Rect(position.x, position.y, position.width * 0.5f, position.height), GUIUtility.GetControlID(FocusType.Passive), label);
        Rect rightYRect = EditorGUI.PrefixLabel(new Rect(position.x + position.width * 0.5F, position.y, position.width * 0.5f, position.height), GUIUtility.GetControlID(FocusType.Passive), label);
        Rect leftXRect = EditorGUI.PrefixLabel(new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight + 1, position.width, EditorGUIUtility.singleLineHeight), GUIUtility.GetControlID(FocusType.Passive), label);
        Rect leftYRect = EditorGUI.PrefixLabel(new Rect(position.x + 120, position.y + 120, 30, position.height), GUIUtility.GetControlID(FocusType.Passive), label);

        /*
        if (position.height > 16f)
        {
            position.height = 16f;
            EditorGUI.indentLevel += 1;
            contentPosition = EditorGUI.IndentedRect(position);
            contentPosition.y += 18f;
        }
        contentPosition.width *= 0.75f;
        EditorGUI.indentLevel = 0;*/

        SerializedProperty rightXProp = property.FindPropertyRelative("rightX");
        SerializedProperty rightYProp = property.FindPropertyRelative("rightY");
        SerializedProperty leftXProp = property.FindPropertyRelative("leftX");
        SerializedProperty leftYProp = property.FindPropertyRelative("leftY");

        float oldWidth = EditorGUIUtility.labelWidth;
        EditorGUIUtility.labelWidth *= 0.5f;

        EditorGUI.PropertyField(rightXRect, rightXProp, new GUIContent("RightX", "The bottom right of the playspace"));
        EditorGUI.PropertyField(rightYRect, rightYProp, new GUIContent("RightX", "The top right of the playspace"));
        EditorGUI.PropertyField(leftXRect, leftXProp, new GUIContent("RightX", "The bottom left of the playspace"));
        EditorGUI.PropertyField(leftYRect, leftYProp, new GUIContent("RightX", "The bottom left of the playspace"));

        EditorGUIUtility.labelWidth = oldWidth;

        EditorGUIUtility.labelWidth = 14f;

        //EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float numberOfLines = 1f;
        if (EditorGUIUtility.currentViewWidth < 332) numberOfLines++;
        return numberOfLines * (EditorGUIUtility.singleLineHeight + 1);
    }

}

/*
 * [CustomPropertyDrawer(typeof(Enemy))]
public class MyStructDrawer : PropertyDrawer
{
    public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
    {
        Rect colorRect = new Rect(rect.x, rect.y, rect.width * 0.5f, rect.height);
        Rect speedRect = new Rect(rect.x + rect.width * 0.5F, rect.y, rect.width * 0.5f, rect.height);
        Rect vectorRect = new Rect(rect.x, rect.y + EditorGUIUtility.singleLineHeight + 1, rect.width, EditorGUIUtility.singleLineHeight);

        SerializedProperty colorProp = property.FindPropertyRelative("enemyColor");
        SerializedProperty speedProp = property.FindPropertyRelative("enemySpeed");
        SerializedProperty vectorProp = property.FindPropertyRelative("spawnPosition");

        float oldWidth = EditorGUIUtility.labelWidth;
        EditorGUIUtility.labelWidth *= 0.5f;

        EditorGUI.PropertyField(colorRect, colorProp);
        EditorGUI.PropertyField(speedRect, speedProp);
        EditorGUI.PropertyField(vectorRect, vectorProp);

        EditorGUIUtility.labelWidth = oldWidth;
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float numberOfLines = 1f;
        if (EditorGUIUtility.currentViewWidth < 332) numberOfLines++;
        return numberOfLines * (EditorGUIUtility.singleLineHeight + 1);
    }

}

 */