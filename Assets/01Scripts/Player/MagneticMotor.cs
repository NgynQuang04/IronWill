using UnityEngine;

public class MagneticMotor : MonoBehaviour
{
    public float magneticStrength = 50f;
    public float minDistance = 1f;
    public float maxForce = 4f;

    [Range(0f, 1f)]
    public float orbitStrength = 0.3f;

    [Range(0f, 1f)]
    public float steerStrength = 0.75f;

    public void ApplyMagnetForce(
        Rigidbody2D rb,
        MagneticTargetDetector detector,
        PlayerInput input)
    {
        if (!detector.HasTarget) return;

        var hit = detector.CurrentHit;

        Rigidbody2D targetRb = hit.collider.attachedRigidbody;
        if (!targetRb) return;

        float distance = Mathf.Max(hit.distance, minDistance);

        float t = 1f - (distance / 8f);
        float force = magneticStrength * t * t;
        force = Mathf.Min(force, maxForce);

        Vector2 baseDir = (hit.point - rb.position).normalized;

        Vector2 steeredDir =
            Vector2.Lerp(baseDir, input.MouseDirection, steerStrength).normalized;

        Vector2 perpendicular =
            new Vector2(-steeredDir.y, steeredDir.x);

        Vector2 finalDir =
            (steeredDir + perpendicular * orbitStrength).normalized;

        Vector2 forceVector = Vector2.zero;

        if (input.Pull)
            forceVector = finalDir * force;

        if (input.Push)
            forceVector = -finalDir * force;

        rb.AddForce(forceVector);

        if (targetRb.bodyType == RigidbodyType2D.Dynamic)
        {
            targetRb.AddForce(-forceVector);
        }
    }
}