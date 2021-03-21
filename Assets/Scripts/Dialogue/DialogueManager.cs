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
    
    [SerializeField] private DialogueUI UI;
    [SerializeField] private DialogueBoxesUI BoxesUI;
    [SerializeField] private DialogueChoiceUI ChoiceUI;

    [SerializeField] private SceneLoader SceneLoader;

    private static HashSet<string> Flags;

    private Dialogue Dialogue;

    private int PartIndex;
    private int LineIndex;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        
        if (Flags == null)
            Flags = new HashSet<string>();

        foreach (var flag in Flags)
        {
            Debug.Log(flag);
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        if (dialogue == null)
        {
            StartCoroutine(EndScene());
            return;
        }
        
        Dialogue = dialogue;
        
        PartIndex = 0;
        LineIndex = 0;

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
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
        
        BoxesUI.SetNextDialogue(LineIndex == 0 ? part.Name : "", line.Text);

        if (line.ArtIndex >= 0)
        {
            ArtManager.Instance.FadeIn(line.ArtIndex);
        }

        if (line.Audio != null)
        {
            SoundManager.Instance.NextAudio(line.Audio);
        }
        
        LineIndex++;
    }

    public void SetFlag(string flag)
    {
        if (string.IsNullOrEmpty(flag))
        {
            return;
        }

        Flags.Add(flag);
    } 
    
    public bool GetFlag(string flag)
    {
        if (string.IsNullOrEmpty(flag))
        {
            return false;
        }

        return Flags.Contains(flag);
    } 
    
    private void EndDialogue()
    {
        Debug.Log("End of conversation");

        if (Dialogue.NextChoice != null)
        {
            ChoiceUI.Show(Dialogue.NextChoice.Options);
        }
        else if (Dialogue.NextDialogue != null)
        {
            StartDialogue(Dialogue.NextDialogue);
        }
        else
        {
            StartCoroutine(EndScene());
        }
    }

    private IEnumerator EndScene()
    {
        SoundManager.Instance.NextAudio(clip: null);
        yield return UI.FadeOutAll();
        yield return new WaitForSeconds(0.5f);
        
        SceneLoader.NextScene();
    }
}
