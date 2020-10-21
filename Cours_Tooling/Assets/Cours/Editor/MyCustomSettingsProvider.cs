using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MyCustomSettingsProvider : SettingsProvider
{
    static Editor myCustomSettingsProvider;

    public MyCustomSettingsProvider(string path, SettingsScope scope = SettingsScope.Project) : base (path, scope)
    {

    }

    [SettingsProvider]
    static SettingsProvider CreateProvider()
    {
        MyCustomSettingsProvider mySP = new MyCustomSettingsProvider("Project/My Custom Settings", SettingsScope.Project);

        mySP.guiHandler = OnProviderGUI;

        return mySP;
    }

    static void OnProviderGUI(string searchBarContents)
    {
        MyCustomSettings mcs = Resources.Load("My Settings") as MyCustomSettings;

        if(!myCustomSettingsProvider) Editor.CreateCachedEditor(mcs, null, ref myCustomSettingsProvider);
        myCustomSettingsProvider.OnInspectorGUI();
    }
}
