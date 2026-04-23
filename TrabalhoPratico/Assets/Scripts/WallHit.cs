using UnityEngine;

public class WallHit : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.LoseLife();
            Destroy(transform.parent.gameObject);
        }
    }
}