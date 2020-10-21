using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MySecondGameplayScript))]
public class MySecondGameplayEditor : Editor
{
    MySecondGameplayScript mySecondGameplayScript;
    SerializedProperty myColorProperty;
    SerializedProperty redLevel;
    SerializedProperty myStrings;

    SerializedProperty myEnemy;

    private void OnEnable()
    {
        mySecondGameplayScript = target as MySecondGameplayScript;
        
        myEnemy = serializedObject.FindProperty("myEnemy");

        myStrings = serializedObject.FindProperty(nameof(mySecondGameplayScript.myStrings));
        myColorProperty = serializedObject.FindProperty(nameof(mySecondGameplayScript.myColor));

        redLevel = myColorProperty.FindPropertyRelative("r");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        if (GUILayout.Button("Turn Red")) myColorProperty.colorValue = Color.red;
        if (GUILayout.Button("Turn Green")) myColorProperty.colorValue = Color.green;

        EditorGUILayout.PropertyField(myColorProperty);

        //EditorGUI.BeginChangeCheck();
        redLevel.floatValue = EditorGUILayout.Slider("Red", redLevel.floatValue, 0f, 1f);
        EditorGUILayout.LabelField(redLevel.floatValue.ToString());
        //if (EditorGUI.EndChangeCheck()) redLevel.floatValue = tempRedLevel;

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add One")) myStrings.arraySize++;
        if (GUILayout.Button("Remove One")) myStrings.arraySize--;
        EditorGUILayout.EndHorizontal();

        if(myStrings.arraySize > 0)
        {
            for (int i = 0; i < myStrings.arraySize; i++)
            {
                EditorGUILayout.PropertyField(myStrings.GetArrayElementAtIndex(i), new GUIContent("My String " + i, "This is a tooltip"));
            }
        }

        EditorGUILayout.PropertyField(myEnemy);

        serializedObject.ApplyModifiedProperties();
    }
}