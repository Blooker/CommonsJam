using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArtManager : MonoBehaviour
{
    public static ArtManager Instance;
    
    [SerializeField] private RectTransform[] Art;
    [SerializeField] private float FadeTime;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void FadeIn(int index)
    {
        gameObject.SetActive(true);
        LeanTween.alpha(Art[index], 1, FadeTime).setEaseOutQuart();
    }
}
