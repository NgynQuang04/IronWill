using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Drifter : MonoBehaviour
{
    [Header("Movement")]
    public float initialImpulse = 3f;
    public float maxSpeed = 12f;

    [Header("Direction")]
    public Vector2 moveDirection = Vector2.right;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        Vector2 dir = moveDirection.normalized;

        rb.AddForce(dir * initialImpulse, ForceMode2D.Impulse);
    }

    void FixedUpdate()
    {
        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity =
                rb.linearVelocity.normalized * maxSpeed;
        }
    }
}