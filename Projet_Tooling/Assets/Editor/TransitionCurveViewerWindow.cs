using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TransitionCurveViewerWindow : EditorWindow
{
    float time;
    Vector2 change;
    Vector2 startValue = new Vector2(0, 0);
    Vector2 targetValue = new Vector2(150, 150);
    float tweenDuration = 50f;

    List<Rect> myRects = new List<Rect>();

    Rect linearRect = new Rect(0, 0, 10, 10);
    Rect easeInRect = new Rect(200, 0, 10, 10);
    Rect easeInQuad = new Rect(400, 0, 10, 10);
    Rect EaseInOutQuad = new Rect(600, 0, 10, 10);
    Rect easeInOutQuint = new Rect(800, 0, 10, 10);
    Rect easeInOutSine = new Rect(1000, 0, 10, 10);

    TweenManager.TweenFunction tweenFunction = default;

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

        for (int i = 0; i < myRects.Count; i++)
        {
            EditorGUI.DrawRect(new Rect(myRects[i].position.x, myRects[i].position.y, 155, 155), Color.black);
            startValue = myRects[i].position;
            targetValue = myRects[i].position + new Vector2(150, 150);
            Rect currentRect = new Rect(Vector2.zero, new Vector2(10, 10));
            currentRect.center = MoveValue(myRects[i].position, TweenManager.tweenFunctions[i]);
            EditorGUI.DrawRect(currentRect, Color.green);
        }

        Repaint();
    }

    //example function
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
            linearRect.position = new Vector2(0, 0);
            time = 0f;
            return targetValue;
        }
    }
}
