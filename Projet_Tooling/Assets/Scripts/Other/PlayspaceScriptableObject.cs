using UnityEngine;

[CreateAssetMenu(fileName = "PlayspaceData", menuName = "ScriptableObjects/PlayspaceScriptableObject", order = 1)]
public class PlayspaceScriptableObject : ScriptableObject
{
    public Vector4Bounds playspaceBounds;
}
