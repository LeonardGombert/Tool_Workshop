using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Enemy))]
public class MyStructDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float numberOfLines = 1f;
        if (EditorGUIUtility.currentViewWidth < 332) numberOfLines++;
        return numberOfLines * (EditorGUIUtility.singleLineHeight + 1);
    }

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
}
