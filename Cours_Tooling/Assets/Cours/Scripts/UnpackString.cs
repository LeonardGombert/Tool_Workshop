using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnpackString : MonoBehaviour
{
    public TextAsset textAsset;

    // Start is called before the first frame update
    void Start()
    {
        if(textAsset != null)
        {
            string fullFileContents = textAsset.text;
            string[] lines = fullFileContents.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

            List<string>[] fullTable = new List<string>[lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                fullTable[i] = new List<string>();
                string[] colums = lines[i].Split(new string[] { "," }, StringSplitOptions.None);

                for (int j = 0; j < colums.Length; j++) fullTable[i].Add(colums[j]);
            }
        }
    }
}
