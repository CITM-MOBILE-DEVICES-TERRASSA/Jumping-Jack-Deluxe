using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerDeluxe : MonoBehaviour
{

    public static GameManagerDeluxe instance;
    private string[] levelSceneNames = new string[5] { "Level1JJD", "Level2JJD", "Level3JJD", "Level4JJD", "Level5JJD" };


    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pauseGame()
    {
        if (Time.timeScale == 1)
        {
            // Pausar el juego
            Time.timeScale = 0;
            // Opcional: Mostrar un men� de pausa o interfaz
            Debug.Log("Juego pausado");
        }
        else
        {
            // Reanudar el juego
            Time.timeScale = 1;
            // Opcional: Ocultar el men� de pausa
            Debug.Log("Juego reanudado");
        }
    }


    

    public void RestartGame()
    {
        // Reinicia la escena actual
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);

        // Opcional: Si usas Time.timeScale, aseg�rate de restablecerlo
        Time.timeScale = 1;

        Debug.Log("Juego reiniciado");
    }

    public void GoHome()
    {
        SceneManager.LoadScene("LevelSelectorJJD");
        Time.timeScale = 1;
    }

    public void LoadLevel(int levelIndex)
    {
        if (levelIndex >= 0 && levelIndex < levelSceneNames.Length)
        {
            SceneManager.LoadScene(levelSceneNames[levelIndex]);
            Time.timeScale = 1;
        }
        else
        {
            Debug.LogError("Invalid level index!");
        }
    }
}
