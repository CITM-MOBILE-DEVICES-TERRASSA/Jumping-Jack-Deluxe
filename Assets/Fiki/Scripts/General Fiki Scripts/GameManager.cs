using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public static Manager instance;

    public int lives = 3;
    public int score;
    public int maxscore;
    public int currentLevel;
    public bool hasPrice=false;

    public GameObject pauseCanvas;

    public AudioClip atrasFx;

    public bool isPaused = false;

    [SerializeField] private TextMeshProUGUI currentLevelText;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }
    private void Start()
    {
       
        pauseCanvas.SetActive(false);
        currentLevelText.text = currentLevel.ToString();
        
    }
    public void PauseisActive()
    {
        pauseCanvas.SetActive(true);
        isPaused = true;
        Time.timeScale = 0f;        
    }

    public void PauseisNotActive()
    {
        AudioManager.instance.PlaySFX(atrasFx);
        pauseCanvas.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;        
    }
    public void LevelCompleted()
    {
        Save();
        LevelTransitionController.instance.StartTransition(14,2);
    }
    public void LevelSelectorButton()
    {
        Time.timeScale = 1;
        LevelTransitionController.instance.StartTransition(15, 2);
    }
    public void Save()
    {
        Debug.Log("GAME SAVED");
        PlayerPrefs.SetInt("HighScore_Fiki", maxscore);
        PlayerPrefs.SetInt("Score_Fiki", score);
        PlayerPrefs.SetInt("CurrentLevel_Fiki", currentLevel);
        PlayerPrefs.SetInt("Lives_Fiki", lives);
    }
    public void Load()
    {
        Debug.Log("GAME LOAD");
        maxscore = PlayerPrefs.GetInt("HighScore_Fiki", 0);
        score = PlayerPrefs.GetInt("Score_Fiki", 0);
        currentLevel = PlayerPrefs.GetInt("CurrentLevel_Fiki", 1);
        lives = PlayerPrefs.GetInt("Lives_Fiki", 0);

        Debug.Log("HighScore_Fiki " + maxscore);
        Debug.Log("Score_Fiki " + score);
        Debug.Log("Lives_Fiki " + lives);
        Debug.Log("Level_Fiki " + currentLevel);
    }

    public void GoToMenu()
    {
        Time.timeScale = 1;
        Save(); // revisqar com posar player`perfs o q 
        SceneManager.LoadScene("Lobby");
    }

}
