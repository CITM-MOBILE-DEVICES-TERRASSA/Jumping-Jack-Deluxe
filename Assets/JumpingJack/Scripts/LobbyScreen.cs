using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LobbyScreen : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int totalScore = 0;

    private void Start()
    {
        totalScore = PlayerPrefs.GetInt("totalScore", 0) + /* Jumping Jack */
        PlayerPrefs.GetInt("MaxTotalGame2", 0) + /* Vacaciones */
        PlayerPrefs.GetInt("Score", 0) + /* Colors Magic */ 
        (PlayerPrefs.GetInt("MaxAutumnScore", 0) + PlayerPrefs.GetInt("MaxHalloweenScore", 0) + PlayerPrefs.GetInt("MaxSpringScore", 0) + PlayerPrefs.GetInt("MaxWinterScore", 0) + PlayerPrefs.GetInt("MaxSummerScore", 0)); /* Felix Jump*/ 
        //PlayerPrefs /* Fiki Jump */

        UpdateScoreDisplay();
    }

    public void SelectMinigame(string minigameName)
    {
        SceneManager.LoadScene(minigameName);
    }

    private void UpdateScoreDisplay()
    {
        scoreText.text = "TOTAL SCORE: " + totalScore;
    }


}
