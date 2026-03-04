using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Drifter : MonoBehaviour
{
    [Header("Movement")]
    public float initialImpulse = 3f;
    public float maxSpeed = 12f;

    [Header("Direction")]
    public bool moveRight = true;   // tick = sang phải, bỏ tick = sang trái

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        Vector2 dir = moveRight ? Vector2.right : Vector2.left;

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