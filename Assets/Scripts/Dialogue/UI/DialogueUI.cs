using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup CanvasGroup;
    [SerializeField] private float FadeTime;

    public IEnumerator FadeOutAll()
    {
        int id = LeanTween.alphaCanvas(CanvasGroup, 0, FadeTime).id;
        while (LeanTween.isTweening(id))
        {
            yield return null;
        }
    }
}
