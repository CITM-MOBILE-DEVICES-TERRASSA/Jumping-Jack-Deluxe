using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyScreen : MonoBehaviour
{
    public Text scoreText; 
    private int totalScore = 0;

    private void Start()
    {
        UpdateScoreDisplay();
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
}
