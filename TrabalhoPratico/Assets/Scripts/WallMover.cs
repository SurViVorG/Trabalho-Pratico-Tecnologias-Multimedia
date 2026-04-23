using UnityEngine;

public class WallMover : MonoBehaviour
{
    [Header("Configuração")]
    public Vector3 moveDirection = Vector3.back; // direção de avanço para o centro
    public float speed = 5f;
    public float destroyZ = 12f; // distância a que a parede é destruída

    [Header("Buraco")]
    public Transform wallLeft;
    public Transform wallRight;
    public float holeSize = 2.5f;  // largura do buraco
    public float arenaHalfWidth = 9f;


    void Start()
    {
        SetRandomHole();
    }

    void Update()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);

        // Destrói a parede quando sai da arena
        float dist = Mathf.Max(
            Mathf.Abs(transform.position.x),
            Mathf.Abs(transform.position.z)
        );
        if (dist > destroyZ)
        {
            Destroy(gameObject);
        }
    }

    void SetRandomHole()
    {
        // Posição aleatória do buraco ao longo da largura da parede
        float holeCenter = Random.Range(-arenaHalfWidth + holeSize, arenaHalfWidth - holeSize);

        float leftWidth  = (holeCenter - holeSize / 2f) + arenaHalfWidth;
        float rightWidth = arenaHalfWidth - (holeCenter + holeSize / 2f);

        float leftCenter  = -arenaHalfWidth + leftWidth / 2f;
        float rightCenter = arenaHalfWidth - rightWidth / 2f;

        // Aplica aos cubos filhos
        // Deteta se a parede é horizontal (Norte/Sul) ou vertical (Este/Oeste)
        bool isNS = (Mathf.Abs(moveDirection.z) > 0.5f);

        if (isNS)
        {
            wallLeft.localPosition  = new Vector3(leftCenter,  0, 0);
            wallLeft.localScale     = new Vector3(leftWidth,   2, 1);
            wallRight.localPosition = new Vector3(rightCenter, 0, 0);
            wallRight.localScale    = new Vector3(rightWidth,  2, 1);
        }
        else // Este/Oeste — parede ao longo do eixo Z
        {
            wallLeft.localPosition  = new Vector3(0, 0, leftCenter);
            wallLeft.localScale     = new Vector3(1, 2, leftWidth);
            wallRight.localPosition = new Vector3(0, 0, rightCenter);
            wallRight.localScale    = new Vector3(1, 2, rightWidth);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.LoseLife();
            Destroy(gameObject);
        }
    }
}