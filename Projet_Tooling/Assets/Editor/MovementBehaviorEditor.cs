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

        foldoutState = EditorGUILayout.Foldout(foldoutState, "Vector4 Bounds", true);

        if(foldoutState)EditorGUILayout.PropertyField(vectorBounds);

        if (GUILayout.Button("Open Playspace Editor")) PlaySpaceEditorWindow.InitWithContent(target as MovementBehavior);

        EditorGUILayout.EndVertical();
        serializedObject.ApplyModifiedProperties();

    }
}
