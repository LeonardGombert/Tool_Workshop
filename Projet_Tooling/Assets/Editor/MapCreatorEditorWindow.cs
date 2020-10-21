using Gameplay.Player;
using System;
using UnityEditor;
using UnityEngine;

public class MapCreatorEditorWindow : EditorWindow
{
    [MenuItem("Window/Map Creator Editor Window %q")]
    public static void Init()
    {
        MapCreatorEditorWindow window = EditorWindow.GetWindow(typeof(MapCreatorEditorWindow)) as MapCreatorEditorWindow;
        window.Show();
    }

    private void OnGUI()
    {
    }

    private void OnEnable()
    {
        foreach (Transform item in GameObject.Find("GRID").transform) DestroyImmediate(item.gameObject);
        DrawGrid();
    }

    private void DrawGrid()
    {
        int gridX = 1000, gridY = 1000;
        float offset = 30f;

        for (int i = 0, x = 0; x < gridX; x+= 50)
        {
            for (int y = 0; y < gridY; y+=50, i++)
            {
                Rect myRect = new Rect(50 + x * offset, 50 + y * offset, 30, 30);
                if (GUI.Button(myRect, "1")) ChangeColor(myRect);
                //GameObject yes = GameObject.Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube), new Vector3(x, 0, y), Quaternion.identity, GameObject.Find("GRID").transform);
            }
        }
    }

    private void ChangeColor(Rect targetRect)
    {
        EditorGUI.DrawRect(targetRect, Color.red);
    }
}
