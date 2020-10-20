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
        if (GUILayout.Button("Open Playspace Editor")) PlaySpaceEditorWindow.InitWithContent(target as MovementBehavior);

        serializedObject.Update();
        EditorGUILayout.BeginVertical();

        EditorGUILayout.PropertyField(playerSpeed);
        EditorGUILayout.PropertyField(vectorBounds);

        foldoutState = EditorGUILayout.Foldout(foldoutState, "Vector4 Bounds", true);

        if(foldoutState)
        {
            EditorGUILayout.BeginHorizontal();
            //GUILayout.Label("Vector4 Bounds");
            EditorGUILayout.PropertyField(vectorBounds.FindPropertyRelative("rightX"), GUIContent.none);
            EditorGUILayout.PropertyField(vectorBounds.FindPropertyRelative("rightY"), GUIContent.none);
            EditorGUILayout.PropertyField(vectorBounds.FindPropertyRelative("leftX"), GUIContent.none);
            EditorGUILayout.PropertyField(vectorBounds.FindPropertyRelative("leftY"), GUIContent.none);
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.EndVertical();
        serializedObject.ApplyModifiedProperties();
        
    }
}
