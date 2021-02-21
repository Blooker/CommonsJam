using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup CanvasGroup;
    [SerializeField] private float FadeTime;

    public void FadeOutAll()
    {
        CanvasGroup.interactable = false;
        LeanTween.alphaCanvas(CanvasGroup, 0, FadeTime);
    }
}
