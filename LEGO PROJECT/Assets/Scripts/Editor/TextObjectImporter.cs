using UnityEditor.Experimental.AssetImporters;
using System.IO;
using System.Linq;
using UnityEngine;

[ScriptedImporter(1, "otxt")]
public class TextObjectImporter : ScriptedImporter
{
    public override void OnImportAsset(AssetImportContext ctx)
    {
        var lines = File.ReadAllLines(ctx.assetPath);

        GameObject lastObject = null;
        int lastOffset = 0;

        foreach (var line in lines)
        {
            var offset = line.Count(c => c == '>');
            var obj = new GameObject(line.Substring(offset));

            if (offset == 0)
                ctx.AddObjectToAsset(obj.name, obj);

            else
            {
                var offsetDiff = offset - lastOffset;

                if (offsetDiff < 0) for (int i = 0; i > offsetDiff; i--) 
                        lastObject = lastObject.transform.parent.gameObject;

                obj.transform.parent = lastObject.transform;
            }

            lastObject = obj;
            lastOffset = offset;
        }
    }
}
