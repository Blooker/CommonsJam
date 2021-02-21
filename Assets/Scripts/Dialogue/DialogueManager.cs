using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [SerializeField] private DialogueBoxUI BoxUI;
    [SerializeField] private DialogueChoiceUI ChoiceUI;
    
    
    private Dialogue Dialogue;
    
    private int PartIndex;
    private int LineIndex;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Dialogue = dialogue;
        
        PartIndex = 0;
        LineIndex = 0;

        BoxUI.FadeIn();
        DisplayNextSentence();
    }

    public void NextPressed()
    {
        if (BoxUI.IsScrolling)
        {
            BoxUI.SkipScroll();
        }
        else
        {
            DisplayNextSentence();
        }
    }
    
    private void DisplayNextSentence()
    {
        if (LineIndex >= Dialogue.Parts[PartIndex].Lines.Length)
        {
            LineIndex = 0;
            PartIndex++;
        }
        
        if (PartIndex >= Dialogue.Parts.Length)
        {
            EndDialogue();
            return;
        }

        var part = Dialogue.Parts[PartIndex];
        BoxUI.SetDialogue(part.Name, part.Lines[LineIndex]);

        LineIndex++;
    }

    void EndDialogue()
    {
        Debug.Log("End of conversation");

        if (Dialogue.NextChoice != null)
        {
            ChoiceUI.Show(Dialogue.NextChoice.Options);
        }
        else if (Dialogue.NextDialogue != null)
        {
            StartDialogue(Dialogue.NextDialogue);
            return;
        }
        
        BoxUI.FadeOut();
    }
}
