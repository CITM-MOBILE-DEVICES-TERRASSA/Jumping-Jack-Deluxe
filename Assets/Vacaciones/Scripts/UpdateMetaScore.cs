using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UpdateMetaScore : MonoBehaviour
{
    public TextMeshProUGUI metaScoreText;
    public TextMeshProUGUI Level1ScoreText;
    public TextMeshProUGUI Level2ScoreText;

    private ScoreManager scoreManager;

    private void Start()
    {
        
        scoreManager = ScoreManager.Instance;
        UpdateScoreTotal();
        
    }
    private void Update()
    {
        UpdateMetaScoreTotalText();
        UpdateMetaScoreLevel1Text();
        UpdateMetaScoreLevel2Text();
        UpdateScoreTotal();
    }
    private void UpdateScoreTotal()
    {
        scoreManager.MaxTotalLevels = scoreManager.MaxScore1 + scoreManager.MaxScore2;
        scoreManager.MaxTotalGame = scoreManager.MaxTotalLevels;
        PlayerPrefs.SetInt("MaxTotalGame2", scoreManager.MaxTotalGame);
    }
    private void UpdateMetaScoreTotalText()
    {
        if (metaScoreText != null)
        {
            metaScoreText.text = "Puntos: " + scoreManager.MaxTotalLevels;
        }
        else
        {
            Debug.LogError("Score Text no est� asignado en el ScoreManager.");
        }
        
    }
    private void UpdateMetaScoreLevel1Text()
    {
        if (Level1ScoreText != null)
        {
            Level1ScoreText.text = "Puntos: " + scoreManager.MaxScore1;
        }
        else
        {
            Debug.LogError("Score Text no est� asignado en el ScoreManager.");
        }
       
    }
    private void UpdateMetaScoreLevel2Text()
    {
        if (Level2ScoreText != null)
        {
            Level2ScoreText.text = "Puntos: " + scoreManager.MaxScore2;
        }
        else
        {
            Debug.LogError("Score Text no est� asignado en el ScoreManager.");
        }
    }
}
