using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level Profile", menuName ="Leonard/New Level Profile", order = 100)]
public class LevelProfile : ScriptableObject
{
    public float difficulty;
    public Color environementColor;
    public int[] levelValues;
}
