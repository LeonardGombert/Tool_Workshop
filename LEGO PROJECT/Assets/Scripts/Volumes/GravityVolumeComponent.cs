using UnityEngine;
using UnityEngine.Rendering;

[VolumeComponentMenu("Gameplay/Gravity")]
public class GravityVolumeComponent : VolumeComponent
{
    public FloatParameter worldGravity = new FloatParameter(Physics.gravity.y);
}
