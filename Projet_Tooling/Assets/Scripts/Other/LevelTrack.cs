using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayspaceData", menuName = "ScriptableObjects/PlayspaceScriptableObject", order = 2)]
public class LevelTrack : ScriptableObject
{
    public List<Vector2> softObstacleRectCoords = new List<Vector2>();
    public List<Vector2> hardObstacleRectCoords = new List<Vector2>();
    public List<Vector2> playspaceRectCoords = new List<Vector2>();
    public List<PlayspaceScriptableObject> playspaceValues = new List<PlayspaceScriptableObject>();
        
    public LevelTrack(List<Vector2> softObstacles = default, List<Vector2> hardObstacles = default, List<Vector2> playspaces = default, List<PlayspaceScriptableObject> playspaceValues = default)
    {
        this.softObstacleRectCoords = softObstacles;
        this.hardObstacleRectCoords = hardObstacles;
        this.playspaceRectCoords = playspaces;
        this.playspaceValues = playspaceValues;
    }
}
