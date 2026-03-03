using UnityEngine;

public class MagneticBox : MonoBehaviour
{
    Rigidbody2D rb;

    bool beingMagnetized = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetMagnetState(bool state)
    {
        beingMagnetized = state;

        if (!beingMagnetized)
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (!beingMagnetized && collision.collider.CompareTag("Player"))
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
    }
}