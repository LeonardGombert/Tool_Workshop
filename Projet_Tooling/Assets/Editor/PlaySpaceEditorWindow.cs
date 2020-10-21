using Gameplay.Player;
using System;
using UnityEditor;
using UnityEngine;

public class PlaySpaceEditorWindow : EditorWindow
{
    MovementBehavior movementBehavior;
    Vector4Bounds windowSize;

    Rect screenSpace, playerRect;
    Rect botLeft, topRight, topLeft, botRight;

    Vector2 leftPlayerspace, rightPlayspace;
    Vector3 scaledPosition;

    float scale = 1.0f;



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

            // draw the player... 
            playerRect = new Rect(0, 0, 10, 10);

            // ...and center him
            playerRect.center = new Vector2(scaledPosition.x, scaledPosition.y);

            // draw the virtual screen and the player
            EditorGUI.DrawRect(screenSpace, Color.black);
            EditorGUI.DrawRect(playerRect, Color.green);
            #endregion

            #region Calculate Virtual Screen Playspace
            // get the player's positions and convert to virtual screen proportions
            leftPlayerspace = ScaleGameToScreen(new Vector2(movementBehavior.playSpace.leftX, movementBehavior.playSpace.leftY));
            rightPlayspace = ScaleGameToScreen(new Vector2(movementBehavior.playSpace.rightX, movementBehavior.playSpace.rightY));

            // create the Rects...
            botLeft = new Rect(0, 0, 10, 10);
            topRight = new Rect(0, 0, 10, 10);
            topLeft = new Rect(0, 0, 10, 10);
            botRight = new Rect(0, 0, 10, 10);

            // ... and center them on the position of playSpace extremities
            botLeft.center = new Vector2(leftPlayerspace.x, leftPlayerspace.y);
            topRight.center = new Vector2(rightPlayspace.x, rightPlayspace.y);
            topLeft.center = new Vector2(leftPlayerspace.x, rightPlayspace.y);
            botRight.center = new Vector2(rightPlayspace.x, leftPlayerspace.y);
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

            #region Check if moving a rect
            // if the user clicks on a square
            if (botLeft.Contains(Event.current.mousePosition) && Event.current.type == EventType.MouseDown)
            {

            }

            scale = EditorGUI.Slider(new Rect(5, 5, 150, 20), scale, 10, 200);

            botLeft.center = ScaleGameToScreen(new Vector2(scale, scale));
            botLeft.center = ScaleGameToScreen(new Vector2(scale, scale));
            topRight.center = ScaleGameToScreen(new Vector2(Camera.main.pixelWidth - scale, Camera.main.pixelHeight - scale));
            topRight.center = ScaleGameToScreen(new Vector2(Camera.main.pixelWidth - scale, Camera.main.pixelHeight - scale));
            #endregion

            #region LAST STEP : Apply Any Changes in Editor Window to Player Values
            // assign the virtual screen values to a new Vector4    
            Vector4Bounds calcBounds = new Vector4Bounds();
            calcBounds.leftX = botLeft.center.x; //coords in pixels
            calcBounds.leftY = botLeft.center.y;
            calcBounds.rightX = topRight.center.x;
            calcBounds.rightY = topRight.center.y;

            // convert the virtual screen values to Game Screen Values
            Vector4Bounds appliedBounds = ScaleScreenToGame(calcBounds);
            movementBehavior.playSpace.leftX = appliedBounds.leftX;
            movementBehavior.playSpace.leftY = appliedBounds.leftY;
            movementBehavior.playSpace.rightX = appliedBounds.rightX;
            movementBehavior.playSpace.rightY = appliedBounds.rightY;
            #endregion
        }

        Repaint();
    }

    // attach to Mouse
    private void TrackMouse(ref Rect myRect)
    {
        Vector2 mousePos = Event.current.mousePosition;
        Vector2 scaledMousePos = ScaleWindowToScreen(mousePos);

        myRect.position = mousePos;
    }

    #region Scaling Methods
    private Vector4Bounds ScaleScreenToGame(Vector4Bounds screenPosition)
    {
        double leftX = CustomScaler.Scale(screenPosition.leftX, windowSize.leftX, windowSize.rightX, 0, Camera.main.pixelWidth);
        double leftY = CustomScaler.Scale(screenPosition.leftY, windowSize.leftY, windowSize.rightY, 0, Camera.main.pixelHeight);
        double rightX = CustomScaler.Scale(screenPosition.rightX, windowSize.leftX, windowSize.rightX, 0, Camera.main.pixelWidth);
        double rightY = CustomScaler.Scale(screenPosition.rightY, windowSize.leftY, windowSize.rightY, 0, Camera.main.pixelHeight);

        return new Vector4Bounds((float)leftX, (float)leftY, (float)rightX, (float)rightY);
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
    #endregion
}
