using UnityEngine;

public class Trap : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        PlayerHealth player = other.GetComponent<PlayerHealth>();

        if (player != null)
            player.Kill();
    }
}