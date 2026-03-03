using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Drifter : MonoBehaviour
{
    public float initialImpulse = 3f;
    public float maxSpeed = 12f;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Vector2 randomDir = Random.insideUnitCircle.normalized;
        rb.AddForce(randomDir * initialImpulse, ForceMode2D.Impulse);
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