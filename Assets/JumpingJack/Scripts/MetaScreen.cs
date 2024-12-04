using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MetaScreen : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int totalScore = 0;

    private void Start()
    {
        UpdateScoreDisplay();
    }

    public void SelectLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
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
