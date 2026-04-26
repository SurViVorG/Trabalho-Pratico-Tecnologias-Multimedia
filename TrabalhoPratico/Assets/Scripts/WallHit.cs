using UnityEngine;

public class WallHit : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (AudioManager.Instance != null)
                AudioManager.Instance.PlayHit();

            GameManager.Instance.LoseLife();
            Destroy(transform.parent.gameObject);
        }
    }
}