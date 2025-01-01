using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerDeluxe : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
            // Opcional: Mostrar un menú de pausa o interfaz
            Debug.Log("Juego pausado");
        }
        else
        {
            // Reanudar el juego
            Time.timeScale = 1;
            // Opcional: Ocultar el menú de pausa
            Debug.Log("Juego reanudado");
        }
    }

    

    public void RestartGame()
    {
        // Reinicia la escena actual
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);

        // Opcional: Si usas Time.timeScale, asegúrate de restablecerlo
        Time.timeScale = 1;

        Debug.Log("Juego reiniciado");
    }

    public void GoHome()
    {
        SceneManager.LoadScene("LevelSelectorJJD");
        Time.timeScale = 1;
    }


}
