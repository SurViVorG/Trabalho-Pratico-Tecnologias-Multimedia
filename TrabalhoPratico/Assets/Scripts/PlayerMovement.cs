using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movimento")]
    public float moveSpeed = 8f;
    public float rotationSpeed = 10f;

    [Header("Personagens")]
    public GameObject kyleModel;        // arrasta o RobotKyle aqui
    public GameObject survivalistModel; // arrasta o Survivalist aqui

    private Rigidbody rb;
    private float arenaLimit = 8f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        AplicarPersonagem();
    }

    void AplicarPersonagem()
    {
        int id = PlayerPrefs.GetInt("SelectedCharacter", 0);

        if (kyleModel != null)        kyleModel.SetActive(id == 0);
        if (survivalistModel != null) survivalistModel.SetActive(id == 1);
    }

    void Update()
    {
        if (Keyboard.current == null) return;

        // Tecla 1 — Kyle
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            PlayerPrefs.SetInt("SelectedCharacter", 0);
            AplicarPersonagem();
        }

        // Tecla 2 — Survivalist
        if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            PlayerPrefs.SetInt("SelectedCharacter", 1);
            AplicarPersonagem();
        }
    }

    void FixedUpdate()
    {
        Vector2 input = Vector2.zero;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed)    input.y =  1;
            if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed)  input.y = -1;
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) input.x =  1;
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)  input.x = -1;
        }

        if (input.magnitude > 0.1f)
        {
            // Direção relativa à rotação atual do jogador
            Vector3 movement = (transform.forward * input.y + transform.right * input.x).normalized;
            movement.y = 0f;

            // Rotação suave para a direção do movimento
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);

            // Move na direção para onde está virado
            Vector3 newPos = rb.position + movement * moveSpeed * Time.fixedDeltaTime;
            newPos.x = Mathf.Clamp(newPos.x, -arenaLimit, arenaLimit);
            newPos.z = Mathf.Clamp(newPos.z, -arenaLimit, arenaLimit);
            newPos.y = 1f;
            rb.MovePosition(newPos);
        }
    }
}