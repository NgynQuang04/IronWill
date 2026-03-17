using UnityEngine;

public class MagneticFieldZone : MonoBehaviour
{
    [Header("Force Settings")]
    [SerializeField] private bool attract = true;
    [SerializeField] private float forceStrength = 15f;

    [Header("Movement Limit")]
    [SerializeField] private float maxSpeed = 6f;

    [Header("Safety")]
    [SerializeField] private float minDistance = 0.3f;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        Rigidbody2D rb = other.attachedRigidbody;
        if (rb == null)
            return;

        ApplyMagneticForce(rb);
    }

    private void ApplyMagneticForce(Rigidbody2D rb)
    {
        Vector2 dir = attract ? -transform.up : transform.up;

        Vector2 toPlayer = rb.position - (Vector2)transform.position;

        // dùng sqrMagnitude nhanh hơn Distance
        if (toPlayer.sqrMagnitude < minDistance * minDistance)
            return;

        // force ổn định theo physics step
        rb.AddForce(dir * forceStrength * Time.fixedDeltaTime, ForceMode2D.Force);

        // clamp velocity mượt hơn
        rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, maxSpeed);
    }
}