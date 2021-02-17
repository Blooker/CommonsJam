using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [SerializeField] private DialogueBoxUI UI;

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

        UI.Show();
        DisplayNextSentence();
    }

    public void NextPressed()
    {
        if (UI.IsScrolling)
        {
            UI.SkipScroll();
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
        var line = part.Lines[LineIndex];
        
        UI.SetName(part.Name);
        UI.SetDialogue(line);

        LineIndex++;
    }

    void EndDialogue()
    {
        Debug.Log("End of conversation");
        UI.Hide();
    }
}
