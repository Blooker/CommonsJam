using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueBoxUI : MonoBehaviour
{
    [SerializeField] private TMP_Text NameText;
    [SerializeField] private TMP_Text DialogueText;

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

    public void SetDialogue(string dialogue)
    {
        DialogueText.text = dialogue;
    }
}
