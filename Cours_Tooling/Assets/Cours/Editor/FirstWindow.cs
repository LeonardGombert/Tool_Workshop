using System.Linq.Expressions;
using UnityEditor;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class FirstWindow : EditorWindow
{
    LevelProfile currentProfile;

    [MenuItem("Window/My First Window %w")]
    public static void Init()
    {
        FirstWindow window = EditorWindow.GetWindow(typeof(FirstWindow)) as FirstWindow;
        window.Show();
    }

    public static void InitWithContent(LevelProfile profile)
    {
        FirstWindow window = EditorWindow.GetWindow(typeof(FirstWindow)) as FirstWindow;
        window.currentProfile = profile;
        window.Show();
    }

    private void OnGUI()
    {
        if(currentProfile == null) EditorGUILayout.LabelField("Is Empty");

        if(currentProfile.levelValues.Length > 0)
        {
            float tileWidth = 50f;
            float tileHeight = 50f;

            //int rowAmount = 2;
            //int columnAmount = 2;

            Event cur = Event.current;

            for (int i = 0; i < currentProfile.levelValues.Length; i++)
            {
                Rect squareRect = new Rect(30 + tileWidth * i, 30 + tileHeight * i, tileWidth, tileHeight);
                EditorGUI.DrawRect(squareRect, Color.green);

                if (squareRect.Contains(cur.mousePosition)) EditorGUI.DrawRect(squareRect, Color.blue);
                else EditorGUI.DrawRect(squareRect, Color.green);

                Repaint();
            }
        }
    }

    public void OldGUI()
    {
        EditorGUI.DrawRect(new Rect(30, 30, 100, 100), Color.green);

        Rect closeButtonRect = new Rect(30, 200, 60, 20);
        if (GUI.Button(closeButtonRect, "Close")) Close();
    }
}
