using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelTrackEditorWIndow : EditorWindow
{

    List<Rect> myRects = new List<Rect>();
    Vector2 screenSize;

    [MenuItem("Window/Level Track Editor Window %k")]
    public static void Init()
    {
        LevelTrackEditorWIndow window = GetWindow<LevelTrackEditorWIndow>();
        window.Show();
    }

    [MenuItem("Window/Level Track Editor Window %k")]
    public static void InitWithContent()
    {
        LevelTrackEditorWIndow window = GetWindow<LevelTrackEditorWIndow>();
        window.Show();
    }

    private void OnEnable()
    {
    }

    private void OnGUI()
    {
        screenSize = new Vector2(Camera.main.pixelWidth/2, Camera.main.pixelHeight/2);

        #region Draw Track visuals

        Rect screen = new Rect(0, 0, screenSize.x, screenSize.y);
        screen.center = new Vector2(position.width / 2, position.height / 2);
        EditorGUI.DrawRect(screen, Color.black);

        Event cur = Event.current;

        int rectSize = 20;
        float yes = position.height * 0.5f;
        float yes2 = position.width * 0.5f;

        for (int x = 50; x < screenSize.x; x += rectSize + 2)
        {
            for (int y = (int)yes; y < position.height / 2; y += rectSize + 2)
            {
                Rect newRect = new Rect(x, y, rectSize, rectSize);
                newRect.center = new Vector2(x, y);

                EditorGUI.DrawRect(newRect, Color.white);

                if (newRect.Contains(cur.mousePosition)) EditorGUI.DrawRect(newRect, Color.red);
                else EditorGUI.DrawRect(newRect, Color.white);

            }
        }

        Repaint();

        // get screen size -> corridor height
        // have the amount of rects height as a parameter
        // draw a straight corridor-shaped grid;
        // make each Rect clickable
        // draw the player (to scale)
        #endregion
    }
}