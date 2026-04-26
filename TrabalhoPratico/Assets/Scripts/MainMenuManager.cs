using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject characterSelectPanel;
    public GameObject highscorePanel;
    public AudioSource musicSource;
    public UnityEngine.UI.Slider volumeSlider;

    void Start()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (mainPanel != null)            mainPanel.SetActive(true);
        if (characterSelectPanel != null) characterSelectPanel.SetActive(false);
        if (highscorePanel != null)       highscorePanel.SetActive(false);

        // Carrega o volume guardado
        float savedVolume = PlayerPrefs.GetFloat("GameVolume", 0.5f);
        if (volumeSlider != null) volumeSlider.value = savedVolume;
        AudioListener.volume = savedVolume;

        // Liga o slider ao método
        if (volumeSlider != null)
            volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat("GameVolume", value);
        PlayerPrefs.Save();
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