using UnityEngine;
using UnityEngine.Rendering;

[ExecuteAlways]
[RequireComponent(typeof(Renderer))]
public class ObjectColorVolumeReader : MonoBehaviour
{
    Renderer renderer;
    public string colorPropertyName = "_BaseColor";
    public LayerMask layerMask = -1;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        VolumeManager.instance.Update(transform, layerMask);
        renderer.material.SetColor(colorPropertyName, VolumeManager.instance.stack.GetComponent<ObjectColorVolumeComponent>().color.value);
    }
}