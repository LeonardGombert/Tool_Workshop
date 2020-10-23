using Gameplay.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayspaceChangeBehavior : MonoBehaviour
{
    public PlayspaceScriptableObject myValues;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("yes");
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("YES");
            MovementBehavior player = collision.gameObject.GetComponent<MovementBehavior>();
            StartCoroutine(player.ChangePlayspace(myValues.playspaceBounds, myValues.backupTweenTransitionInt));
        }
    }
}
