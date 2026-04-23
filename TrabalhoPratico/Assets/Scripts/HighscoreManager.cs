using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class HighscoreManager : MonoBehaviour
{
    public static HighscoreManager Instance;

    [Header("UI")]
    public TextMeshProUGUI scoresText;

    private const string KEY = "Highscores";
    private const int MAX_SCORES = 5;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public void SaveScore(int waves)
    {
        List<int> scores = GetScores();
        scores.Add(waves);
        scores.Sort((a, b) => b.CompareTo(a)); // ordena decrescente
        if (scores.Count > MAX_SCORES)
            scores.RemoveRange(MAX_SCORES, scores.Count - MAX_SCORES);

        // Guarda como string separada por vírgulas
        string data = string.Join(",", scores);
        PlayerPrefs.SetString(KEY, data);
        PlayerPrefs.Save();
    }

    public List<int> GetScores()
    {
        List<int> scores = new List<int>();
        string data = PlayerPrefs.GetString(KEY, "");
        if (data == "") return scores;

        foreach (string s in data.Split(','))
        {
            if (int.TryParse(s, out int val))
                scores.Add(val);
        }
        return scores;
    }

    public void ShowScores()
    {
        if (scoresText == null) return;
        List<int> scores = GetScores();

        if (scores.Count == 0)
        {
            scoresText.text = "Ainda não há pontuações!";
            return;
        }

        string result = "";
        for (int i = 0; i < scores.Count; i++)
            result += (i + 1) + "º  —  " + scores[i] + " ondas\n";

        scoresText.text = result;
    }
}