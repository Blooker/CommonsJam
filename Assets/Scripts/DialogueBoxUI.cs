using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueBoxUI : MonoBehaviour
{
    [SerializeField] private float ScrollTime;
    [SerializeField] private TMP_Text NameText;
    [SerializeField] private TMP_Text DialogueText;

    private bool ScrollSkipped;
    
    private void Start()
    {
        Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
    
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    
    public void SetName(string name)
    {
        NameText.text = name;
    }

    public void SetDialogue(string sentence)
    {
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        if (ScrollTime > 0)
        {
            DialogueText.text = "";
    
            var chars = sentence.ToCharArray();

            int i = 0;
            float charPos = 0;

            while (i < chars.Length)
            {
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
    }
}
