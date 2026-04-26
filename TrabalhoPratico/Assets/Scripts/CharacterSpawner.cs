//Este script vai ao Player e carrega o modelo correto conforme a escolha:
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    [Header("Modelos das personagens")]
    public GameObject[] characterPrefabs; // arrasta os 4 prefabs aqui por ordem

    private GameObject currentModel;

    void Start()
    {
        int id = PlayerPrefs.GetInt("SelectedCharacter", 0);
        id = Mathf.Clamp(id, 0, characterPrefabs.Length - 1);

        // Instancia o modelo escolhido dentro do Player
        currentModel = Instantiate(characterPrefabs[id], transform);
        currentModel.transform.localPosition = new Vector3(0f, -1f, 0f);
        currentModel.transform.localRotation = Quaternion.identity;

        // Remove componentes que conflituam com o movimento
        Destroy(currentModel.GetComponent<CharacterController>());

        // Procura e remove ThirdPersonController se existir
        MonoBehaviour[] scripts = currentModel.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour s in scripts)
        {
            string name = s.GetType().Name;
            if (name.Contains("Controller") || name.Contains("Input"))
                Destroy(s);
        }
    }
}