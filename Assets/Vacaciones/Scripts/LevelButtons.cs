using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButtons : MonoBehaviour
{
    public GameObject levelButton;
    public void PlayLevelOne()
    {
        
        SceneLoader.Instance.LoadGameplay();
    }
    public void PlayLevel2()
    {
        
        SceneLoader.Instance.LoadScene("Level 2");
    }

    public void ReturnToLobby()
    {
        SceneLoader.Instance.LoadMainLobby();
    }

}
