using UnityEngine;
using UnityEditor;
using Gameplay.Player;

[CustomEditor(typeof(MovementBehavior))]
public class MovementBehaviorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Open Playspace Editor")) PlaySpaceEditorWindow.InitWithContent(target as MovementBehavior);
    }
}
