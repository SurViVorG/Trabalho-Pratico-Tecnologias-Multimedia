using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 8f;
    private Rigidbody rb;
    private float arenaLimit = 8f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

    // Muda a cor conforme a personagem escolhida
        Renderer rend = GetComponent<Renderer>();
        if (rend != null)
        {
            switch (CharacterSelector.selectedCharacter)
            {
                case 0: rend.material.color = Color.blue;   break;
                case 1: rend.material.color = Color.red;    break;
                case 2: rend.material.color = Color.green;  break;
            }
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

    // Movimento relativo à direção do jogador
    Vector3 movement = (transform.forward * input.y + transform.right * input.x).normalized;
    movement.y = 0f;

    Vector3 newPos = rb.position + movement * moveSpeed * Time.fixedDeltaTime;

    newPos.x = Mathf.Clamp(newPos.x, -arenaLimit, arenaLimit);
    newPos.z = Mathf.Clamp(newPos.z, -arenaLimit, arenaLimit);
    newPos.y = 1f;

    rb.MovePosition(newPos);
}
}