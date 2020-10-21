using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyHeavyGameplayScript : MonoBehaviour
{
    public Camera myCamera;
    public Transform selfTransform;
    public Transform camTransform;
    public AudioListener audioListener;

    [HideInInspector] public float mySpeed;
    [SerializeField] private float myPrivateProperty;

    public Color myColor;
    public Vector2 myVector2;
    public AnimationCurve animationCurve;

    public WrapMode exampleEnum;

#if UNITY_EDITOR
    public bool foldoutState;
#endif
    // Update is called once per frame
    void Update()
    {

    }
}
