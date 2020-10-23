using Gameplay.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessVignetting : MonoBehaviour
{
    private Vignette vignette;
    [SerializeField] MovementBehavior player;
    [SerializeField] PostProcessVolume postProcVolume;

    // Start is called before the first frame update
    void Awake()
    {
        postProcVolume.profile.TryGetSettings(out vignette);
    }

    // Update is called once per frame
    void Update()
    {
        CustomScaler.Scale(vignette.intensity, player.playspace.leftX, 0, 0, 1);
    }
}
