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
        gameOverPanel.SetActive(true);
        finalWaveText.text = "Sobreviveste " + currentWave + " ondas!";

        // Para todas as paredes em movimento
        WallMover[] walls = FindObjectsByType<WallMover>(FindObjectsSortMode.None);
        foreach (WallMover w in walls) w.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void UpdateUI()
    {
        if (waveText  != null) waveText.text  = "Onda: "  + currentWave;
        if (livesText != null) livesText.text = "Vidas: " + lives;
    }
}