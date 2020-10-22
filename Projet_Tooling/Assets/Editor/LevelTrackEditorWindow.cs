using Microsoft.Win32;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelTrackEditorWIndow : EditorWindow
{

    List<int> obstacleRectsIndexes = new List<int>();
    List<int> playSpaceRectsIndexes = new List<int>();

    Vector2 screenSize;

    // grid paramaters
    int gridHeight = 6;
    int rectSize = 20;

    enum Brush
    {
        playSpaceTransition,
        obstacle,
    }

    Brush myEnum;


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

    private void OnGUI()
    {
        screenSize = new Vector2(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2);
        Event cur = Event.current;

        #region Track Player Inputs
        myEnum = (Brush)EditorGUILayout.EnumPopup(myEnum);

        //if (cur.type == EventType.ScrollWheel && myEnum>0 && myEnum < Brush.obstacle + 1) myEnum += (int)Mathf.Sign(cur.delta.y);
        #endregion

        #region Draw Track visuals
        int startingHeight = (int)(position.height * 0.5f);
        int startingWidth = 50;

        // draw a grid of screen size length and game screen size height
        for (int i = 1, x = startingWidth; x < screenSize.x; x += rectSize + 2)
        {
            for (int y = startingHeight; y < startingHeight + rectSize * gridHeight; y += rectSize + 2, i++)
            {
                Rect newRect = new Rect(x, y, rectSize, rectSize);
                newRect.center = new Vector2(x, y);

                //if the tile to be drawn is a playspace change, draw it as green
                if (playSpaceRectsIndexes.Contains(i)) EditorGUI.DrawRect(newRect, Color.green);
                // if the tile to be drawn is in an obstacle, draw it as green
                else if (obstacleRectsIndexes.Contains(i)) EditorGUI.DrawRect(newRect, Color.red);
                // othrewise, draw it as an empty tile
                else EditorGUI.DrawRect(newRect, Color.black);/*
                if (playSpaceRectsIndexes.Contains(i)) GUI.Button(newRect, i.ToString());
                else if (obstacleRectsIndexes.Contains(i)) GUI.Button(newRect, i.ToString());
                else GUI.Button(newRect, i.ToString());*/

                if(myEnum == Brush.playSpaceTransition)
                {
                    // if the player is clicking on a tile at the bottom of the screen
                    if((i % 6) == 0)
                    {
                        // if the player right clicks on an already green tile -> delete
                        if (newRect.Contains(cur.mousePosition) && cur.type == EventType.MouseDown && playSpaceRectsIndexes.Contains(i) && cur.button == 1) playSpaceRectsIndexes.Remove(i);
                        // if the player clicks on a tile that isn't green -> turn it green
                        else if (newRect.Contains(cur.mousePosition) && cur.type == EventType.MouseDown && !playSpaceRectsIndexes.Contains(i))
                        {
                            for (int j = i; j >= i - (gridHeight - 1); j--)
                            {
                                Debug.Log(j);
                                Debug.Log(i - gridHeight);
                                playSpaceRectsIndexes.Add(j);
                            }
                        }
                    }
                }

                // if the player right clicks on an already green tile -> delete
                if (myEnum == Brush.obstacle)
                {
                    if (newRect.Contains(cur.mousePosition) && cur.type == EventType.MouseDown && obstacleRectsIndexes.Contains(i) && cur.button == 1) obstacleRectsIndexes.Remove(i);
                    // if the player clicks on a tile that isn't green -> turn it green
                    else if (newRect.Contains(cur.mousePosition) && cur.type == EventType.MouseDown && !obstacleRectsIndexes.Contains(i)) obstacleRectsIndexes.Add(i);
                }
            }
        }
        #endregion

        Repaint();
        // get screen size -> corridor height
        // have the amount of rects height as a parameter
        // draw a straight corridor-shaped grid;
        // make each Rect clickable
        // draw the player (to scale)
    }
}