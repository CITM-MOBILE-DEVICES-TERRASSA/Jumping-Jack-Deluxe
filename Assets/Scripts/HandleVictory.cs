using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandleVictory : MonoBehaviour
{
    [SerializeField]
    GameObject victoryScreen;

    [SerializeField]
    GameObject nextLevelButton;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")){
            victoryScreen.SetActive(true);
            if(SceneManager.GetActiveScene().name != "Level3")
            {
                nextLevelButton.SetActive(true);
            }
            else
            {
                nextLevelButton.SetActive(false);
            }
        }
    }
}
