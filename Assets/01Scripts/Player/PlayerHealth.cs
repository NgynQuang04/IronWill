using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    bool isDead;

    public void Kill()
    {
        if (isDead) return;

        isDead = true;

        GameEvents.OnPlayerDeath?.Invoke();

        gameObject.SetActive(false);
    }
}