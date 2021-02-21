using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueBoxesUI : MonoBehaviour
{
    [SerializeField] private DialogueBoxUI[] Boxes;
    
    private int BoxIndex;

    public void SetNextDialogue(string name, string sentence)
    {
        Boxes[BoxIndex].FadeIn();
        Boxes[BoxIndex].SetDialogue(name, sentence);
    }

    public void ContinuePressed()
    {
        if (Boxes[BoxIndex].IsScrolling)
        {
            Boxes[BoxIndex].SkipScroll();
        }
        else
        {
            Boxes[BoxIndex].HideIndicator();
            BoxIndex++;

            DialogueManager.Instance.DisplayNextSentence();
        }
    }
}
