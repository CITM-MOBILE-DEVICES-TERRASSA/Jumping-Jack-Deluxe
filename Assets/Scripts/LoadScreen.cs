using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SplashScreen : MonoBehaviour
{
    public Slider loadingBar;
    public float loadingSpeed = 0.5f;

    private void Start()
    {
        StartCoroutine(LoadLobbyScene());
    }

    private IEnumerator LoadLobbyScene()
    {
        float progress = 0;

        while (progress < 1f)
        {
            progress += Time.deltaTime * loadingSpeed;
            loadingBar.value = progress;
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene("Lobby");
    }
}
