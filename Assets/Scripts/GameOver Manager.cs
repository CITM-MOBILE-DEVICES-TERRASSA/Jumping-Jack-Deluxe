using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public void RestartLevel()
    {
        string lastLevel = PlayerPrefs.GetString("lastLevel", null); // el ultimo nivel 
        if (!string.IsNullOrEmpty(lastLevel))
        {
            SceneManager.LoadScene(lastLevel); // Carga el nivel 
            ResetTimer();
        }
        else
        {
            Debug.LogWarning("No se encontró el nivel anterior.");
        }
    }

    public void GoToLobby()
    {
        SceneManager.LoadScene("Lobby");
    }
    private void ResetTimer()
    {
        Timer timer = FindObjectOfType<Timer>();
        if (timer != null)
        {
            timer.ResetTimer();
        }
    }
}
