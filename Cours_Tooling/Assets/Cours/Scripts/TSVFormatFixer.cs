using UnityEditor;
using System.IO;

public class TSVFormatFixer : AssetPostprocessor
{
    public static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        if (importedAssets == null) return;
        if (importedAssets.Length == 0) return;
        for (int i = 0; i < importedAssets.Length; i++)
        {
            string str = importedAssets[i];
            if (!str.EndsWith(".tsv")) continue;

            str = str.Substring(0, str.Length - 4);
            str += ".csv";
            File.Move(importedAssets[i], str);
            File.Move(importedAssets[i]+ ".meta", str + ".meta");

            /*char seperator = ';';
            string content = File.ReadAllText(str);
            content = content.Replace('\t', seperator);
            File.WriteAllText(str, content);*/
        }
    }
}
