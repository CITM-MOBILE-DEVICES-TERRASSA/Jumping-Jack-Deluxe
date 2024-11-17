using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadlyObstacle : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("Lobby"); // Para ir al lobby
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); Esto para reiniciar el nivel actual
        }
    }
}
