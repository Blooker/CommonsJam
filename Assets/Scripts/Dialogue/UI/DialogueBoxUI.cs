using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class DialogueBoxUI : MonoBehaviour
{
    [Header("Fonts")]
    [SerializeField] private string NameFont;
    
    [Header("Box")]
    [SerializeField] private CanvasGroup BoxGroup;
    [SerializeField] private float BoxFadeTime;
    
    [Header("Text")]
    [SerializeField] private float ScrollTime;
    [SerializeField] private TMP_Text NameText;
    [SerializeField] private TMP_Text DialogueText;
    
    [Header("Indicator")]
    [SerializeField] private RectTransform Indicator;
    [SerializeField] private float IndicatorTweenY = -0.4f;
    [SerializeField] private float IndicatorTweenTime = 0.3f;
    
    private Vector3 IndicatorStartPos;
    
    private int BoxTweenID;
    private int IndicatorTweenID;
    
    public bool IsScrolling { get; private set; }

    private void Awake()
    {
        IndicatorStartPos = Indicator.anchoredPosition;
    }
    
    public void FadeIn()
    {
        gameObject.SetActive(true);
        
        LeanTween.cancel(BoxTweenID);
        BoxTweenID = LeanTween.alphaCanvas(BoxGroup, 1, BoxFadeTime).setEaseOutQuart().id;
    }
    
    public void FadeOut()
    {
        LeanTween.cancel(BoxTweenID);
        BoxTweenID = LeanTween.alphaCanvas(BoxGroup, 0, BoxFadeTime).setEaseInQuart().id;
    }

    public void SkipScroll()
    {
        IsScrolling = false;
    }

    public void SetDialogue(string name, string sentence)
    {
        string nameString = "";
        if (!string.IsNullOrEmpty(name))
        {
            nameString = $"<font=\"{NameFont}\">{name}: </font>";
        }
        
        DialogueText.text = nameString;

        HideIndicator();
        StartCoroutine(TypeSentence(nameString, sentence));
    }

    private void ShowIndicator()
    {
        Indicator.anchoredPosition = IndicatorStartPos;
        IndicatorTweenID = LeanTween.moveY(Indicator,  Indicator.anchoredPosition.y - IndicatorTweenY, IndicatorTweenTime).setEaseInQuart().setLoopPingPong().id;
        Indicator.gameObject.SetActive(true);
    }

    private void HideIndicator()
    {
        LeanTween.cancel(IndicatorTweenID);
        Indicator.gameObject.SetActive(false);
    }
    
    IEnumerator TypeSentence(string nameString, string sentence)
    {
        IsScrolling = true;
        
        if (ScrollTime > 0)
        {
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
                int j = 0;
                while (j < newI - i)
                {
                    // If a font tag is detected, add it all in one go before continuing
                    if (chars[i+j] == '<')
                    {
                        do
                        {
                            DialogueText.text += chars[i + j];
                            i++;
                            newI++;
                            charPos++;
                        } while (chars[i+j-1] != '>');
                    }
                    
                    DialogueText.text += chars[i+j];
                    j++;
                }

                // Update old index
                i = newI;

                yield return null;
            }
        }
        
        DialogueText.text = nameString + sentence;
        IsScrolling = false;

        ShowIndicator();
    }
}
