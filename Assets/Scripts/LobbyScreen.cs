using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyScreen : MonoBehaviour
{
    public Text scoreText;
    public Text minigameNameText;
    private int totalScore = 0;

    private void Start()
    {
        UpdateScoreDisplay();
        minigameNameText.text = "";
    }

    public void SelectMinigame(string minigameName)
    {
        SceneManager.LoadScene(minigameName);
    }

    private void UpdateScoreDisplay()
    {
        scoreText.text = "TOTAL SCORE: " + totalScore;
    }

    public void AddScore(int score)
    {
        totalScore += score;
        UpdateScoreDisplay();
    }

    public void OnMinigameHover(string minigameName)
    {
        minigameNameText.text = minigameName;
    }

    public void OnMinigameExit()
    {
        minigameNameText.text = "";
    }
}
