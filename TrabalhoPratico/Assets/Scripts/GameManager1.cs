using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Singleton
    public static GameManager Instance;

    [Header("UI")]
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI livesText;
    public GameObject gameOverPanel;
    public TextMeshProUGUI finalWaveText;

    [Header("Jogo")]
    public int lives = 3;

    [HideInInspector] public bool isGameOver = false;
    private int currentWave = 0;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
            // Aplica o volume guardado
        float savedVolume = PlayerPrefs.GetFloat("GameVolume", 0.5f);
        AudioListener.volume = savedVolume;

        gameOverPanel.SetActive(false);
        UpdateUI();
    }

    public void AddWave()
    {
        currentWave++;
        UpdateUI();
    }

    public void LoseLife()
    {
        if (isGameOver) return;

        lives--;
        UpdateUI();

        if (lives <= 0)
        {
            GameOver();
        }
        else
        {
            // Feedback visual (opcional: piscar o ecrã)
            Debug.Log("Perdeu uma vida! Restam: " + lives);
        }
    }

    void GameOver()
    {
        isGameOver = true;

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayGameOver();

        // Guarda o score
        if (HighscoreManager.Instance != null)
            HighscoreManager.Instance.SaveScore(currentWave);

        gameOverPanel.SetActive(true);
        finalWaveText.text = "Sobreviveste " + currentWave + " ondas!";
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        WallMover[] walls = FindObjectsByType<WallMover>(FindObjectsSortMode.None);
        foreach (WallMover w in walls) w.enabled = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void GoToMainMenu()
    {
    SceneManager.LoadScene("MainMenu");
    }

    void UpdateUI()
    {
        if (waveText  != null) waveText.text  = "Onda: "  + currentWave;
        if (livesText != null) livesText.text = "Vidas: " + lives;
    }
}