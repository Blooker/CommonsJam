using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    
    public static int Score { get; private set; }

    public static readonly int WIN_SCORE_PROTEST = 20;
    public static readonly int WIN_SCORE_NO_PROTEST = 17;
    
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
    
    public void Zero()
    {
        Score = 0;
        DEBUG_Score = Score;
    }

    public bool MetWinCriteria(bool protest)
    {
        return Score >= (protest ? WIN_SCORE_PROTEST : WIN_SCORE_NO_PROTEST);
    }
}
