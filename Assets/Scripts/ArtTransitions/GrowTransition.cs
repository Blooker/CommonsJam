using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowTransition : ArtTransition
{
    [SerializeField] private Vector3 EndSize;
    [SerializeField] private RectTransform EndRect;

    [SerializeField] private float ScaleTime;
    
    private RectTransform Rect;
    
    // Start is called before the first frame update
    void Start()
    {
        Rect = (RectTransform)transform;
    }

    public override void Transition()
    {
        LeanTween.scale(Rect, EndSize, ScaleTime);
        LeanTween.move(Rect, EndRect.anchoredPosition, ScaleTime);
    }
}
