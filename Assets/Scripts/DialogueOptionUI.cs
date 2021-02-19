using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueOptionUI : MonoBehaviour
{
    [SerializeField] private Button Button;
    [SerializeField] private DialogueChoiceUI ChoiceUI;
    [SerializeField] private TMP_Text Text;
    
    private DialogueOption Option;

    private void Start()
    {
        Button.onClick.AddListener(Select);
    }

    public void SetOption(DialogueOption option)
    {
        Option = option;
        Text.text = Option.Text;
    }

    private void Select()
    {
        ChoiceUI.Select(Option);
    }
}
