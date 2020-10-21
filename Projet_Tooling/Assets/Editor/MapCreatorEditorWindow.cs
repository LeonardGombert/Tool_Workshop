using Gameplay.Player;
using System;
using UnityEditor;
using UnityEngine;

public class MapCreatorEditorWindow : EditorWindow
{
    [MenuItem("Window/Playspace Editor Window %w")]
    public static void Init()
    {
        MapCreatorEditorWindow window = EditorWindow.GetWindow(typeof(MapCreatorEditorWindow)) as MapCreatorEditorWindow;
        window.Show();
    }

    private void OnGUI()
    {
        DrawGrid();
    }

    private void DrawGrid()
    {
        int gridX = 200, gridY = 200;
        float offset = 15f;

        for (int i = 0, x = 0; x < gridX; x++)
        {
            for (int y = 0; y < gridY; y++, i++)
            {
                Rect myRect = new Rect(50 + x * offset, 50 + y * offset, 10, 10);
                if (GUI.Button(myRect, "")) ChangeColor(myRect);
            }
        }
    }

    private void ChangeColor(Rect targetRect)
    {
        EditorGUI.DrawRect(targetRect, Color.red);
    }
}
