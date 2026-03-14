using UnityEngine;

public class MagneticFieldZone : MonoBehaviour
{
    [Header("Force Settings")]
    [SerializeField] private bool attract = true;
    [SerializeField] private float forceStrength = 10f;

    [Header("Movement Limit")]
    [SerializeField] private float maxSpeed = 6f;

    [Header("Safety")]
    [SerializeField] private float minDistance = 0.3f;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        if (rb == null)
            return;

        ApplyMagneticForce(rb);
    }

    private void ApplyMagneticForce(Rigidbody2D rb)
    {
        Vector2 direction = attract ? -transform.up : transform.up;

        float distance = Vector2.Distance(transform.position, rb.position);

        if (distance < minDistance)
        {
            return;
        }
            

        rb.AddForce(direction * forceStrength);

        LimitSpeed(rb);
    }

    private void LimitSpeed(Rigidbody2D rb)
    {
        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity =
                rb.linearVelocity.normalized * maxSpeed;
        }
    }
}