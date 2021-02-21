using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue Dialogue;

    private void Start()
    {
        TriggerDialogue();
    }

    private void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(Dialogue);
    }
}
