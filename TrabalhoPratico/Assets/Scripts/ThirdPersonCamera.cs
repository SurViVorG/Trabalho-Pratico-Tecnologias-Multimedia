using UnityEngine;
using UnityEngine.InputSystem;

public class SubwaySurfersCamera : MonoBehaviour
{
    [Header("Alvo")]
    public Transform target;

    [Header("Distância e altura")]
    public float distance = 4f;
    public float height = 3f;

    [Header("Rato")]
    public float mouseSensitivity = 0.2f;
    public float minVertical = -10f;
    public float maxVertical = 50f;

    [Header("Suavidade")]
    public float smoothSpeed = 10f;

    private float yaw = 0f;
    private float pitch = 15f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        if (target == null) return;
        if (Time.timeScale == 0f) return;
        if (Mouse.current == null) return;

        // Input do rato
        float mouseX = Mouse.current.delta.x.ReadValue() * mouseSensitivity;
        float mouseY = Mouse.current.delta.y.ReadValue() * mouseSensitivity;

        yaw   += mouseX;
        pitch -= mouseY;
        pitch  = Mathf.Clamp(pitch, minVertical, maxVertical);

        // Calcula posição atrás e acima do jogador
        Quaternion rotation    = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 offset         = rotation * new Vector3(0f, 0f, -distance);
        Vector3 targetPos      = target.position + Vector3.up * height;
        Vector3 desiredPos     = targetPos + offset;

        // Suaviza o movimento
        transform.position = Vector3.Lerp(transform.position, desiredPos, smoothSpeed * Time.deltaTime);

        // Câmara olha para o jogador
        transform.LookAt(target.position + Vector3.up * 1.5f);

        // Roda o jogador com o rato (só horizontal)
        target.rotation = Quaternion.Euler(0f, yaw, 0f);
    }
}