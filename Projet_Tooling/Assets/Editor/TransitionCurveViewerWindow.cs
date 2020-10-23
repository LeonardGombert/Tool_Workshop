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

    Rect linearRect = new Rect(0, 0, 10, 10);
    Rect easeInRect = new Rect(250, 0, 10, 10);
    Rect easeInQuad = new Rect(500, 0, 10, 10);
    Rect EaseInOutQuad = new Rect(750, 0, 10, 10);
    Rect easeInOutQuint = new Rect(1000, 0, 10, 10);
    Rect easeInOutSine = new Rect(1250, 0, 10, 10);

    TweenManager.TweenFunction tweenFunction = default; // use for enum ? 

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
            EditorGUI.DrawRect(new Rect(myRects[i].position.x, myRects[i].position.y, 205, 205), Color.black);
            Rect currentRect = new Rect(Vector2.zero, new Vector2(10, 10));

            startValue = myRects[i].position;
            targetValue = myRects[i].position + new Vector2(200, 200);

            currentRect.center = MoveValue(myRects[i].position, TweenManager.tweenFunctions[i]);
            EditorGUI.DrawRect(currentRect, Color.green);
            myRectPositions.Add(currentRect.center);
        }
        #endregion

        foreach (Vector2 position in myRectPositions) EditorGUI.DrawRect(new Rect(position, new Vector2(2, 2)), Color.green);

        Repaint();
    }

    #region Tween Move Function
    // Move the rect along the curve
    Vector2 MoveValue(Vector2 exampleValue, TweenManager.TweenFunction type)
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
    #endregion
}
