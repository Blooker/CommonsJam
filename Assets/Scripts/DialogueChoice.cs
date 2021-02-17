using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Dialogue_Choice", menuName = "ScriptableObjects/Dialogue Choice", order = 2)]
public class DialogueChoice : ScriptableObject
{
    public DialogueOption[] Options;
}

[System.Serializable]
public class DialogueOption
{
    [TextArea(3, 10)]
    public string Option;
    public Dialogue ResultDialogue;
}
