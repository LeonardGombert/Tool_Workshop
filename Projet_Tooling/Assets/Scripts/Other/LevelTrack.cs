using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelTrack
{
    public List<Vector2> obstacleRectCoords = new List<Vector2>();
    public List<Vector2> playspaceRectCoords = new List<Vector2>();
    public List<PlayspaceValue> playspaceValues = new List<PlayspaceValue>();
        
    public LevelTrack(List<Vector2> obstacles = default, List<Vector2> playspaces = default, List<PlayspaceValue> playspaceValues = default)
    {
        this.obstacleRectCoords = obstacles;
        this.playspaceRectCoords = playspaces;
        this.playspaceValues = playspaceValues;
    }
}
