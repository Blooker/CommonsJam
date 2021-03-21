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

    private void OnEnable()
    {
        NumScenes = SceneManager.sceneCountInBuildSettings;

        #if UNITY_EDITOR
        CurrentScene = SceneManager.GetActiveScene().buildIndex;
        #else
        CurrentScene = 0;
        #endif

    }

    public void NextScene(int nextSceneIndex)
    {
        if (nextSceneIndex >= 0)
        {
            CurrentScene = nextSceneIndex;
        }
        else
        {
            CurrentScene++;
        }
        
        if (CurrentScene < NumScenes)
        {
            Debug.Log($"Loading scene {CurrentScene}");
            SceneManager.LoadSceneAsync(CurrentScene, LoadSceneMode.Single);
        }
    }
}
