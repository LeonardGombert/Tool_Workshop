using Gameplay.Player;
using System;
using UnityEditor;
using UnityEngine;

public class PlaySpaceEditorWindow : EditorWindow
{
    MovementBehavior movementBehavior;

    [MenuItem("Window/Playspace Editor Window %w")]
    public static void Init()
    {
        PlaySpaceEditorWindow window = EditorWindow.GetWindow(typeof(PlaySpaceEditorWindow)) as PlaySpaceEditorWindow;
        window.Show();
    }

    public static void InitWithContent(MovementBehavior _movementBehavior)
    {
        PlaySpaceEditorWindow window = EditorWindow.GetWindow(typeof(PlaySpaceEditorWindow)) as PlaySpaceEditorWindow;
        window.movementBehavior = _movementBehavior;
        window.Show();
    }

    private void OnGUI()
    {
        DrawGrid();
        /*EditorGUI.DrawRect(new Rect(50, 50, Screen.width*0.5f, Screen.width * 0.5f), Color.green);
        Rect closeButtonRect = new Rect(30, 200, 60, 20);
        if (GUI.Button(closeButtonRect, "Close")) Close();*/
    }

    private void DrawGrid()
    {
        int gridX = 200, gridY = 200;
        float offset = 15f;

        for (int i = 0, x = 0; x < gridX; x++)
        {
            for (int y = 0; y < gridY; y++, i++)
            {
                Rect myRect = new Rect(50+x * offset, 50+y * offset, 10, 10);
                if (GUI.Button(myRect, "")) ChangeColor(myRect);
            }
        }
    }

    private void ChangeColor(Rect targetRect)
    {
        EditorGUI.DrawRect(targetRect, Color.red);
    }
}
