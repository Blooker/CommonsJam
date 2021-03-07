using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    
    [SerializeField] private float FadeTime;
    
    private AudioSource Source;
    private float MaxVolume;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        Source = GetComponent<AudioSource>();
    }

    void Start()
    {
        MaxVolume = Source.volume;
        Source.Play();
        Fade(fadeIn: true);
    }

    public void Fade(bool fadeIn)
    {
        StartCoroutine(FadeRoutine(fadeIn));
    }

    IEnumerator FadeRoutine(bool fadeIn)
    {
        float timer = FadeTime;

        while (timer > 0)
        {
            var fadeInFloat = (float)Convert.ToInt32(fadeIn);
            
            var t = fadeInFloat + timer / FadeTime * (1-2*fadeInFloat);

            var vol = Mathf.Lerp(0, MaxVolume, t);
            Source.volume = Mathf.Clamp01(vol);
            
            timer -= Time.deltaTime;

            yield return null;
        }
    }
}
