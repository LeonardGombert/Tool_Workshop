using UnityEngine;
using UnityEditor;
using Gameplay.Player;

[CustomEditor(typeof(MovementBehavior))]
public class MovementBehaviorEditor : Editor
{
    MovementBehavior movementBehavior;

    SerializedProperty playerSpeed;
    SerializedProperty vectorBounds;

    bool foldoutState;

    private void OnEnable()
    {
        movementBehavior = target as MovementBehavior;
        playerSpeed = serializedObject.FindProperty(nameof(movementBehavior.movementSpeed));
        vectorBounds = serializedObject.FindProperty(nameof(movementBehavior.playSpace));
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.BeginVertical();

        EditorGUILayout.PropertyField(playerSpeed);
        EditorGUILayout.PropertyField(vectorBounds);

        foldoutState = EditorGUILayout.Foldout(foldoutState, "Vector4 Bounds", true);

        if(foldoutState)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Vector4 Bounds");
            EditorGUILayout.PropertyField(vectorBounds.FindPropertyRelative("botLeft"), GUIContent.none);
            EditorGUILayout.PropertyField(vectorBounds.FindPropertyRelative("topLeft"), GUIContent.none);
            EditorGUILayout.PropertyField(vectorBounds.FindPropertyRelative("botRight"), GUIContent.none);
            EditorGUILayout.PropertyField(vectorBounds.FindPropertyRelative("topRight"), GUIContent.none);
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.EndVertical();
        serializedObject.ApplyModifiedProperties();
        if (GUILayout.Button("Open Playspace Editor")) PlaySpaceEditorWindow.InitWithContent(target as MovementBehavior);
    }
}
