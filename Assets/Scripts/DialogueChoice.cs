using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


[CreateAssetMenu(fileName = "Dialogue_Choice", menuName = "ScriptableObjects/Dialogue Choice", order = 2)]
public class DialogueChoice : ScriptableObject
{
    public DialogueOption[] Options;
}

[System.Serializable]
public class DialogueOption
{
    [TextArea(3, 10)]
    public string Text;
    public Dialogue ResultDialogue;
}
