using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonCamera : MonoBehaviour
{
    [Header("Câmara")]
    public Transform playerBody;
    public float mouseSensitivity = 0.3f;

    private float xRotation = 0f;

    void Start()
    {
        // Esconde e bloqueia o cursor no centro do ecrã
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Mouse.current == null) return;

        float mouseX = Mouse.current.delta.x.ReadValue() * mouseSensitivity;
        float mouseY = Mouse.current.delta.y.ReadValue() * mouseSensitivity;

        // Rotação vertical da câmara (olhar para cima/baixo)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f); // limita para não virar ao contrário
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotação horizontal do corpo do jogador
        playerBody.Rotate(Vector3.up * mouseX);
    }
}