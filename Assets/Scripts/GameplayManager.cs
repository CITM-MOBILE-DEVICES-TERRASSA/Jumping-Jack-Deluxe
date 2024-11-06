using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Instance { get; private set; }

    int currentScore = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void ReturnToLobby()
    {
        SceneManager.LoadScene("Lobby");
        int totalScore = PlayerPrefs.GetInt("totalScore", 0);
        PlayerPrefs.SetInt("totalScore", totalScore + currentScore);
    }
}
