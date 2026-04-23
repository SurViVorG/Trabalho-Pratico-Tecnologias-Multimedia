using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    // ID da personagem selecionada guardado entre cenas
    public static int selectedCharacter = 0;

    public void SelectCharacter(int id)
    {
        selectedCharacter = id;
        Debug.Log("Personagem selecionada: " + id);
    }
}