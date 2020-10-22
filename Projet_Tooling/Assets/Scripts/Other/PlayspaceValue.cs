using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PlayspaceValue
{
    public Vector2 playspaceTunnelCoords;
    public int playspaceWidth;
    public int playspaceHeight;

    public PlayspaceValue(Vector2 coords, int playspaceWidth, int playspaceHeight)
    {
        this.playspaceTunnelCoords = coords;
        this.playspaceWidth = playspaceWidth;
        this.playspaceHeight = playspaceHeight;
    }
}
