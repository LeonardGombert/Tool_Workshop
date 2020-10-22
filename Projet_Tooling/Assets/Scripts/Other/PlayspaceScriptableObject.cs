using UnityEngine;

[CreateAssetMenu(fileName = "PlayspaceData", menuName = "ScriptableObjects/PlayspaceScriptableObject", order = 1)]
public class PlayspaceScriptableObject : ScriptableObject
{
    public int playspaceWidth;
    public int playspaceHeight;
}
