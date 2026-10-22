using UnityEngine;

public class KillZone : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // Sprawdzamy, czy to kula (musi mieć tag "Player")
        if (other.CompareTag("Player"))
        {
            // Wywołujemy utratę życia z GameManagera
            GameManager.Instance.LoseLife();
        }
    }
}