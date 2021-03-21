using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Dialogue", menuName = "ScriptableObjects/Dialogue", order = 1)]
public class Dialogue : ScriptableObject
{
    public DialoguePart[] Parts;
    
    public DialogueChoice NextChoice;
    public Dialogue NextDialogue;
}

[System.Serializable]
public class DialoguePart
{
    public string Name;

    [FormerlySerializedAs("Lines2")] public DialogueLine[] Lines;
}

[System.Serializable]
public class DialogueLine
{
    [TextArea(3, 10)]
    public string Text;
    
    public int ArtIndex = -1;
    public AudioClip Audio;
}
