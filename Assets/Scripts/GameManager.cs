using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager m_Instance;
    public static GameManager Instance => m_Instance;

    public const string PlayScene = "GameScene";
    public const string MenuScene = "MainMenuScene";
    public const string GameOverScene = "GameOverScene";

    private int m_Score = 0;
    public int TopScore
    {
        get
        {
            return m_Score;
        }
        set
        {
            if (value > m_Score)
                m_Score = value;
        }
    }

    private void Awake()
    {
        if (m_Instance == null)
        {
            m_Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
        InitValues();

        ChangeScene(MenuScene);
    }

    //Es crida al carregar una escena
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("GameManager - OnSceneLoaded: " + scene.name);
    }

    private void InitValues()
    {
        m_Score = 0;
        
    }

    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}


