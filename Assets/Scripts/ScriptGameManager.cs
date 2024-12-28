using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptGameManager : MonoBehaviour
{
    // Score controller
    public GameObject scoreController;

    // public vars
    public int lives;
    public float score;

    // private vars
    private int currLevel;

    private void Start()
    {
        DontDestroyOnLoad(gameObject); 
        NewGame();
    }

    private void NewGame()
    {
        lives = 2;
        score = 0;
        LoadLevel(1);
    }

    private void LoadLevel(int index)
    {
        currLevel = index;

        // transition
        Camera cam = Camera.main;
        if(cam != null)
        {
            cam.cullingMask = 0;
        }

        // Delaying scene load
        Invoke(nameof(LoadScene), 1f);
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(currLevel);
    }

    public void LevelFailed()
    {
        lives--;    
        if(lives <= 0)
        {
            NewGame();
        }else
        {
            LoadLevel(currLevel);
        }
    }

    public void LevelComplete()
    {
        score += 20;
        int nextLevel = currLevel + 1;
        if(nextLevel < SceneManager.sceneCountInBuildSettings)
        {
            LoadLevel(nextLevel);
        }
        else
        {
            LoadLevel(1);
        }
    }
}
