using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Vector4Bounds))]
public class CustomVector4BoundsDrawer : PropertyDrawer
{
    SerializedProperty leftXProp;
    SerializedProperty leftYProp;
    SerializedProperty rightXProp;
    SerializedProperty rightYProp;

    string name;
    bool cache = false;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (!cache)
        {
            //get the name before it's gone
            name = property.displayName;

            //get the values
            property.Next(true);
            leftXProp = property.Copy();
            property.Next(true);
            leftYProp = property.Copy();
            property.Next(true);
            rightXProp = property.Copy();
            property.Next(true);
            rightYProp = property.Copy();

            cache = true;
        }

        Rect contentPosition = EditorGUI.PrefixLabel(position, new GUIContent(name));

        if (position.height > 16f)
        {
            position.height = 16f;
            EditorGUI.indentLevel += 1;
            contentPosition = EditorGUI.IndentedRect(position);
            contentPosition.y += 18f;
        }

        //EditorGUIUtility.labelWidth = 14f;
        contentPosition.width *= 0.25f;

        EditorGUI.BeginProperty(contentPosition, label, leftXProp);
        {
            EditorGUI.BeginChangeCheck();
            EditorGUI.LabelField(contentPosition, "LeftX");
            Rect newRect = contentPosition;
            newRect.x += 50;
            float newVal = EditorGUI.FloatField(newRect, leftXProp.floatValue);
            if(EditorGUI.EndChangeCheck()) leftXProp.floatValue = newVal;
        }
        EditorGUI.EndProperty();

        contentPosition.x += 200;

        EditorGUI.BeginProperty(contentPosition, label, leftYProp);
        {
            EditorGUI.BeginChangeCheck();
            EditorGUI.LabelField(contentPosition, "LeftY");
            Rect newRect = contentPosition;
            newRect.x += 50;
            float newVal = EditorGUI.FloatField(newRect, leftYProp.floatValue);
            if (EditorGUI.EndChangeCheck()) leftYProp.floatValue = newVal;
        }
        EditorGUI.EndProperty();

        contentPosition.x = position.x;
        contentPosition.y += 20;
        EditorGUI.indentLevel += 1;

        EditorGUI.BeginProperty(contentPosition, label, rightXProp);
        {
            EditorGUI.BeginChangeCheck();
            EditorGUI.LabelField(contentPosition, "RightX");
            Rect newRect = contentPosition;
            newRect.x += 50;
            float newVal = EditorGUI.FloatField(newRect, rightXProp.floatValue);
            if (EditorGUI.EndChangeCheck()) rightXProp.floatValue = newVal;
        }
        EditorGUI.EndProperty();

        contentPosition.x += 200;

        EditorGUI.BeginProperty(contentPosition, label, rightYProp);
        {
            EditorGUI.BeginChangeCheck();
            EditorGUI.LabelField(contentPosition, "RightY");
            Rect newRect = contentPosition;
            newRect.x += 50;
            float newVal = EditorGUI.FloatField(newRect, rightYProp.floatValue);
            if (EditorGUI.EndChangeCheck()) rightYProp.floatValue = newVal;
        }
        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float numberOfLines = 3;
        if (EditorGUIUtility.currentViewWidth < 332) numberOfLines++;
        return numberOfLines * (EditorGUIUtility.singleLineHeight + 1);
    }

}