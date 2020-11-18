using UnityEngine;
using UnityEngine.Rendering;

[VolumeComponentMenu("Gameplay/Object Color")]
public class ObjectColorVolumeComponent : VolumeComponent
{
    public ColorParameter color = new ColorParameter(Color.white, false, true, true);
}