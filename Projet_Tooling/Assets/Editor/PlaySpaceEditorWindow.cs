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
        movementBehavior = GameObject.Find("Player").GetComponent<MovementBehavior>();
    }

    private void OnGUI()
    {
        if (movementBehavior != null)
        {
            Rect myRect = new Rect(0, 0, position.width / 2, position.width / 2);
            myRect.center = new Vector2(position.width / 2, position.height / 2);
            EditorGUI.DrawRect(myRect, Color.black);

            windowSize.leftX = myRect.center.x - myRect.width / 2;
            windowSize.leftY = myRect.center.y + myRect.height / 2;
            windowSize.rightX = myRect.center.x + myRect.width / 2;
            windowSize.rightY = myRect.center.y - myRect.height / 2;
            
            float height = Camera.main.orthographicSize * 6.0f;

            double xPos = CustomScaler.Scale(movementBehavior.transform.position.x, - 10,
                10, windowSize.leftX, windowSize.rightX);
            double yPos = CustomScaler.Scale(movementBehavior.transform.position.y, -height,
               height, windowSize.leftY, windowSize.rightY);

            EditorGUI.DrawRect(new Rect((float)xPos, (float)yPos, 10, 10), Color.green);
            Repaint();
        }
    }
}
