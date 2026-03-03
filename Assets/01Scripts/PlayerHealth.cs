using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    [Header("Base Impact Damage")]
    public float damageThreshold = 2f;
    public float damageMultiplier = 5f;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        float impactForce = collision.relativeVelocity.magnitude;

        DamageSource source = collision.gameObject.GetComponent<DamageSource>();

        float damage = 0f;

        // Nếu object có DamageSource riêng
        if (source != null)
        {
            if (source.ignoreVelocity)
            {
                damage = source.flatDamage;
            }
            else
            {
                if (impactForce > damageThreshold)
                {
                    damage = (impactForce - damageThreshold)
                             * damageMultiplier
                             * source.baseDamageMultiplier;
                }
            }
        }
        else
        {
            // Ground mặc định
            if (impactForce > damageThreshold)
            {
                damage = (impactForce - damageThreshold) * damageMultiplier;
            }
        }

        if (damage > 0)
        {
            TakeDamage(damage);
        }
    }

    void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log("Damage: " + amount + " | HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}