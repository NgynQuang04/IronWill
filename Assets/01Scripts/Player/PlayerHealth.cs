using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private bool isDead;

    public void Kill()
    {
        if (isDead)
            return;

        isDead = true;

        Debug.Log("Player died");

        GameEvents.OnPlayerDeath?.Invoke();

        gameObject.SetActive(false);
    }
}