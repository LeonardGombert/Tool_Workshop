using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TransitionCurveViewerWindow : EditorWindow
{
    float time;
    Vector2 change;
    Vector2 startValue = new Vector2(0, 0);
    Vector2 targetValue = new Vector2(150, 150);
    float tweenDuration = 200f;

    List<Rect> myRects = new List<Rect>();
    List<Vector2> myRectPositions = new List<Vector2>();

    Rect linearRect = new Rect(0, 25, 10, 10);
    Rect easeInRect = new Rect(250, 25, 10, 10);
    Rect easeInQuad = new Rect(500, 25, 10, 10);
    Rect EaseInOutQuad = new Rect(750, 25, 10, 10);
    Rect easeInOutQuint = new Rect(1000, 25, 10, 10);
    Rect easeInOutSine = new Rect(1250, 25, 10, 10);

    public Vector2 vectorScaling;

    bool visualizing;

    [MenuItem("Window/Curve Animation Window")]
    public static void Init()
    {
        TransitionCurveViewerWindow window = GetWindow<TransitionCurveViewerWindow>();
        window.Show();
    }

    private void OnEnable()
    {
        myRects.Add(linearRect);
        myRects.Add(easeInRect);
        myRects.Add(easeInQuad);
        myRects.Add(EaseInOutQuad);
        myRects.Add(easeInOutQuint);
        myRects.Add(easeInOutSine);
    }

    private void OnGUI()
    {
        time += Time.deltaTime;

        #region Draw the Rects moving over time
        for (int i = 0; i < myRects.Count; i++)
        {
            // set the button color to black to match the asthetic
            var oldColor = GUI.backgroundColor;
            GUI.backgroundColor = Color.black;
            if (GUI.Button(new Rect(myRects[i].position.x, myRects[i].position.y, 205, 205), ""))
            {
                // if you're already visualizing something, throw the user an error message
                if (visualizing == true) EditorUtility.DisplayDialog
                        ("Warning", "Try stopping the current visualization before trying out a new one. You can also take the time to change some parameters around first :)", "Ok");

                else
                {
                    visualizing = true;
                    // send the info to the playspace window
                    PlaySpaceEditorWindow.InitForVisualization(this, TweenManager.tweenFunctions[i]);
                }
            }
            GUI.backgroundColor = oldColor;

            startValue = myRects[i].position;
            targetValue = myRects[i].position + new Vector2(200, 200);

            Rect currentRect = new Rect(Vector2.zero, new Vector2(10, 10));
            currentRect.center = MoveValue(TweenManager.tweenFunctions[i]);
            EditorGUI.DrawRect(currentRect, Color.green);
            myRectPositions.Add(currentRect.center);
        }

        var oldColor2 = GUI.backgroundColor;
        GUI.backgroundColor = Color.red;
        if (GUILayout.Button(new GUIContent("Reset Visualization Counter")))
        {
            time = 0;
            PlaySpaceEditorWindow.StopVisualizing();
            visualizing = false;
        }
        GUI.backgroundColor = oldColor2;
        #endregion

        // render a line that follows the curve
        foreach (Vector2 position in myRectPositions)
            EditorGUI.DrawRect(new Rect(position, new Vector2(2, 2)), Color.green);

        Repaint();
    }

    #region Tween Move Function
    // Move the rect along the curve
    Vector2 MoveValue(TweenManager.TweenFunction type)
    {
        Vector2 returnVector = new Vector2();

        if (time <= tweenDuration)
        {
            change = targetValue - startValue;
            returnVector.x = type(time, startValue.x, change.x, tweenDuration);
            returnVector.y = TweenManager.LinearTween(time, startValue.y, change.y, tweenDuration); // leave this as a linear tween to get a nicer visualization
            return returnVector;
        }

        else
        {
            time = 0f;
            return targetValue;
        }
    }

    private void OnDisable()
    {
        // stop sending info to the playspace manager
    }
    #endregion
}
