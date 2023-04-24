using UnityEngine;
using System.Collections;

public class LootDrop_AudioCurves : MonoBehaviour
{
    public AnimationCurve AudioCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    public float GraphTimeMultiplier = 1;
    public bool IsLoop;
    public AnimationCurve AudioCurveFadeOut = AnimationCurve.EaseInOut(0, 1, 1, 0);
    public float FadeOutTime = 1;
    public bool FadeOut;

    private bool canUpdate;
    private float startTime;
    private AudioSource audioSource;
    private float startVolume;
    private float fadeOutStartTime;
    private bool fadeOutStarted;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        startVolume = audioSource.volume;
        audioSource.volume = AudioCurve.Evaluate(0);
    }

    private void OnEnable()
    {
        startTime = Time.time;
        canUpdate = true;
        FadeOut = false;
        fadeOutStarted = false;
    }

    private void Update()
    {
        var time = Time.time - startTime;
        if (canUpdate) {
            var eval = AudioCurve.Evaluate(time / GraphTimeMultiplier) * startVolume;
            audioSource.volume = eval;
        }
        if (time >= GraphTimeMultiplier) {
            if (IsLoop) startTime = Time.time;
            else canUpdate = false;
        }

        if (FadeOut == true && fadeOutStarted == false)
        {
            fadeOutStarted = true;
            canUpdate = false;
            fadeOutStartTime = Time.time;
        }

        if (fadeOutStarted == true) {
            var fadeTime = Time.time - fadeOutStartTime;
            var evalFade = AudioCurveFadeOut.Evaluate(fadeTime / FadeOutTime) * startVolume;
            audioSource.volume = evalFade;
           // Debug.Log(fadeTime + "  " + evalFade);
        }


    }
}