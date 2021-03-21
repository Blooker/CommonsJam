using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private float FadeTime;
    [SerializeField] private AudioSource[] Sources;
    
    private float[] MaxVolumes;

    private int SourceIndex = -1;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        Init();
    }

    public void NextAudio(AudioClip clip)
    {
        if (SourceIndex >= 0)
        {
            Fade(fadeIn: false, index: SourceIndex);
        }

        SourceIndex++;

        if (SourceIndex < Sources.Length)
        {
            Sources[SourceIndex].clip = clip;
            Fade(fadeIn: true, index: SourceIndex);
        }
    }
    
    private void Fade(bool fadeIn, int index)
    {
        StartCoroutine(FadeRoutine(fadeIn, index));
    }

    private void Init()
    {
        SourceIndex = -1;
        MaxVolumes = new float[Sources.Length];

        for (int i = 0; i < Sources.Length; i++)
        {
            MaxVolumes[i] = Sources[i].volume;
            Sources[i].volume = 0;
        }
    }
    
    IEnumerator FadeRoutine(bool fadeIn, int index)
    {
        float timer = FadeTime;

        if (fadeIn)
            Sources[index].Play();
        
        while (timer > 0)
        {
            var fadeInFloat = (float)Convert.ToInt32(fadeIn);
            
            var t = fadeInFloat + timer / FadeTime * (1-2*fadeInFloat);

            var vol = Mathf.Lerp(0, MaxVolumes[index], t);
            Sources[index].volume = Mathf.Clamp01(vol);
            
            timer -= Time.deltaTime;

            yield return null;
        }
        
        if (!fadeIn)
            Sources[index].Stop();
    }
}
