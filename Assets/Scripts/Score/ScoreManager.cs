using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    
    public static int Score { get; private set; }

    private int DEBUG_Score;

    private void Awake()
    {
        DEBUG_Score = Score;

        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void Add(int points)
    {
        Score += points;
        DEBUG_Score = Score;
    }
}
