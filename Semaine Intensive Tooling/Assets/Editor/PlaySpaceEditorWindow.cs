using Gameplay.Player;
using System;
using UnityEditor;
using UnityEngine;

public class PlaySpaceEditorWindow : EditorWindow
{
    MovementBehavior movementBehavior;
    Vector4Bounds windowSize;

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

    private void OnEnable()
    {
    }

    private void OnGUI()
    {
        Rect myRect = new Rect(0, 0, Screen.width / 2, Screen.height/ 2);
        myRect.center = new Vector2(position.width / 2, position.height / 2);
        EditorGUI.DrawRect(myRect, Color.black);


        windowSize.leftX = myRect.center.x - Screen.width/ 4;
        windowSize.leftY = myRect.center.y;
        windowSize.rightX = myRect.center.x + Screen.width / 4;
        windowSize.rightY = myRect.center.y;
        EditorGUI.DrawRect(new Rect(windowSize.leftX, windowSize.leftY, 5, 5), Color.white);

        double xPos = (int)CustomScaler.Scale(movementBehavior.transform.position.x, 100,
            Camera.main.pixelWidth - 100, windowSize.leftX, windowSize.rightX);
        double yPos = (int)CustomScaler.Scale(movementBehavior.transform.position.y, 100,
            Camera.main.pixelHeight - 100, windowSize.leftY, windowSize.rightY);

        EditorGUI.DrawRect(new Rect((float)xPos, (float)yPos, 10, 10), Color.green);

        Debug.Log("Scaled to window x is : " + xPos);
        Debug.Log("Scaled to window y is : " + yPos);

        Repaint();
    }
}
