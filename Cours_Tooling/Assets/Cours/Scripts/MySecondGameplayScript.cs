using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Enemy
{
    public Color enemyColor;
    public float enemySpeed;
    public Vector3 spawnPosition;
}

public class MySecondGameplayScript : MonoBehaviour
{
    public Enemy myEnemy;

    public Color myColor;
   
    //[Header("Texts")]
    public string myString;
    public string[] myStrings;
    //[TextArea(1, 3)]
    public string someBigText;

    /*[Space]
    [Header("Numbers")]
    [Range(0f, 1f)] */public int slider;
    public Vector2 myVector2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
