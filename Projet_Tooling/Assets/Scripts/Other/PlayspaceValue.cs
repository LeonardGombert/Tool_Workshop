using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlayspaceValue
{
    Vector2 playspaceTunnelCoords;
    Object playspaceTunnelValues;

    public PlayspaceValue(Vector2 coords, Object values)
    {
        this.playspaceTunnelCoords = coords;
        this.playspaceTunnelValues = values;
    }
}
