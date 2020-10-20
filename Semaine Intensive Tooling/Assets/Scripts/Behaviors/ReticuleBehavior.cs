using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ReticuleBehavior : MonoBehaviour
{
    public Camera camera;
    public RectTransform rectTransform;
    public Transform playerTransform;
    RaycastHit hitInto;

    // Update is called once per frame
    void Update()
    {
        //Vector2 worldPosition = Camera.main.ScreenToWorldPoint();
        //Vector2 worldPosition = Mouse.current.delta.ReadValue();
        Vector2 worldPosition = Mouse.current.position.ReadValue();
        //Debug.Log(worldPosition);
        rectTransform.position = new Vector3(worldPosition.x, worldPosition.y, 30f);

        if (Physics.Raycast(playerTransform.position, Camera.main.ScreenToWorldPoint(worldPosition) * 500f, out hitInto))
        {
            rectTransform.position = hitInto.collider.gameObject.transform.position;
        }
    }
}
