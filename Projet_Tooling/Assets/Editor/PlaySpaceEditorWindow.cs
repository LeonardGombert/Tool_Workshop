using Gameplay.Player;
using System;
using UnityEditor;
using UnityEngine;

public class PlaySpaceEditorWindow : EditorWindow
{
    MovementBehavior movementBehavior;
    Vector4Bounds windowSize;
    Vector4Bounds appliedBounds;

    Rect screenSize, playerRect;
    Rect botLeft, topRight, topLeft, botRight;

    Vector2 leftPlayerspace, rightPlayspace;
    Vector3 scaledPosition;

    float xSliderScale = 100f, ySliderScale = 100f;

    TransitionCurveViewerWindow transitionWindow;
    TweenManager.TweenFunction tweenFunction = default; // use for enum ? 
    TweenName selectedTween;
    
    float time;
    
    Vector2 changeBotLeftX, changeTopLeftY, changeBotRightX, changeTopRightY;
    Vector2 startValueLeftX, startValueLeftY, startValueRightX, startValueRightY;

    float tweenDuration = 200f;

    float positionOfBottoms;

    [MenuItem("Window/Playspace Editor Window %w")]
    public static void Init()
    {
        PlaySpaceEditorWindow window = GetWindow<PlaySpaceEditorWindow>();
        window.Show();
    }

    public static void InitWithContent(MovementBehavior _movementBehavior)
    {
        PlaySpaceEditorWindow window = GetWindow<PlaySpaceEditorWindow>();
        window.movementBehavior = _movementBehavior;
        window.Show();
    }

    public static void InitForVisualization(TransitionCurveViewerWindow transitionWindow, TweenManager.TweenFunction myTweenFunction, int tweenIndex)
    {
        PlaySpaceEditorWindow window = GetWindow<PlaySpaceEditorWindow>();
        window.transitionWindow = transitionWindow;
        window.tweenFunction = myTweenFunction;
        window.selectedTween = (TweenName)tweenIndex;
        
        window.startValueLeftX = window.botLeft.position;
        window.startValueLeftY = window.topLeft.position;
        window.startValueRightX = window.botRight.position;
        window.startValueRightY = window.topRight.position;

        window.Show();
    }

    public static void StopVisualizing()
    {
        PlaySpaceEditorWindow window = GetWindow<PlaySpaceEditorWindow>();
        window.transitionWindow = null;
        window.Show();
    }

    private void OnEnable()
    {
        if (movementBehavior == null) movementBehavior = GameObject.Find("Player").GetComponent<MovementBehavior>();
        topLeft = new Rect(leftPlayerspace.x, rightPlayspace.y, 10, 10); // forgot what this was for
    }

