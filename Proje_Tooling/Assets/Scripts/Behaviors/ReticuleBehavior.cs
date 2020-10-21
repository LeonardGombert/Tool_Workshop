using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ReticuleBehavior : MonoBehaviour
{
    [SerializeField] RectTransform rectTransform;
    [SerializeField] Transform playerTransform;
    RaycastHit hit;

    // Update is called once per frame
    void Update()
    {
        Vector2 worldPosition = Mouse.current.position.ReadValue();
        rectTransform.position = new Vector3(worldPosition.x, worldPosition.y, 100f);

        if (Physics.Raycast(playerTransform.position, Camera.main.ScreenToWorldPoint(rectTransform.position), out hit))
            rectTransform.position = new Vector3(worldPosition.x, worldPosition.y, hit.collider.gameObject.transform.position.z);
    }
}
