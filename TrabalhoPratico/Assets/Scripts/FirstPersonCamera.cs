using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonCamera : MonoBehaviour
{
    [Header("Câmara")]
    public Transform playerBody;
    public float mouseSensitivity = 0.3f;
    public float distanceFromPlayer = 6f;
    public float heightOffset = 3f;

    private float yRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Mouse.current == null) return;

        float mouseX = Mouse.current.delta.x.ReadValue() * mouseSensitivity;
        yRotation += mouseX;

        // Posição atrás e acima do jogador
        Quaternion rotation = Quaternion.Euler(0, yRotation, 0);
        Vector3 offset = rotation * new Vector3(0, heightOffset, -distanceFromPlayer);
        transform.position = playerBody.position + offset;

        // Câmara olha sempre para o jogador
        transform.LookAt(playerBody.position + Vector3.up);

        // Roda o corpo do jogador com o rato
        playerBody.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}