using Gameplay.Player;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelTrackEditorWIndow : EditorWindow
{
    List<int> obstacleRectsIndexes = new List<int>();
    List<Vector2> obstacleRectCoords = new List<Vector2>();
    List<Vector2> convertedRectCoords = new List<Vector2>();
    List<GameObject> obstaclesList = new List<GameObject>();

    List<int> playSpaceRectsIndexes = new List<int>();

    // grid paramaters
    int columnSize = 7;
    int rowSize = 50;
    int rectSize = 20;

    int gridDepthCoord;
    int gridHeightCoord;

    // where does the grid start drawing ? 
    int startingHeightCoord = 50; //(int)(position.height * 0.5f);
    int startingDepthCoord = 50;

    // size of 1 Chunk (determines zdepth of the window in world position)
    int chunkSize = 300;

    enum Brush
    {
        playSpaceTransition,
        obstacle,
    }

    Brush myEnum;

    [MenuItem("Window/Level Track Editor Window %k")]
    public static void InitWithContent()
    {
        LevelTrackEditorWIndow window = GetWindow<LevelTrackEditorWIndow>();
        window.Show();
    }

    private void OnGUI()
    {
        #region Track Player Inputs
        Event cur = Event.current;
        myEnum = (Brush)EditorGUILayout.EnumPopup(myEnum);
        //if (cur.type == EventType.ScrollWheel && myEnum>0 && myEnum < Brush.obstacle + 1) myEnum += (int)Mathf.Sign(cur.delta.y);
        #endregion

        #region Draw All Obstacles in Scene
        if (GUILayout.Button("Place Cubes"))
        {
            foreach (Vector2 item in obstacleRectCoords)
            {
                Vector3 convertedCoords = ScaleToWorldSpace(item);
                GameObject newObstacle = Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube), convertedCoords, Quaternion.identity, GameObject.Find("MOVING OBJECTS").transform);
                obstaclesList.Add(newObstacle);
            }
        }

        if (GUILayout.Button("Delete Cubes"))
        {
            foreach (GameObject obstacle in obstaclesList)
            {
                DestroyImmediate(obstacle, false);
            }
        }
        #endregion

        #region Calculate and Draw Track Grid
        gridDepthCoord = startingDepthCoord + (rectSize + 2) * rowSize;
        gridHeightCoord = startingHeightCoord + (rectSize + 2) * columnSize;

        // draw a grid of screen size length and game screen size height
        for (int i = 1, z = startingDepthCoord; z < gridDepthCoord; z += rectSize + 2)
        {
            for (int y = startingHeightCoord; y < gridHeightCoord; y += rectSize + 2, i++)
            {
                Rect newRect = new Rect(z, y, rectSize, rectSize);
                newRect.center = new Vector2(z, y);

                //if the tile to be drawn is a playspace change, draw it as green
                if (playSpaceRectsIndexes.Contains(i)) EditorGUI.DrawRect(newRect, Color.green);
                // if the tile to be drawn is in an obstacle, draw it as green
                else if (obstacleRectsIndexes.Contains(i)) EditorGUI.DrawRect(newRect, Color.red);
                // othrewise, draw it as an empty tile
                else EditorGUI.DrawRect(newRect, Color.black);
                /*if (playSpaceRectsIndexes.Contains(i)) GUI.Button(newRect, i.ToString());
                else if (obstacleRectsIndexes.Contains(i)) GUI.Button(newRect, i.ToString());
                else GUI.Button(newRect, i.ToString()); */

                if (myEnum == Brush.playSpaceTransition)
                {
                    // if the player right clicks on an already green tile -> delete
                    if (newRect.Contains(cur.mousePosition) && cur.type == EventType.MouseDown && playSpaceRectsIndexes.Contains(i) && cur.button == 1) 
                        playSpaceRectsIndexes.Remove(i);

                    // if the player clicks on a tile that isn't green -> turn it green
                    else if (newRect.Contains(cur.mousePosition) && cur.type == EventType.MouseDown && !playSpaceRectsIndexes.Contains(i))
                    {
                        // if the player is clicking on a tile at the bottom of the screen
                        if ((i % columnSize) == 0) 
                            for (int j = i; j >= i - (columnSize - 1); j--) 
                                playSpaceRectsIndexes.Add(j);

                        // else, iterate through the tiles until you find the bottom one, and draw them back to the top
                        else
                            for (int l = i; l < i + columnSize; l++) 
                                if (l % columnSize == 0) 
                                    for (int j = l; j >= l - (columnSize - 1); j--) 
                                        playSpaceRectsIndexes.Add(j);
                    }
                }

                // if the player right clicks on an already green tile -> delete
                if (myEnum == Brush.obstacle)
                {
                    if (newRect.Contains(cur.mousePosition) && cur.type == EventType.MouseDown && obstacleRectsIndexes.Contains(i) && cur.button == 1)
                    {
                        obstacleRectsIndexes.Remove(i);
                        obstacleRectCoords.Remove(new Vector2(z, y));
                    }

                    // if the player clicks on a tile that isn't green -> turn it green
                    else if (newRect.Contains(cur.mousePosition) && cur.type == EventType.MouseDown && !obstacleRectsIndexes.Contains(i))
                    {
                        obstacleRectsIndexes.Add(i);
                        obstacleRectCoords.Add(new Vector2(z, y));
                    }
                }
            }
        }
        #endregion

        #region Convert Obstacle Coordinates to Screen Space
        // convert grid height to player screen size
        // get top of cameraPos
        // scale gridHeight to topOfCamPos
        //spawn cube at gridHeight

        // convert grid length to Z player z depth position
        foreach (Vector2 obstacleCoords in obstacleRectCoords)
        {
            //Debug.Log(convertedObstacleCoords);
        }
        #endregion

        Repaint();
    }

    #region Scaling Methods
    // Convert Vector2 "tunnel space" values to Vector3 World Coordinates
    private Vector3 ScaleToWorldSpace(Vector2 screenCoords)
    {
        Vector3 convertedObstacleCoords;
        double depth = CustomScaler.Scale(screenCoords.x, startingDepthCoord, gridDepthCoord, 0, chunkSize);
        double height = CustomScaler.Scale(screenCoords.y, startingHeightCoord, gridHeightCoord, Camera.main.fieldOfView / 10, -Camera.main.fieldOfView / 10);
        return convertedObstacleCoords = new Vector3(0, (float)height, (float)depth);
    }
    #endregion
}