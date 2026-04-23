using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject characterSelectPanel;
    public GameObject highscorePanel;

    void Start()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (mainPanel != null)            mainPanel.SetActive(true);
        if (characterSelectPanel != null) characterSelectPanel.SetActive(false);
        if (highscorePanel != null)       highscorePanel.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OpenCharacterSelect()
    {
        mainPanel.SetActive(false);
        characterSelectPanel.SetActive(true);
    }

    public void OpenHighscores()
    {
        mainPanel.SetActive(false);
        highscorePanel.SetActive(true);

        HighscoreManager hs = FindFirstObjectByType<HighscoreManager>();
        if (hs != null) hs.ShowScores();
    }

    public void BackToMain()
    {
        mainPanel.SetActive(true);
        characterSelectPanel.SetActive(false);
        highscorePanel.SetActive(false);
    }
}