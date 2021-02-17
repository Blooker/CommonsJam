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
    
    private Queue<string> sentences;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log($"Starting conversation with {dialogue.Name}");
        UI.SetName(dialogue.Name);
        
        sentences.Clear();

        for (var index = 0; index < dialogue.Sentences.Length; index++)
        {
            var sentence = dialogue.Sentences[index];
            sentences.Enqueue(sentence);
        }

        UI.Show();
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count <= 0)
        {
            EndDialogue();
            return;
        }

        var sentence = sentences.Dequeue();
        UI.SetDialogue(sentence);
    }

    void EndDialogue()
    {
        Debug.Log("End of conversation");
        UI.Hide();
    }
}
