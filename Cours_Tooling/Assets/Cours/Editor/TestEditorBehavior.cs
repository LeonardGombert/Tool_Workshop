using UnityEngine;
using UnityEditor;

public class TestEditorBehavior
{
    // can be called using the shortcut CTRL + W
    [MenuItem("Tools/Hello World %w")]
    public static void HelloWorld()
    {
        Debug.Log("Finding References...");

        MyHeavyGameplayScript manager = Object.FindObjectOfType<MyHeavyGameplayScript>();

        Undo.RecordObject(manager, "Just set the references");

        manager.audioListener = Object.FindObjectOfType<AudioListener>();
        manager.myCamera = Object.FindObjectOfType<Camera>();
        manager.selfTransform = manager.transform;
        manager.camTransform = manager.myCamera.transform;
    }
}