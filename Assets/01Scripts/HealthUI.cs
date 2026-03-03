using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public Image healthFill;

    float maxWidth;

    void Start()
    {
        maxWidth = healthFill.rectTransform.sizeDelta.x;
    }

    void Update()
    {
        float percent = playerHealth.currentHealth / playerHealth.maxHealth;

        healthFill.rectTransform.sizeDelta =
            new Vector2(maxWidth * percent,
                        healthFill.rectTransform.sizeDelta.y);
    }
}