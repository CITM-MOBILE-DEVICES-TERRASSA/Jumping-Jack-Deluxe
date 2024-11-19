using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    #region Singleton
    private static GameplayManager _instance;
    public static GameplayManager Instance => _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    #endregion

    public GameObject gameOverScreen;

    int currentScore = 0;

    public void ReturnToLobby()
    {
        SceneManager.LoadScene("Lobby");
        int totalScore = PlayerPrefs.GetInt("totalScore", 0);
        PlayerPrefs.SetInt("totalScore", totalScore + currentScore);
    }

    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        ResetTimer();
    }

    public void LoadMetaLobby()
    {
        SceneManager.LoadScene("Meta1");
    }

    public void LoadNextLevel()
    {
        // Guardamos el puntaje acumulado del nivel antes de cambiar de escena
        Score score = FindObjectOfType<Score>();
        if (score != null)
        {
            score.AddScoreToTotal();  // Añadimos la puntuación del nivel al total
        }

        // Reiniciamos el temporizador para el siguiente nivel
        ResetTimer();

        // Cargar el siguiente nivel
        if (SceneManager.GetActiveScene().name == "Level1")
        {
            SceneManager.LoadScene("Level2");
        }
        else if (SceneManager.GetActiveScene().name == "Level2")
        {
            SceneManager.LoadScene("Level3");
        }
    }

    private void ResetTimer()
    {
        Timer timer = FindObjectOfType<Timer>();
        if (timer != null)
        {
            timer.ResetTimer(); // Reinicia el temporizador
        }
    }
}
