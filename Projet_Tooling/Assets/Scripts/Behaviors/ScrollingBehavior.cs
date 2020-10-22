using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBehavior : MonoBehaviour
{
    private float speed = 15f;

    private void Start()
    {
        StartCoroutine(Scroll());
    }

    // Update is called once per frame
    IEnumerator Scroll()
    {
        while (true)
        {
            transform.position += Vector3.back * speed * Time.deltaTime;
            yield return null;
        }
    }
}
