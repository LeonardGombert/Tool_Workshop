using UnityEngine;
using UnityEngine.Rendering;

public class ObjectGravityReader : MonoBehaviour
{
    public LayerMask layerMask = -1;
    Rigidbody rdb;

    private void Start()
    {
        rdb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        VolumeManager.instance.Update(transform, layerMask);
        var gravityComponent = VolumeManager.instance.stack.GetComponent<ObjectGravityVolumeComponent>();
        var gravityOverride = gravityComponent.gravityActive.value;

        if (gravityOverride)
        {
            rdb.useGravity = false;
            rdb.AddForce(gravityComponent.gravity.value, ForceMode.Acceleration);
        }
    }
}