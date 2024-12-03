using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEditor.SearchService;

public class LoadingBar : MonoBehaviour
{
    public Image loadingBarFill;  // Reference to the Image UI component you created for the fill

    private void Start()
    {
        StartCoroutine(SimulateLoading());
    }

    IEnumerator SimulateLoading()
    {
        float progress = 0f;

        while (progress < 1f)
        {
            progress += Time.deltaTime / 2f;  // Adjust 5f to control the loading speed
            loadingBarFill.fillAmount = progress; // Set the fill amount of the image

            yield return null;
        }
        SceneLoader.Instance.LoadMainLobby();
    }
}
