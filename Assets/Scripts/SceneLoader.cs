using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "SceneLoader", menuName = "ScriptableObjects/SceneLoader", order = 1)]
public class SceneLoader : ScriptableObject
{
    private static readonly int SCENE_1_INDEX = 0;
    private static readonly int SCENE_7_INDEX = 8;
    
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

        // Gross special case code, might make a better system if I need to
        if (CurrentScene == SCENE_1_INDEX)
        {
            ScoreManager.Instance.Zero();
        }
        
        if (CurrentScene == SCENE_7_INDEX && DialogueManager.Instance.GetFlag("BONUS"))
        {
            CurrentScene = SCENE_7_INDEX + 1;
        }
        
        if (CurrentScene < NumScenes)
        {
            Debug.Log($"Loading scene {CurrentScene}");
            SceneManager.LoadSceneAsync(CurrentScene, LoadSceneMode.Single);
        }
    }
}
