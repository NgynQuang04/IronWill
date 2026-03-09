using UnityEngine;

public class PlayerPhysicsLimiter : MonoBehaviour
{
    public float maxSpeed = 15f;

    public void Apply(Rigidbody2D rb)
    {
        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = Vector2.Lerp(
                rb.linearVelocity,
                rb.linearVelocity.normalized * maxSpeed,
                0.15f
            );
        }
    }
}