using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelProfile))]
public class LevelProfileInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Open Editor")) FirstWindow.InitWithContent(target as LevelProfile);
    }
}
