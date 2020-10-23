using UnityEditor;
using UnityEngine;

public class TransitionCurveViewerWindow : EditorWindow
{
    float time;
    Vector2 change;
    Vector2 startValue = new Vector2(0, 0);
    Vector2 targetValue = new Vector2(200, 200);
    float tweenDuration = 40f;

    Rect linearRect = new Rect(0, 0, 10, 10);
    Rect easeInRect = new Rect(0, 0, 10, 10);
    Rect easeInQuad = new Rect(0, 0, 10, 10);
    Rect EaseInOutQuad = new Rect(0, 0, 10, 10);
    Rect easeInOutQuint = new Rect(0, 0, 10, 10);
    Rect easeInOutSine = new Rect(0, 0, 10, 10);

    //static TweenManager.TweenFunction tweenfunction = TweenManager.GetTween();;
    
    static TweenManager.TweenFunction 

    [MenuItem("Window/Curve Animation Window")]
    public static void Init()
    {
        TransitionCurveViewerWindow window = GetWindow<TransitionCurveViewerWindow>();
        window.Show();
    }

    private void OnGUI()
    {
        time += Time.deltaTime;
        foreach (var item in collection)
            EditorGUI.DrawRect(new Rect(MoveValue(linearRect.position), new Vector2(10, 10)), Color.green);
        {

        }
        Repaint();
    }

    //example function
    Vector2 MoveValue(Vector2 exampleValue)
    {
        Vector2 returnVector = new Vector2();

        if (time <= tweenDuration)
        {
            change = targetValue - startValue;
            returnVector.x = TweenManager.LinearTween(time, startValue.x, change.x, tweenDuration);
            returnVector.y = TweenManager.LinearTween(time, startValue.y, change.y, tweenDuration);
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
