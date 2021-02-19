using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueChoiceUI : MonoBehaviour
{
    [SerializeField] private DialogueManager Dialogue;
    [SerializeField] private DialogueOptionUI[] OptionUIs;

    public void SetOptions(DialogueOption[] options)
    {
        gameObject.SetActive(true);
        
        for (int i = 0; i < OptionUIs.Length; i++)
        {
            if (i < options.Length)
            {
                OptionUIs[i].SetOption(options[i]);
            }
            else
            {
                OptionUIs[i].gameObject.SetActive(false);
            }
        }
    }
    
    public void Select(DialogueOption option)
    {
        Dialogue.StartDialogue(option.ResultDialogue);
        gameObject.SetActive(false);
    }
}
