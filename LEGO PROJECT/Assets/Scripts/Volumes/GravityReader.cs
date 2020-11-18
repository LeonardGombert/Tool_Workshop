using UnityEngine;
using UnityEngine.Rendering;

public class GravityReader : MonoBehaviour
{
    public LayerMask layerMask = -1;

    private void Update()
    {
        VolumeManager.instance.Update(transform, layerMask);
        Physics.gravity = new Vector3(0, VolumeManager.instance.stack.GetComponent<GravityVolumeComponent>().worldGravity.value, 0);
        Debug.Log("World Gravity is " + Physics.gravity);
    }
}
