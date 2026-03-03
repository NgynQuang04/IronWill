using UnityEngine;
using UnityEngine.UI;

public class HealthUISmooth : MonoBehaviour
{
    public PlayerHealth playerHealth;

    public Image instantFill;   // thanh ??
    public Image delayFill;     // thanh vàng

    public float smoothSpeed = 3f;

    float targetFill;

    void Update()
    {
        float percent =
            playerHealth.currentHealth /
            playerHealth.maxHealth;

        // Thanh ?? t?t ngay
        instantFill.fillAmount = percent;

        // Thanh vàng tr??t t? t?
        targetFill = percent;

        delayFill.fillAmount = Mathf.Lerp(
            delayFill.fillAmount,
            targetFill,
            Time.deltaTime * smoothSpeed
        );
    }
}