    private void OnGUI()
    {
        if (movementBehavior != null)
        {
            #region Create Virtual Screen from Player Window
            // create the virtual screen and position it
            screenSize = new Rect(0, 0, Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2);
            screenSize.center = new Vector2(position.width / 2, (position.height + positionOfBottoms) / 2);

            // define the virtual screen's bounds
            windowSize.leftX = screenSize.center.x - screenSize.width / 2;
            windowSize.leftY = screenSize.center.y + screenSize.height / 2;
            windowSize.rightX = screenSize.center.x + screenSize.width / 2;
            windowSize.rightY = screenSize.center.y - screenSize.height / 2;

            // get the player's screenPosition and convert to the virtual screen proportions
            Vector3 screenPos = Camera.main.WorldToScreenPoint(movementBehavior.screenSpacePosition);
            scaledPosition = ScaleGameToScreen(screenPos);

            // draw the player... 
            playerRect = new Rect(0, 0, 10, 10);

            // ...and center him
            playerRect.center = new Vector2(scaledPosition.x, scaledPosition.y);

            // draw the virtual screen and the player
            EditorGUI.DrawRect(screenSize, Color.black);
            EditorGUI.DrawRect(playerRect, Color.green);
            #endregion

            #region Calculate Virtual Screen Playspace
            // get the player's positions and convert to virtual screen proportions
            leftPlayerspace = ScaleGameToScreen(new Vector2(movementBehavior.playspace.leftX, movementBehavior.playspace.leftY));
            rightPlayspace = ScaleGameToScreen(new Vector2(movementBehavior.playspace.rightX, movementBehavior.playspace.rightY));

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

            #region Make the Playspace transition if you are visualizing
            // if the transition window is open (and you've pressed on one of the visualizations)
            if (transitionWindow != null)
            {
                // increase the time Passed
                time += Time.deltaTime;

                if (time <= tweenDuration)
                {
                    // set the coordinates for the bottom left point
                    changeBotLeftX = startValueLeftX - new Vector2(windowSize.leftX, windowSize.leftY);
                    float botLeftX = tweenFunction(time, windowSize.leftX, changeBotLeftX.x, tweenDuration);
                    float botLeftY = tweenFunction(time, windowSize.leftY, changeBotLeftX.y, tweenDuration);

                    botLeft.center = new Vector2(botLeftX, botLeftY);

                    // set the coordinates for the top left point
                    changeTopLeftY = startValueLeftY - new Vector2(windowSize.leftX, windowSize.rightY);
                    float topLeftX = tweenFunction(time, windowSize.leftX, changeTopLeftY.x, tweenDuration);
                    float topLeftY = tweenFunction(time, windowSize.rightY, changeTopLeftY.y, tweenDuration);

                    topLeft.center = new Vector2(topLeftX, topLeftY);

                    // set the coordinates for the bottom right point
                    changeBotRightX = startValueRightX - new Vector2(windowSize.rightX, windowSize.leftY);
                    float botRightX = tweenFunction(time, windowSize.rightX, changeBotRightX.x, tweenDuration);
                    float botRightY = tweenFunction(time, windowSize.leftY, changeBotRightX.y, tweenDuration);

                    botRight.center = new Vector2(botRightX, botRightY);

                    // set the coordinates for the top right point
                    changeTopRightY = startValueRightY - new Vector2(windowSize.rightX, windowSize.rightY);
                    float topRightX = tweenFunction(time, windowSize.rightX, changeTopRightY.x, tweenDuration);
                    float topRightY = tweenFunction(time, windowSize.rightY, changeTopRightY.y, tweenDuration);

                    topRight.center = new Vector2(topRightX, topRightY);
                }

                // once you've reached the outer limit of the sreen, reset the time
                else time = 0;
            }
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

            #region Change Rect Size with a slider
            // if you aren't currently visualizing translations
            if (transitionWindow == null)
            {
                // create two label fields
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Playspace Width");
                xSliderScale = EditorGUILayout.Slider(xSliderScale, 0, Camera.main.pixelWidth / 2);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Playspace Height");
                ySliderScale = EditorGUILayout.Slider(ySliderScale, 0, Camera.main.pixelHeight / 2);
                EditorGUILayout.EndHorizontal();

                botLeft.center = ScaleGameToScreen(new Vector2(xSliderScale, ySliderScale));
                topRight.center = ScaleGameToScreen(new Vector2(Camera.main.pixelWidth - xSliderScale, Camera.main.pixelHeight - ySliderScale));
            }

            //UNDO SLIDER
            /*EditorGUI.BeginChangeCheck();
            var myFloatForUndoCheck = scale;
            myFloatForUndoCheck = EditorGUI.Slider(new Rect(5, 5, 150, 20), myFloatForUndoCheck, 10, 200);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(this, "Changed Slider Value");
                scale = myFloatForUndoCheck;
            }*/
            #endregion

            #region LAST STEP : Apply Any Changes in Editor Window to Player Values
            // assign the virtual screen values to a new Vector4    
            Vector4Bounds calcBounds = new Vector4Bounds();
            calcBounds.leftX = botLeft.center.x; //coords in pixels
            calcBounds.leftY = botLeft.center.y;
            calcBounds.rightX = topRight.center.x;
            calcBounds.rightY = topRight.center.y;

            // convert the virtual screen values to Game Screen Values
            appliedBounds = ScaleScreenToGame(calcBounds);
            movementBehavior.playspace.leftX = appliedBounds.leftX;
            movementBehavior.playspace.leftY = appliedBounds.leftY;
            movementBehavior.playspace.rightX = appliedBounds.rightX;
            movementBehavior.playspace.rightY = appliedBounds.rightY;
            #endregion

            #region Export the Playspace as a new Preset
            if (GUILayout.Button(new GUIContent("Save this preset", "Export these Playspace settings into a ScriptableObject format")))
            {
                PlayspaceScriptableObject newSaveData = CreateInstance<PlayspaceScriptableObject>();
                newSaveData.playspaceBounds = appliedBounds;
                newSaveData.tweenTransition = TweenManager.tweenFunctions[(int)selectedTween];
                AssetDatabase.CreateAsset(newSaveData, "Assets/Playspace Data/NewData.asset");
                EditorUtility.SetDirty(newSaveData);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }

            EditorGUILayout.BeginHorizontal();
            // Draw a dropdown menu of transitions that the user can select from
            selectedTween = (TweenName)EditorGUILayout.EnumPopup(selectedTween);
            
            // Draw the "Open Visualizer" button on the same line
            if (GUILayout.Button(new GUIContent("Open Transition Visualizer", "Open a window to visualise ohw the different transitions behave"))) 
                TransitionCurveViewerWindow.Init();
            EditorGUILayout.EndHorizontal();

            if (GUILayoutUtility.GetLastRect().position.y > 0)
                positionOfBottoms = GUILayoutUtility.GetLastRect().position.y;
            #endregion
        }

        Repaint();
    }

    #region Scaling Methods
    // used to scale an object's position in the Virtual Screen to the Player's Viewport
    private Vector4Bounds ScaleScreenToGame(Vector4Bounds screenPosition)
    {
        double leftX = CustomScaler.Scale(screenPosition.leftX, windowSize.leftX, windowSize.rightX, 0, Camera.main.pixelWidth);
        double leftY = CustomScaler.Scale(screenPosition.leftY, windowSize.leftY, windowSize.rightY, 0, Camera.main.pixelHeight);
        double rightX = CustomScaler.Scale(screenPosition.rightX, windowSize.leftX, windowSize.rightX, 0, Camera.main.pixelWidth);
        double rightY = CustomScaler.Scale(screenPosition.rightY, windowSize.leftY, windowSize.rightY, 0, Camera.main.pixelHeight);

        return new Vector4Bounds((float)leftX, (float)leftY, (float)rightX, (float)rightY);
    }

    // used to scale an object's position in  the Player's Viewport to the Virtual Screen
    private Vector2 ScaleGameToScreen(Vector2 screenPos)
    {
        double xPos = CustomScaler.Scale(screenPos.x, 0, Camera.main.pixelWidth, windowSize.leftX, windowSize.rightX);
        double yPos = CustomScaler.Scale(screenPos.y, 0, Camera.main.pixelHeight, windowSize.leftY, windowSize.rightY);

        return new Vector2((float)xPos, (float)yPos);
    }
    #endregion
}