using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(MyHeavyGameplayScript))]
[CanEditMultipleObjects]
public class MyHeavyGameplayScriptEditor : Editor
{
    MyHeavyGameplayScript myTargetScript;

    bool foldoutState;

    private void OnEnable()
    {
        myTargetScript = target as MyHeavyGameplayScript;
        Undo.undoRedoPerformed += RecalculateStuffAfterundo;
    }

    public void RecalculateStuffAfterundo()
    {

    }

    public override void OnInspectorGUI()
    {
        GUILayout.BeginVertical();

        EditorGUILayout.LabelField(EditorGUIUtility.labelWidth.ToString());

        int oldIndent = EditorGUI.indentLevel;
        EditorGUI.indentLevel += 2;

        #region EditorGUILayout
        myTargetScript.myColor = EditorGUILayout.ColorField("My Color", myTargetScript.myColor);

        EditorGUILayout.HelpBox("This is a HelpBox", MessageType.Info);

        string[] options = new string[] { "option 1 ", "option 2", "option 3", "option 4"};
        myTargetScript.exampleEnum = (WrapMode)EditorGUILayout.EnumPopup(myTargetScript.exampleEnum);

        // listen for changes on the ObjectField
        EditorGUI.BeginChangeCheck();
        Transform transformResult = EditorGUILayout.ObjectField("My Transform", myTargetScript.selfTransform, typeof(Transform), true) as Transform;
        bool somethingChanged = EditorGUI.EndChangeCheck();
        
        if (somethingChanged)
        {
            Debug.Log("Something changed");
            Undo.RecordObject(myTargetScript, "Assigned a Transform");
            myTargetScript.selfTransform = transformResult;
        }
        #endregion

        EditorGUI.indentLevel -= 2;

        #region buttons
        Color defColor = GUI.color;
        GUI.color = Color.green;

        GUILayout.BeginHorizontal();
        if(GUILayout.Button("AutoSet References")) AutoSetReferences();
        if(GUILayout.Button("Clear References")) SetReferencesToNull();
        GUILayout.EndHorizontal();

        GUI.color = defColor;
        #endregion

        myTargetScript.foldoutState = EditorGUILayout.Foldout(myTargetScript.foldoutState, "References", true);
        if (myTargetScript.foldoutState) EditorGUILayout.LabelField("Hello there");

        GUILayout.EndVertical();

        //EditorUtility.SetDirty(myTargetScript);
        EditorSceneManager.MarkAllScenesDirty();
    }

    private void SetReferencesToNull()
    {
        myTargetScript.audioListener = null;
        myTargetScript.myCamera = null;
        myTargetScript.selfTransform = null;
        myTargetScript.camTransform = null;
    }

    private void AutoSetReferences()
    {
        Undo.RecordObject(myTargetScript, "Just set the references");

        myTargetScript.audioListener = FindObjectOfType<AudioListener>();
        myTargetScript.myCamera = FindObjectOfType<Camera>();
        myTargetScript.selfTransform = myTargetScript.transform;
        myTargetScript.camTransform = myTargetScript.myCamera.transform;
    }

    private void OnDisable()
    {
        Undo.undoRedoPerformed -= RecalculateStuffAfterundo;
    }
}
