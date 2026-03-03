using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MagneticMovement : MonoBehaviour
{
    [Header("Magnetic Settings")]
    public float magneticRadius = 10f;
    public float magneticStrength = 80f;
    public float minDistance = 1f;       // tránh lực vô hạn
    public float maxForce = 25f;         // clamp lực tối đa

    [Header("Brake")]
    public float brakePower = 4f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask groundLayer;

    bool isGrounded;

    public LayerMask metalLayer;

    Rigidbody2D rb;
    Camera cam;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }

    void FixedUpdate()
    {
        HandleMagnet();

        isGrounded = Physics2D.OverlapCircle(
        groundCheck.position,
        groundRadius,
        groundLayer
);
    }

    public bool IsGrounded()
    {
        return isGrounded;
    }

    void HandleMagnet()
    {
        Vector2 mouseWorld = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mouseDir = (mouseWorld - rb.position).normalized;

        // Vẽ tia debug
        Debug.DrawLine(
            rb.position,
            rb.position + mouseDir * magneticRadius,
            Color.cyan
        );

        RaycastHit2D hit = Physics2D.Raycast(
            rb.position,
            mouseDir,
            magneticRadius,
            metalLayer
        );

        if (!hit) return;

        Rigidbody2D targetRb = hit.collider.attachedRigidbody;
        if (targetRb == null) return;

        float distance = Mathf.Max(hit.distance, minDistance);

        float t = 1f - (distance / magneticRadius);
        float force = magneticStrength * t * t;
        force = Mathf.Min(force, maxForce);

        Vector2 forceVector = Vector2.zero;

        if (Input.GetMouseButton(0)) // HÚT
            forceVector = mouseDir * force;

        if (Input.GetMouseButton(1)) // ĐẨY
            forceVector = -mouseDir * force;

        rb.AddForce(forceVector, ForceMode2D.Force);

        if (targetRb.bodyType == RigidbodyType2D.Dynamic)
        {
            targetRb.AddForce(-forceVector, ForceMode2D.Force);
        }

        ApplySoftSpeedLimit();
    }

    void ApplySoftSpeedLimit()
    {
        float maxSpeed = 12f;

        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity =
                Vector2.Lerp(
                    rb.linearVelocity,
                    rb.linearVelocity.normalized * maxSpeed,
                    0.1f
                );
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, magneticRadius);
    }
}