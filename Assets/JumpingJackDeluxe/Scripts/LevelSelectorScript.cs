using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectorScript : MonoBehaviour
{
    [SerializeField]
    private string lobbySceneName = "Lobby";
    
    [SerializeField]
    private string[] levelSceneNames = new string[5] { "Level1JJD", "Level2JJD", "Level3JJD", "Level4JJD", "Level5JJD" };

    public void LoadLevel(int levelIndex)
    {
        if (levelIndex >= 0 && levelIndex < levelSceneNames.Length)
        {
            SceneManager.LoadScene(levelSceneNames[levelIndex]);
        }
        else
        {
            Debug.LogError("Invalid level index!");
        }
    }

    public void ReturnToLobby()
    {
        SceneManager.LoadScene(lobbySceneName);
    }
}