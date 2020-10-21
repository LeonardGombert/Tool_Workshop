using Gameplay.Player;
using System;
using UnityEditor;
using UnityEngine;

public class PlaySpaceEditorWindow : EditorWindow
{
    MovementBehavior movementBehavior;
    Vector4Bounds windowSize;

    Rect screenSpace;
    Vector2 leftPlayerspace, rightPlayspace;
    Vector3 scaledPosition;

    Rect botLeft, topRight, topLeft, botRight;
    bool track = false;

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
        if (movementBehavior == null) movementBehavior = GameObject.Find("Player").GetComponent<MovementBehavior>();
        topLeft = new Rect(leftPlayerspace.x, rightPlayspace.y, 10, 10);

        movementBehavior.playSpace.leftX = 100;
        movementBehavior.playSpace.leftY = 100;
        movementBehavior.playSpace.rightX = Camera.main.pixelWidth - 100;
        movementBehavior.playSpace.rightY = Camera.main.pixelHeight - 100;
    }

    private void OnGUI()
    {
        if (movementBehavior != null)
        {
            #region Create Virtual Screen from Player Window
            // create the virtual screen and position it
            screenSpace = new Rect(0, 0, Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2);
            screenSpace.center = new Vector2(position.width / 2, position.height / 2);

            // define the virtual screen's bounds
            windowSize.leftX = screenSpace.center.x - screenSpace.width / 2;
            windowSize.leftY = screenSpace.center.y + screenSpace.height / 2;
            windowSize.rightX = screenSpace.center.x + screenSpace.width / 2;
            windowSize.rightY = screenSpace.center.y - screenSpace.height / 2;

            // get the player's screenPosition and convert to the virtual screen proportions
            Vector3 screenPos = Camera.main.WorldToScreenPoint(movementBehavior.screenSpacePosition);
            scaledPosition = ScaleGameToScreen(screenPos);

            // draw the virtual screen
            EditorGUI.DrawRect(screenSpace, Color.black);
            EditorGUI.DrawRect(new Rect(scaledPosition.x, scaledPosition.y, 10, 10), Color.green);
            #endregion

            #region Calculate Virtual Screen Playspace
            // get the player's positions and convert to virtual screen proportions
            leftPlayerspace = ScaleGameToScreen(new Vector2(movementBehavior.playSpace.leftX, movementBehavior.playSpace.leftY));
            rightPlayspace = ScaleGameToScreen(new Vector2(movementBehavior.playSpace.rightX, movementBehavior.playSpace.rightY));

            // calculate position of playSpace extremities
            botLeft = new Rect(leftPlayerspace.x, leftPlayerspace.y, 10, 10);
            topRight = new Rect(rightPlayspace.x, rightPlayspace.y, 10, 10);
            topLeft = new Rect(leftPlayerspace.x, rightPlayspace.y, 10, 10);
            botRight = new Rect(rightPlayspace.x, leftPlayerspace.y, 10, 10);
            #endregion

            #region Draw Playspace on Virtual Screen
            // draw extremities of the playSpace
            EditorGUI.DrawRect(topLeft, Color.white);
            EditorGUI.DrawRect(topRight, Color.white);
            EditorGUI.DrawRect(botLeft, Color.white);
            EditorGUI.DrawRect(botRight, Color.white);

            // draw the lines between the playSpaces
            Handles.BeginGUI();
            Handles.color = Color.white;
            Handles.DrawLine(botLeft.center, topLeft.center);
            Handles.DrawLine(topLeft.center, topRight.center);
            Handles.DrawLine(topRight.center, botRight.center);
            Handles.DrawLine(botRight.center, botLeft.center);
            Handles.EndGUI();
            #endregion

            #region Apply Any Changes in Editor Window to Player Values
            // convert the virtual screen values to game window values

            Vector4Bounds calcBounds = new Vector4Bounds();
            calcBounds.leftX = botLeft.x; //coords in pixels
            calcBounds.leftY = topLeft.y;
            calcBounds.rightX = botRight.x;
            calcBounds.rightY = topRight.y;

            Vector4Bounds appliedBounds = ScaleScreenToGame(calcBounds);
            movementBehavior.playSpace.leftX = appliedBounds.leftX;
            movementBehavior.playSpace.leftY = appliedBounds.leftY;
            movementBehavior.playSpace.rightX = appliedBounds.rightX;
            movementBehavior.playSpace.rightY = appliedBounds.rightY;

            /*
            Vector2 botLeft2 = ScaleScreenToGame(new Vector2(botLeft.x, topLeft.y));
            Vector2 topLeft2 = ScaleScreenToGame(new Vector2(topLeft.x, topLeft.y));
            Vector2 botRight2 = ScaleScreenToGame(new Vector2(botRight.x, topRight.y));
            Vector2 topRight2 = ScaleScreenToGame(new Vector2(topRight.x, topRight.y));

            // set the editor window's values as the player's values
            movementBehavior.playSpace.leftX = botLeft2.x;
            movementBehavior.playSpace.leftY = topLeft2.y;
            movementBehavior.playSpace.rightX = botRight2.x;
            movementBehavior.playSpace.rightY = topRight2.y;*/
            #endregion
        }

        Repaint();

        #region comment
        /*if (topLeft.Contains(Event.current.mousePosition) && Event.current.type == EventType.MouseDown)
        {
            track = !track;
            if (track == false)
            {
                Vector2 left = ScaleScreenToGame(new Vector2(botLeft.x, topLeft.y));
                movementBehavior.playSpace.leftY = left.y;

            }
        }
        if (track) TrackMouse(ref topLeft);*/
        #endregion
    }

    private Vector4Bounds ScaleScreenToGame(Vector4Bounds screenPosition)
    {
        double leftX = CustomScaler.Scale(screenPosition.leftX, windowSize.leftX, windowSize.rightX, 0, Camera.main.pixelWidth);
        double leftY = CustomScaler.Scale(screenPosition.leftY, windowSize.leftX, windowSize.rightX, 0, Camera.main.pixelWidth);
        double rightX = CustomScaler.Scale(screenPosition.rightX, windowSize.leftX, windowSize.rightX, 0, Camera.main.pixelWidth);
        double rightY = CustomScaler.Scale(screenPosition.rightY, windowSize.leftX, windowSize.rightX, 0, Camera.main.pixelWidth);
        double yPos = CustomScaler.Scale(screenPos.y, windowSize.leftY, windowSize.rightY, 0, Camera.main.pixelHeight);

        return new Vector4Bounds((float)leftX, (float)leftY, (float)rightX, (float)rightY);
    }

    // attach to Mouse
    private void TrackMouse(ref Rect myRect)
    {
        Vector2 mousePos = Event.current.mousePosition;
        Vector2 scaledMousePos = ScaleWindowToScreen(mousePos);

        myRect.position = mousePos;
    }

    private Vector2 ScaleGameToScreen(Vector2 screenPos)
    {
        double xPos = CustomScaler.Scale(screenPos.x, 0, Camera.main.pixelWidth, windowSize.leftX, windowSize.rightX);
        double yPos = CustomScaler.Scale(screenPos.y, 0, Camera.main.pixelHeight, windowSize.leftY, windowSize.rightY);

        return new Vector2((float)xPos, (float)yPos);
    }

    private Vector2 ScaleWindowToScreen(Vector2 screenPos)
    {
        double xPos = CustomScaler.Scale(screenPos.x, 0, Screen.width, windowSize.leftX, windowSize.rightX);
        double yPos = CustomScaler.Scale(screenPos.y, 0, Screen.height, windowSize.leftY, windowSize.rightY);

        return new Vector2((float)xPos, (float)yPos);
    }
}
