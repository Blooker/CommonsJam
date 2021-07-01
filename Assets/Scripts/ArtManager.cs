using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArtManager : MonoBehaviour
{
    public static ArtManager Instance;
    
    [SerializeField] private ArtTransition[] ArtTransitions;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void Transition(int index)
    {
        ArtTransitions[index].Transition();
    }
}
