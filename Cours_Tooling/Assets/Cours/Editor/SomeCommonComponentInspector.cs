using UnityEditor;
using UnityEngine;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using UnityEditorInternal;

[CustomEditor(typeof(SomeCommonComponent))]
public class SomeCommonComponentInspector : Editor
{
    SerializedProperty myProperty, forkProperty;
    ReorderableList rList;

    #region Morning
    /*
    private void OnEnable()
    {
        myProperty = serializedObject.GetIterator();
        myProperty.NextVisible(true);

        while (myProperty.NextVisible(false))
        {
            Debug.Log(myProperty.name);
            Debug.Log(myProperty.displayName.Length);
            Debug.Log(myProperty.type);

            if (myProperty.type == "Color") forkProperty = myProperty.Copy();
        }

        myProperty.Reset();
    }

    public override void OnInspectorGUI()
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();

        base.OnInspectorGUI();

        sw.Stop();
        EditorGUILayout.LabelField(sw.ElapsedTicks.ToString() + " ticks");
        EditorGUILayout.LabelField(sw.ElapsedMilliseconds.ToString() + " milliseconds");
    }*/
    #endregion

    private void OnEnable()
    {
        
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        rList.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }
}
