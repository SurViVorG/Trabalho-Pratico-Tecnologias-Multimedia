using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject characterSelectPanel;
    public GameObject highscorePanel;
    public GameObject mainPanel;

    void Start()
    {
        mainPanel.SetActive(true);
        characterSelectPanel.SetActive(false);
        highscorePanel.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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
        FindFirstObjectByType<HighscoreManager>().ShowScores();
    }

    public void BackToMain()
    {
        mainPanel.SetActive(true);
        characterSelectPanel.SetActive(false);
        highscorePanel.SetActive(false);
    }
}