using Gameplay.Player;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public class LevelTrackEditorWindow : EditorWindow
{
    // breakable obstacle variables
    List<int> softObstacleRectsIndexes = new List<int>();
    List<Vector2> softObstacleRectCoords = new List<Vector2>();
    List<GameObject> softObstaclesList = new List<GameObject>();
    GameObject softObstaclePrefab;

    // unbreakable obstacle variables
    List<int> hardObstacleRectsIndexes = new List<int>();
    List<Vector2> hardObstacleRectCoords = new List<Vector2>();
    List<GameObject> hardObstaclesList = new List<GameObject>();
    GameObject hardObstaclePrefab;

    // playspace change area variables
    List<int> playSpaceRectsIndexes = new List<int>();
    List<Vector2> playspaceRectCoords = new List<Vector2>();
    List<GameObject> playspaceList = new List<GameObject>();

    List<PlayspaceScriptableObject> playspaceValues = new List<PlayspaceScriptableObject>();

    // grid paramaters
    int tunnelColumns;
    int tunelRows = 50;
    int rectSize = 20;

    int tunnelGridDepth;
    int tunnelGridHeight;

    // where does the grid start drawing ? 
    int startingHeightCoord = 70; //(int)(position.height * 0.5f);
    int startingDepthCoord = 50;

    // size of 1 Chunk (determines zdepth of the window in world position)
    int chunkSize = 300;

    Brush myBrush;

    GameObject playspaceChangePrefab;

    PlayspaceScriptableObject[] playspaceValuesObject = new PlayspaceScriptableObject[0];
    int playspaceValuesArraySize = 0;


    [MenuItem("Window/Level Track Editor Window %k")]
    public static void InitWithContent()
    {
        LevelTrackEditorWindow window = GetWindow<LevelTrackEditorWindow>();
        window.Show();
    }

    private void OnGUI()
    {
        // determine the number of columns
        tunnelColumns = (int)Camera.main.fieldOfView / 10;

        // make it an uneven number (to center on the player's starting position)
        if (tunnelColumns % 2 == 0) tunnelColumns++;
        Event cur = Event.current;

        #region Enum Brush GUI
        EditorGUILayout.BeginHorizontal();
        //EditorGUILayout.LabelField("Current Brush");
        myBrush = (Brush)EditorGUILayout.EnumPopup(myBrush);
        //if (cur.type == EventType.ScrollWheel && myEnum>0 && myEnum < Brush.obstacle + 1) myEnum += (int)Mathf.Sign(cur.delta.y);
        #endregion

        #region PropertyFields GUI
        softObstaclePrefab = EditorGUILayout.ObjectField(new GUIContent("Breakable Obstacle", default, "This type of object can be destroyed by the player"), softObstaclePrefab, typeof(GameObject), true) as GameObject;
        hardObstaclePrefab = EditorGUILayout.ObjectField(new GUIContent("Unbreakable Obstacle", default, "This type of object cannot be destroyed by the player"), hardObstaclePrefab, typeof(GameObject), true) as GameObject;
        playspaceChangePrefab = EditorGUILayout.ObjectField(new GUIContent("Playspace Change", default, "This type of object determines where the player's playspace will change"), playspaceChangePrefab, typeof(GameObject), true) as GameObject;
        #endregion

        #region Button & Label GUI
        // Create a button that allows you to spawn the cubes in the world
        if (GUILayout.Button(new GUIContent("Place Cubes", default, "Generate the Level you've just built in the scene")))
        {
            if (softObstaclePrefab == null || hardObstaclePrefab == null || playspaceChangePrefab == null)
                EditorUtility.DisplayDialog("Creation Error", "You seem to be missing some prefabs. Try assigning them before attempting to generate the level.", "Ok");

            else
            {

                // spawn all breakable obstacles and add them to the list
                foreach (Vector2 item in softObstacleRectCoords)
                {
                    Vector3 convertedCoords = ScaleToWorldSpace(item);
                    GameObject newObstacle =
                        Instantiate(softObstaclePrefab,
                        convertedCoords, Quaternion.identity, GameObject.Find("MOVING OBJECTS").transform);
                    softObstaclesList.Add(newObstacle);
                }

                // spawn all unbreakable obstacles and add them to the list
                foreach (Vector2 item in hardObstacleRectCoords)
                {
                    Vector3 convertedCoords = ScaleToWorldSpace(item);
                    GameObject newObstacle =
                        Instantiate(hardObstaclePrefab,
                        convertedCoords, Quaternion.identity, GameObject.Find("MOVING OBJECTS").transform);
                    hardObstaclesList.Add(newObstacle);
                }

                // spawn all playspace changes and add them to the list
                foreach (Vector2 item in playspaceRectCoords)
                {
                    Vector3 convertedCoords = ScaleToWorldSpace(item);
                    GameObject newPlaySpaceChange =
                        Instantiate(playspaceChangePrefab,
                        convertedCoords, Quaternion.identity, GameObject.Find("MOVING OBJECTS").transform);
                    playspaceList.Add(newPlaySpaceChange);
                }
            }
        }

        // Create a button that allows you to delete the spawned cubes
        if (GUILayout.Button(new GUIContent("Delete Cubes", default, "Delete any objects you've previously generated into the scene")))
        {
            foreach (GameObject obstacle in softObstaclesList) DestroyImmediate(obstacle, false);
            foreach (GameObject obstacle in hardObstaclesList) DestroyImmediate(obstacle, false);
            foreach (GameObject obstacle in playspaceList) DestroyImmediate(obstacle, false);
        }
        EditorGUILayout.EndHorizontal();

        // Create a Button that allows you to save the Track as a scriptable object
        if (GUILayout.Button(new GUIContent("Save This Track", default, "Save the track you've been working on as a ScriptableObject")))
        {
            LevelTrack levelTrack = CreateInstance<LevelTrack>();
            levelTrack.softObstacleRectCoords = softObstacleRectCoords;
            levelTrack.softObstacleRectCoords = hardObstacleRectCoords;
            levelTrack.playspaceRectCoords = playspaceRectCoords;
            levelTrack.playspaceValues = playspaceValues;

            AssetDatabase.CreateAsset(levelTrack, "Assets/Level Tracks/NewTrack.asset");
            EditorUtility.SetDirty(levelTrack);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            EditorUtility.DisplayDialog("Level Saved !", "You've saved your level ! It can be found in Assets/Level Tracks", "Ok");
        }
        #endregion

        #region Calculate and Draw Track Grid
        // calculate edges of the tunnel
        tunnelGridDepth = startingDepthCoord + (rectSize + 2) * tunelRows;
        tunnelGridHeight = startingHeightCoord + (rectSize + 2) * tunnelColumns;

        // draw a grid of screen size length and game screen size height
        for (int i = 1, z = startingDepthCoord; z < tunnelGridDepth; z += rectSize + 2)
        {
            for (int y = startingHeightCoord; y < tunnelGridHeight; y += rectSize + 2, i++)
            {
                Rect newRect = new Rect(z, y, rectSize, rectSize);
                newRect.center = new Vector2(z, y);

                //if the tile to be drawn is a playspace change, draw it as green
                if (playSpaceRectsIndexes.Contains(i)) EditorGUI.DrawRect(newRect, Color.green);
                // if the tile to be drawn is in a breakable obstacle, draw it as yellow
                else if (softObstacleRectsIndexes.Contains(i)) EditorGUI.DrawRect(newRect, Color.yellow);
                // if the tile to be drawn is in an unbreakable obstacle, draw it as green
                else if (hardObstacleRectsIndexes.Contains(i)) EditorGUI.DrawRect(newRect, Color.red);
                // othrewise, draw it as an empty tile
                else EditorGUI.DrawRect(newRect, Color.black);
                /*if (playSpaceRectsIndexes.Contains(i)) GUI.Button(newRect, i.ToString());
                else if (obstacleRectsIndexes.Contains(i)) GUI.Button(newRect, i.ToString());
                else GUI.Button(newRect, i.ToString()); */

                if (myBrush == Brush.playSpaceTransition)
                {
                    // if the player right clicks on an already green tile -> delete
                    if (newRect.Contains(cur.mousePosition) && cur.type == EventType.MouseDown && playSpaceRectsIndexes.Contains(i) && cur.button == 1)
                    {
                        playSpaceRectsIndexes.Remove(i);
                        playspaceRectCoords.Remove(new Vector2(z, y));
                    }

                    // if the player clicks on a tile that isn't green -> turn it green
                    else if (newRect.Contains(cur.mousePosition) && cur.type == EventType.MouseDown && !playSpaceRectsIndexes.Contains(i))
                    {
                        // if the player is clicking on a tile at the bottom of the screen
                        if ((i % tunnelColumns) == 0)
                        {
                            playspaceRectCoords.Add(new Vector2(z, y));

                            playspaceValuesArraySize++;
                            playspaceValuesObject = new PlayspaceScriptableObject[playspaceValuesArraySize];

                            for (int j = i; j >= i - (tunnelColumns - 1); j--)
                                playSpaceRectsIndexes.Add(j);
                        }


                        // else, iterate through the tiles until you find the bottom one, and draw them back to the top
                        else
                            for (int l = i; l < i + tunnelColumns; l++)
                                if (l % tunnelColumns == 0)
                                {
                                    playspaceRectCoords.Add(new Vector2(z, y));

                                    playspaceValuesArraySize++;
                                    playspaceValuesObject = new PlayspaceScriptableObject[playspaceValuesArraySize];

                                    for (int j = l; j >= l - (tunnelColumns - 1); j--)
                                        playSpaceRectsIndexes.Add(j);
                                }
                    }
                }

                // if the player right clicks on an already green tile -> delete
                if (myBrush == Brush.breakableObstacle)
                {
                    if (newRect.Contains(cur.mousePosition) && cur.type == EventType.MouseDown && softObstacleRectsIndexes.Contains(i) && cur.button == 1)
                    {
                        softObstacleRectsIndexes.Remove(i);
                        softObstacleRectCoords.Remove(new Vector2(z, y));
                    }

                    // if the player clicks on a tile that isn't green -> turn it green
                    else if (newRect.Contains(cur.mousePosition) && cur.type == EventType.MouseDown && !softObstacleRectsIndexes.Contains(i))
                    {
                        softObstacleRectsIndexes.Add(i);
                        softObstacleRectCoords.Add(new Vector2(z, y));
                    }
                }

                // if the player right clicks on an already green tile -> delete
                if (myBrush == Brush.unbreakableObstacle)
                {
                    if (newRect.Contains(cur.mousePosition) && cur.type == EventType.MouseDown && hardObstacleRectsIndexes.Contains(i) && cur.button == 1)
                    {
                        hardObstacleRectsIndexes.Remove(i);
                        hardObstacleRectCoords.Remove(new Vector2(z, y));
                    }

                    // if the player clicks on a tile that isn't green -> turn it green
                    else if (newRect.Contains(cur.mousePosition) && cur.type == EventType.MouseDown && !hardObstacleRectsIndexes.Contains(i))
                    {
                        hardObstacleRectsIndexes.Add(i);
                        hardObstacleRectCoords.Add(new Vector2(z, y));
                    }
                }
            }
        }
        #endregion

        #region Draw Playspace Data property field
        for (int i = 0; i < playspaceRectCoords.Count; i++)
        {
            if (playspaceRectCoords.Count > 0)
            {
                Rect propertyRect = new Rect(0, 0, 100, 50);
                propertyRect.center = new Vector2(playspaceRectCoords[i].x, playspaceRectCoords[i].y + 45);
                playspaceValuesObject[i] = (PlayspaceScriptableObject)EditorGUI.ObjectField(propertyRect, playspaceValuesObject[i], typeof(PlayspaceScriptableObject), true);

                if (playspaceValuesObject[i] != null)
                {
                    PlayspaceScriptableObject newObj = new PlayspaceScriptableObject();
                    newObj.playspaceBounds = playspaceValuesObject[i].playspaceBounds;
                    if (!playspaceValues.Contains(newObj)) playspaceValues.Add(newObj);
                }
            }
        }
        #endregion

        Repaint();
    }

    #region Scaling Methods
    // Convert Vector2 "tunnel space" values to Vector3 World Coordinates
    private Vector3 ScaleToWorldSpace(Vector2 screenCoords)
    {
        Vector3 convertedObstacleCoords;
        double depth = CustomScaler.Scale(screenCoords.x, startingDepthCoord, tunnelGridDepth, 0, chunkSize);
        double height = CustomScaler.Scale(screenCoords.y, startingHeightCoord, tunnelGridHeight, Camera.main.fieldOfView / 10, -Camera.main.fieldOfView / 10);
        return convertedObstacleCoords = new Vector3(0, (float)height, (float)depth);
    }
    #endregion
}