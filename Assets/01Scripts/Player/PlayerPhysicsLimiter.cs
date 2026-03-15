using UnityEngine;

public class PlayerPhysicsLimiter : MonoBehaviour
{
    public float maxSpeed = 15f;
    public float deceleration = 0.2f;

    public void Apply(Rigidbody2D rb)
    {
        Vector2 velocity = rb.linearVelocity;

        if (velocity.magnitude > maxSpeed)
        {
            velocity = velocity.normalized * maxSpeed;
        }

        // giảm tốc dần theo thời gian
        velocity = Vector2.Lerp(
            velocity,
            Vector2.zero,
            deceleration * Time.fixedDeltaTime
        );

        rb.linearVelocity = velocity;
    }
}