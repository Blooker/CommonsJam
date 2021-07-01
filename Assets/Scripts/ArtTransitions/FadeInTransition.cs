using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInTransition : ArtTransition
{
    [SerializeField] private float FadeTime = 0.33f;
    private CanvasGroup CanvasGroup;

    private void Awake()
    {
        CanvasGroup = GetComponent<CanvasGroup>();
    }

    // Start is called before the first frame update
    void Start()
    {
        CanvasGroup.alpha = 0f;
    }

    public override void Transition()
    {
        gameObject.SetActive(true);
        LeanTween.alphaCanvas(CanvasGroup, 1, FadeTime).setEaseOutQuart();
    }
}
