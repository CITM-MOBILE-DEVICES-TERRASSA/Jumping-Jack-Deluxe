using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadlyObstacle : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Guarda el nombre del nivel antes de cargar la escena de GameOver
            PlayerPrefs.SetString("lastLevel", SceneManager.GetActiveScene().name);
            SceneManager.LoadScene("GameOver");
        }
    }
}
