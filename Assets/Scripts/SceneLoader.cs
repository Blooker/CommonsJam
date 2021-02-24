using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "SceneLoader", menuName = "ScriptableObjects/SceneLoader", order = 1)]
public class SceneLoader : ScriptableObject
{
    private int NumScenes;
    private int CurrentScene;
    
    private void Awake()
    {
        NumScenes = SceneManager.sceneCountInBuildSettings;
    }

    private void OnEnable()
    {
        CurrentScene = SceneManager.GetActiveScene().buildIndex;
    }

    public void NextScene()
    {
        CurrentScene++;
        if (CurrentScene < NumScenes)
            SceneManager.LoadSceneAsync(CurrentScene, LoadSceneMode.Single);
    }
}
