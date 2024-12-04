using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UpdateLobbyScore : MonoBehaviour
{
    public TextMeshProUGUI lobbyScore;
    
    
    private ScoreManager scoreManager;

    void Start()
    {
        
        scoreManager = ScoreManager.Instance;
        UpdateTotalGameScore();
    }
    private void Update()
    {
        ShowScoreText1();
    }
    public void UpdateTotalGameScore()
    {
        UpdateScoreTotal();
        scoreManager.MaxTotalGame = scoreManager.MaxTotalLevels; // a�adir mas para mas juegos
    }
     private void ShowScoreText1()
    {
        if (lobbyScore != null)
        {
            lobbyScore.text = "Puntos" + scoreManager.MaxTotalGame;
        }
        else
        {
            Debug.LogError("Score Text no est� asignado en el ScoreManager.");
        }
        
    }
    private void UpdateScoreTotal()
    {
        scoreManager.MaxTotalLevels = scoreManager.MaxScore1 + scoreManager.MaxScore2;
        scoreManager.MaxTotalGame = scoreManager.MaxTotalLevels;
    }
}
