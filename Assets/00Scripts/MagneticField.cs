using UnityEngine;

public class MagneticField : MonoBehaviour
{
    [Header("Magnetic Settings")]
    public float magneticRadius = 6f;
    public float magneticForce = 120f;
    public float maxForceDistance = 0.3f;   // tránh giật khi quá gần
    public float maxSpeed = 6f;             // giới hạn tốc độ box
    public LayerMask metalLayer;

    // 1 = hút, -1 = đẩy, 0 = không tác động
    int polarity = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
            polarity = 1;

        if (Input.GetKeyDown(KeyCode.E))
            polarity = -1;

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.E))
            polarity = 0;
    }

    void FixedUpdate()
    {
        // Không có lực → không xử lý
        if (polarity == 0) return;

        Collider2D[] metals = Physics2D.OverlapCircleAll(
            transform.position,
            magneticRadius,
            metalLayer);

        foreach (Collider2D col in metals)
        {
            Rigidbody2D rb = col.attachedRigidbody;
            if (rb == null) continue;

            Vector2 dir = rb.position - (Vector2)transform.position;
            float distance = dir.magnitude;

            if (distance < maxForceDistance) continue;

            Vector2 forceDir = dir.normalized;

            // Chuẩn hóa khoảng cách
            float t = 1f - (distance / magneticRadius);
            t = Mathf.Clamp01(t);

            // Curve mượt (bình phương)
            float forceAmount = magneticForce * t * t;

            Vector2 finalForce = -forceDir * forceAmount * polarity;

            rb.AddForce(finalForce, ForceMode2D.Force);

            // Giới hạn tốc độ để không bắn quá nhanh
            rb.linearVelocity =
                Vector2.ClampMagnitude(rb.linearVelocity, maxSpeed);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, magneticRadius);
    }
}