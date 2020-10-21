using UnityEditor;
using UnityEngine;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using UnityEditorInternal;
using System;

[CustomEditor(typeof(SomeCommonComponent))]
public class SomeCommonComponentInspector : Editor
{
    SerializedProperty myProperty, forkProperty, myArrayProperty;
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
        myArrayProperty = serializedObject.FindProperty("myArray");
        rList = new ReorderableList(serializedObject, myArrayProperty, true, true, true, true);
        rList = SetupList(rList);
    }

    private ReorderableList SetupList(ReorderableList emptyList)
    {
        // you can declare the function as a lambad expression
        emptyList.drawHeaderCallback += (Rect rect) =>
        {
            EditorGUI.LabelField(rect, "This is the header for my list");
        };

        // or as a full method
        emptyList.drawElementCallback += DrawMyListElement;
        emptyList.onReorderCallback += MyListHasBeenReordered;
        emptyList.onAddCallback += MyListAddCallback;
        emptyList.onRemoveCallback += MyListRemoveCallback;
        emptyList.elementHeightCallback += GetArrayElementHeight;

        return emptyList;
    }

    // isActive highlights the element in blue
    //isFocused means isActive AND you're currently in the window (not looking somewhere else)
    void DrawMyListElement(Rect rect, int index, bool isActive, bool isFocused)
    {
        SerializedProperty elementProp = myArrayProperty.GetArrayElementAtIndex(index);
        EditorGUI.PropertyField(rect, elementProp);
    }

    float GetArrayElementHeight(int index)
    {
        // make each element the size of its index (haha)
        return (float)(index+1) * (EditorGUIUtility.singleLineHeight + 1);
    }

    void MyListHasBeenReordered(ReorderableList list)
    {
        // some reorder code
    }

    void MyListAddCallback(ReorderableList list)
    {
        Debug.Log("added an element at index " + list.index.ToString());
        myArrayProperty.InsertArrayElementAtIndex(list.index);
    }

    void MyListRemoveCallback(ReorderableList list)
    {
        Debug.Log("removed an element at index " + list.index.ToString());
        myArrayProperty.DeleteArrayElementAtIndex(list.index);
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        rList.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }
}
