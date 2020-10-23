using Gameplay.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessVignetting : MonoBehaviour
{
    private Vignette vignette;
    [SerializeField] PostProcessVolume postProcVolume;
    [SerializeField] MovementBehavior player;

    // Start is called before the first frame update
    void Awake()
    {
        postProcVolume.profile.TryGetSettings(out vignette);
    }

    // Update is called once per frame
    void Update()
    {
        vignette.intensity.value = player.vignetteSetting;
    }
}
