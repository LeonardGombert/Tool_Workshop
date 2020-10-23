using UnityEngine;
using UnityEditor;
using Gameplay.Player;

[CustomEditor(typeof(MovementBehavior))]
public class MovementBehaviorEditor : Editor
{
    MovementBehavior movementBehavior;

    SerializedProperty playerSpeed;
    SerializedProperty transitionSpeed;
    SerializedProperty tweenDuration;
    SerializedProperty vectorBounds;

    bool foldoutState;

    private void OnEnable()
    {
        movementBehavior = target as MovementBehavior;
        playerSpeed = serializedObject.FindProperty(nameof(movementBehavior.movementSpeed));
        transitionSpeed = serializedObject.FindProperty(nameof(movementBehavior.transitionSpeed));
        tweenDuration = serializedObject.FindProperty(nameof(movementBehavior.tweenDuration));
        vectorBounds = serializedObject.FindProperty(nameof(movementBehavior.playspace));
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.BeginVertical();

        EditorGUILayout.PropertyField(playerSpeed, new GUIContent("Player movement Speed", "This variable determines how fast the player moves around the screen"));
        EditorGUILayout.PropertyField(transitionSpeed, new GUIContent("Playspace Reentry Speed", "This variable determines how fast the player will move back into the playspace when it changes"));
        EditorGUILayout.PropertyField(tweenDuration, new GUIContent("Playspace Change Speed", "This variable determines how fast the playspace will change"));
        foldoutState = EditorGUILayout.Foldout(foldoutState, "Vector4 Bounds", true);
        if (foldoutState) EditorGUILayout.PropertyField(vectorBounds);

        if(GUILayout.Button(new GUIContent("Open Playspace Editor", "Open the Visual Editor to edit the player's Playspace area")))
            PlaySpaceEditorWindow.InitWithContent(target as MovementBehavior);

        EditorGUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }
}
