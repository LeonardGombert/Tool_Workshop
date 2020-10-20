using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ReticuleBehavior : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Vector2 worldPosition = Camera.main.ScreenToViewportPoint(Mouse.current.position.ReadValue()); 
        transform.position = new Vector3(worldPosition.x, worldPosition.y, 30f);
    }
}
