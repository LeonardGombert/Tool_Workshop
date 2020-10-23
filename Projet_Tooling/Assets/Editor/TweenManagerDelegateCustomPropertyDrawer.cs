using UnityEditor;
using UnityEngine;










// Je n'ai pas su/eu le temps de comprendre comment draw un custom property drawer pour un delegate /: surtout celui du type que j'avais déclaré








[CustomPropertyDrawer(typeof(TweenManager.TweenFunction))]
public class TweenManagerDelegateCustomPropertyDrawer : PropertyDrawer
{/*
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        EditorGUI.LabelField(position, label);

        var genderRect = new Rect(position.x, position.y + 54, position.width, 16);
        EditorGUI.indentLevel++;
        EditorGUI.PropertyField(genderRect, property.FindPropertyRelative("tweenFunctions"));

        EditorGUI.indentLevel--;

        EditorGUI.EndProperty();
    }*/
}