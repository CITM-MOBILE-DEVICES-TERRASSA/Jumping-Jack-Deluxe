using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinSensorScript : MonoBehaviour
{
    public GameObject winScreen;
    private bool timePaused = false;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !timePaused)
        {
            winScreen.SetActive(true);
            GameManagerDeluxe.instance.pauseGame();
            timePaused = true;
        }
    }
}
