using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    public static int selectedCharacter = 0;

    public void SelectCharacter(int id)
    {
        selectedCharacter = id;
        PlayerPrefs.SetInt("SelectedCharacter", id);
        PlayerPrefs.Save();
    }

    void Awake()
    {
        selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter", 0);
    }
}