using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CubeBehavior))]        
public class CubeBehaviorEditor : Editor
{ 
    CubeBehavior cubeBehavior;
    Transform cubeTransform;

    public void OnEnable()
    {
        cubeBehavior = target as CubeBehavior;
        cubeTransform = cubeBehavior.transform;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.LabelField("Ceci s'affiche dans l'insector");
    }

    public void OnSceneGUI()
    {
        Handles.BeginGUI();
        EditorGUILayout.LabelField("Ceci s'affiche dans la scene");
        Handles.EndGUI();

        Vector3 pos = cubeTransform.position;
        Quaternion rot = cubeTransform.rotation;
        float size = 1f;
        Vector3 snap = Vector3.zero;

        if (cubeBehavior.outputVector.Length > 0)
        {
            for (int i = 0; i < cubeBehavior.outputVector.Length; i++)
            {
                Handles.DrawLine(pos + cubeBehavior.outputVector[i], pos + cubeBehavior.outputVector[(i + 1) % cubeBehavior.outputVector.Length]);
                //cubeBehavior.outputVector[i] = Handles.FreeMoveHandle(pos + cubeBehavior.outputVector[i], rot, size, snap, Handles.SphereHandleCap) - pos;
                cubeBehavior.outputVector[i] = Handles.PositionHandle(pos + cubeBehavior.outputVector[i], rot) - pos;
            }
        }

        //cubeTransform.position = Handles.FreeMoveHandle(pos, rot, size, snap, Handles.CubeHandleCap);

        EditorUtility.SetDirty(cubeTransform);
    }
}