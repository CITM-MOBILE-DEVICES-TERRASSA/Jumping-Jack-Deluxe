using UnityEngine;
using TMPro;

public class ScoreDeluxe : MonoBehaviour
{
    [SerializeField] private string scoreTextObjectName = "Score"; // Nombre del objeto de texto del marcador
    [SerializeField] private int currentLevel = 1; // Nivel actual, configurable desde el Inspector
    private TextMeshProUGUI scoreText; // Referencia al marcador
    private TimerDeluxe timer; // Referencia al temporizador
    private int levelScore; // Puntuación del nivel actual
    private int lastDisplayedScore = -1; // Para evitar actualizaciones innecesarias

    void Start()
    {
        timer = FindObjectOfType<TimerDeluxe>(); // Encuentra el temporizador
        InitializeScoreText(); // Inicializa el marcador
        ResetScore(); // Reinicia el marcador al empezar
    }

    void Update()
    {
        if (timer != null)
        {
            UpdateLevelScore(); // Actualiza la puntuación según el tiempo transcurrido
        }
    }

    private void UpdateLevelScore()
    {
        levelScore = (int)timer.totalTime;

        if (levelScore != lastDisplayedScore)
        {
            UpdateScoreDisplay();
            lastDisplayedScore = levelScore;
        }
    }

    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {levelScore}";
        }
    }

    public void ResetScore()
    {
        levelScore = 0;
        lastDisplayedScore = -1;
        UpdateScoreDisplay();
    }

    /// <summary>
    /// Llama a este método al completar el nivel para guardar la puntuación más alta automáticamente.
    /// </summary>
    public void SaveHighScore()
    {
        // Obtiene la puntuación más alta actual para este nivel
        int currentHighScore = PlayerPrefs.GetInt($"Level{currentLevel}HighScore", 0);

        // Si la puntuación actual es mayor, actualiza la puntuación más alta
        if (levelScore > currentHighScore)
        {
            PlayerPrefs.SetInt($"Level{currentLevel}HighScore", levelScore);
        }

        // Actualiza la puntuación total acumulada
        UpdateFinalTotalScore();
        PlayerPrefs.Save(); // Asegura que los datos se escriban en disco
    }

    private void UpdateFinalTotalScore()
    {
        int finalTotalScore = 0;

        // Suma todas las puntuaciones más altas almacenadas en PlayerPrefs
        foreach (int level in GetAllLevelNumbers())
        {
            finalTotalScore += PlayerPrefs.GetInt($"Level{level}HighScore", 0);
        }

        // Guarda la puntuación total actualizada
        PlayerPrefs.SetInt("finalTotalScore", finalTotalScore);
    }

    private int[] GetAllLevelNumbers()
    {
        // Devuelve un array con los números de los niveles
        // Ajusta este array según los niveles disponibles en tu juego
        return new int[] { 1, 2, 3, 4, 5 };
    }

    private void InitializeScoreText()
    {
        GameObject textObject = GameObject.Find(scoreTextObjectName);

        if (textObject != null)
        {
            scoreText = textObject.GetComponent<TextMeshProUGUI>();
        }
        else
        {
            Debug.LogWarning($"No se encontró el objeto TextMeshProUGUI con el nombre '{scoreTextObjectName}'.");
        }
    }
}
