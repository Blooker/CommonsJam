using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArtManager : MonoBehaviour
{
    public static ArtManager Instance;
    
    [SerializeField] private CanvasGroup[] ArtGroups;
    [SerializeField] private float FadeTime;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        for (int i = 0; i < ArtGroups.Length; i++)
        {
            ArtGroups[i].alpha = 0f;
        }
    }

    public void FadeIn(int index)
    {
        gameObject.SetActive(true);
        LeanTween.alphaCanvas(ArtGroups[index], 1, FadeTime).setEaseOutQuart();
    }
}
