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
        UpdateScreenProportions();
    }

    void UpdateScreenProportions()
    {
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

        // get the player's positions and convert to virtual screen proportions
        leftPlayerspace = ScaleGameToScreen(new Vector2(movementBehavior.playSpace.leftX, movementBehavior.playSpace.leftY));
        rightPlayspace = ScaleGameToScreen(new Vector2(movementBehavior.playSpace.rightX, movementBehavior.playSpace.rightY));
    }

    private void OnGUI()
    {
        if (movementBehavior != null)
        {
           // UpdateScreenProportions();
            
            // draw the virtual screen
            EditorGUI.DrawRect(screenSpace, Color.black);
            EditorGUI.DrawRect(new Rect(scaledPosition.x, scaledPosition.y, 10, 10), Color.green);

            // draw extremety handles of the playSpace
            botLeft = new Rect(leftPlayerspace.x, leftPlayerspace.y, 10, 10);
            topRight = new Rect(rightPlayspace.x, rightPlayspace.y, 10, 10);
            //topLeft = new Rect(leftPlayerspace.x, rightPlayspace.y, 10, 10);
            botRight = new Rect(rightPlayspace.x, leftPlayerspace.y, 10, 10);

            if (topLeft.Contains(Event.current.mousePosition) && Event.current.type == EventType.MouseDown)
            {
                track = !track;
                if(track == false)
                {
                    UpdateScreenProportions();
                    Vector2 left = ScaleScreenToGame(new Vector2(botLeft.x, topLeft.y));
                    movementBehavior.playSpace.leftY = left.y;
                    /*
                    Vector2 left = ScaleScreenToGame(new Vector2(botLeft.x, topLeft.y));
                    Vector2 right = ScaleScreenToGame(new Vector2(botRight.x, topRight.y));
                    
                    movementBehavior.playSpace.leftX = left.x;
                    movementBehavior.playSpace.leftY = left.y;
                    movementBehavior.playSpace.rightX = right.x;
                    movementBehavior.playSpace.rightY = right.y;*/
                }
            }
            if(track) TrackMouse(ref topLeft);

            EditorGUI.DrawRect(topLeft, Color.white); 
            EditorGUI.DrawRect(topRight, Color.white);
            EditorGUI.DrawRect(botLeft, Color.white); 
            EditorGUI.DrawRect(botRight, Color.white);
                        
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

    private Vector2 ScaleScreenToGame(Vector2 screenPos)
    {
        double xPos = CustomScaler.Scale(screenPos.x, windowSize.leftX, windowSize.rightX, 0, Camera.main.pixelWidth);
        double yPos = CustomScaler.Scale(screenPos.y, windowSize.leftY, windowSize.rightY, 0, Camera.main.pixelHeight);

        return new Vector2((float)xPos, (float)yPos);
    }
}
