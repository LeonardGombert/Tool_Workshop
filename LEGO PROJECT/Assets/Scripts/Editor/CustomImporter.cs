using UnityEditor.Experimental.AssetImporters;
using System.IO;
using UnityEngine;

[ScriptedImporter(1, "cctxt")]
public class CustomImporter : ScriptedImporter
{
    public override void OnImportAsset(AssetImportContext ctx)
    {
        string fileColor = File.ReadAllText(ctx.assetPath);
        GameObject newObj = GameObject.CreatePrimitive(PrimitiveType.Cube);

        MeshRenderer renderer = newObj.GetComponent<MeshRenderer>();
        Material material = renderer.material;

        if (ColorUtility.TryParseHtmlString(fileColor, out Color cubeColor))
            material.SetColor("_BaseColor", cubeColor);

        renderer.material = material;
    }
}
