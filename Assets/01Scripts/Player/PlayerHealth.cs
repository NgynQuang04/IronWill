using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [Header("Timing")]
    [SerializeField] private float deathDuration = 0.8f;
    [SerializeField] private float winDuration = 1.2f;

    [Header("Slow Motion")]
    [SerializeField] private float slowMotionScale = 0.3f;

    private bool isDead;
    private bool isWinning;

    private Rigidbody2D rb;
    private Collider2D col;
    private SpriteRenderer sr;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    // ===================== 💀 DEATH =====================
    public void Kill()
    {
        if (isDead || isWinning) return;

        isDead = true;
        StartCoroutine(DeathRoutine());
    }

    public bool IsDead () { return isDead; }

    IEnumerator DeathRoutine()
    {
        Time.timeScale = slowMotionScale;

        DisablePhysics();

        yield return AnimateScaleRotateFade(Vector3.zero, 360f, deathDuration);

        Time.timeScale = 1f;

        GameEvents.OnPlayerDeath?.Invoke();
        gameObject.SetActive(false);
    }

    // ===================== 🏆 WIN =====================
    public void Win()
    {
        if (isDead || isWinning) return;

        isWinning = true;
        StartCoroutine(WinRoutine());
    }

    IEnumerator WinRoutine()
    {
        Time.timeScale = slowMotionScale;

        DisablePhysics();

        // 👉 animation win: PHÓNG TO + xoay + fade nhẹ
        yield return AnimateScaleRotateFade(
            transform.localScale * 2f, // phóng to
            720f,                      // xoay nhiều hơn
            winDuration
        );

        Time.timeScale = 1f;

        GameEvents.OnGameWin?.Invoke();
        gameObject.SetActive(false);
    }

    // ===================== 🎬 ANIMATION CORE =====================
    IEnumerator AnimateScaleRotateFade(Vector3 targetScale, float rotateAmount, float duration)
    {
        float time = 0f;

        Vector3 startScale = transform.localScale;
        float startRot = transform.eulerAngles.z;
        float endRot = startRot + rotateAmount;

        Color startColor = sr.color;

        while (time < duration)
        {
            time += Time.unscaledDeltaTime;
            float t = time / duration;

            // 👉 ease-out mượt hơn
            t = 1 - Mathf.Pow(1 - t, 3);

            // scale
            transform.localScale = Vector3.Lerp(startScale, targetScale, t);

            // rotate
            float rot = Mathf.Lerp(startRot, endRot, t);
            transform.rotation = Quaternion.Euler(0, 0, rot);

            // fade (win fade nhẹ hơn death)
            if (sr != null)
            {
                Color c = startColor;
                c.a = Mathf.Lerp(1f, 0.3f, t);
                sr.color = c;
            }

            yield return null;
        }
    }

    void DisablePhysics()
    {
        rb.linearVelocity = Vector2.zero;
        rb.simulated = false;
        col.enabled = false;
    }
}