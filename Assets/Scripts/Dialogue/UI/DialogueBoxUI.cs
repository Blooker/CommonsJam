using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class DialogueBoxUI : MonoBehaviour
{
    [Header("Box")]
    [SerializeField] private RectTransform BoxShowPos;
    [SerializeField] private RectTransform BoxHidePos;
    [FormerlySerializedAs("BoxTweenTime")] [SerializeField] private float BoxTweenInTime;
    [SerializeField] private float BoxTweenOutTime;
    
    [Header("Text")]
    [SerializeField] private float ScrollTime;
    [SerializeField] private TMP_Text NameText;
    [SerializeField] private TMP_Text DialogueText;
    
    [Header("Indicator")]
    [SerializeField] private RectTransform Indicator;
    [SerializeField] private float IndicatorTweenY = -0.4f;
    [SerializeField] private float IndicatorTweenTime = 0.3f;

    private RectTransform Rect;
    
    private Vector3 IndicatorStartPos;
    
    private int BoxTweenID;
    private int IndicatorTweenID;
    
    public bool IsScrolling { get; private set; }

    private void Awake()
    {
        IndicatorStartPos = Indicator.anchoredPosition;
        Rect = transform as RectTransform;
    }
    
    public void Show()
    {
        Rect.anchoredPosition = BoxHidePos.anchoredPosition;
        gameObject.SetActive(true);
        
        LeanTween.cancel(BoxTweenID);
        BoxTweenID = LeanTween.move(Rect, BoxShowPos.anchoredPosition, BoxTweenInTime).setEaseOutQuart().id;
    }
    
    public void Hide()
    {
        Rect.anchoredPosition = BoxShowPos.anchoredPosition;

        LeanTween.cancel(BoxTweenID);
        BoxTweenID = LeanTween.move(Rect, BoxHidePos.anchoredPosition, BoxTweenOutTime).setEaseInQuart().id;
    }

    public void SkipScroll()
    {
        IsScrolling = false;
    }
    
    public void SetName(string name)
    {
        NameText.text = name;
    }

    public void SetDialogue(string sentence)
    {
        HideIndicator();
        StartCoroutine(TypeSentence(sentence));
    }

    private void ShowIndicator()
    {
        Indicator.anchoredPosition = IndicatorStartPos;
        IndicatorTweenID = LeanTween.moveY(Indicator, IndicatorTweenY, IndicatorTweenTime).setEaseInQuart().setLoopPingPong().id;
        Indicator.gameObject.SetActive(true);
    }

    private void HideIndicator()
    {
        LeanTween.cancel(IndicatorTweenID);
        Indicator.gameObject.SetActive(false);
    }
    
    IEnumerator TypeSentence(string sentence)
    {
        IsScrolling = true;
        
        if (ScrollTime > 0)
        {
            DialogueText.text = "";
    
            var chars = sentence.ToCharArray();

            int i = 0;
            float charPos = 0;

            while (i < chars.Length)
            {
                if (!IsScrolling)
                {
                    break;
                }
                
                // Work out how many characters should be typed since the last frame
                charPos += Time.deltaTime / ScrollTime;
                
                // Cast to int and clamp to length of chars array to create index
                int newI = (int)Mathf.Clamp(charPos, 0, chars.Length);
                
                // Add all letters between the old and new index to the string
                for (int j = 0; j < newI - i; j++)
                {
                    var letter = chars[i+j];
                    DialogueText.text += letter;
                }
                
                // Update old index
                i = newI;

                yield return null;
            }
        }
        
        DialogueText.text = sentence;
        IsScrolling = false;

        ShowIndicator();
    }
}
