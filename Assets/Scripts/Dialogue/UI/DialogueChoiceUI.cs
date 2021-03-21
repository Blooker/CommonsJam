using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueChoiceUI : MonoBehaviour
{
    [SerializeField] private DialogueOptionUI[] OptionUIs;
    [SerializeField] private float BoxFadeTime;

    [SerializeField] private Button ContinueButton;
    
    private int TweenID;
    private CanvasGroup Group;

    private void Awake()
    {
        Group = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        Group.alpha = 0;
    }

    public void Show(DialogueOption[] options)
    {
        ContinueButton.interactable = false;
        
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
        
        FadeIn();
    }
    
    public void Select(DialogueOption option)
    {
        ScoreManager.Instance.Add(option.ResultPoints);
        
        DialogueManager.Instance.SetFlag(option.Flag);
        DialogueManager.Instance.StartDialogue(option.ResultDialogue);

        ContinueButton.interactable = true;
        FadeOut();
    }
    
    private void FadeIn()
    {
        gameObject.SetActive(true);
        
        LeanTween.cancel(TweenID);
        
        Group.blocksRaycasts = true;
        Group.interactable = true;
        
        TweenID = LeanTween.alphaCanvas(Group, to: 1f, BoxFadeTime).setEaseOutQuart().id;
    }
    
    private void FadeOut()
    {
        LeanTween.cancel(TweenID);

        Group.blocksRaycasts = false;
        Group.interactable = false;

        TweenID = LeanTween.alphaCanvas(Group, to: 0f, BoxFadeTime).setEaseOutQuart().id;
    }
}
