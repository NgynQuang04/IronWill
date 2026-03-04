using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MagneticMovement : MonoBehaviour
{
    [Header("Magnetic Settings")]
    public float magneticRadius = 8f;
    public float magneticStrength = 50f;
    public float minDistance = 1f;
    public float maxForce = 4f;

    [Header("Orbit Boost")]
    [Range(0f, 1f)]
    public float orbitStrength = 0.3f;

    [Header("Steering")]
    [Range(0f, 1f)]
    public float steerStrength = 0.75f;

    [Header("Speed Limit")]
    public float maxSpeed = 15f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask groundLayer;

    public LayerMask metalLayer;

    private Rigidbody2D rb;
    private Camera cam;
    private bool isGrounded;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;

        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    void FixedUpdate()
    {
        HandleMagnet();
        CheckGround();
        ApplySoftSpeedLimit();
    }

    void HandleMagnet()
    {
        Vector2 mouseWorld = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mouseDir = (mouseWorld - rb.position).normalized;

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

        // ===== BASE DIRECTION =====
        Vector2 baseDir = (hit.point - rb.position).normalized;

        // ===== STEERING =====
        Vector2 steeredDir = Vector2.Lerp(baseDir, mouseDir, steerStrength).normalized;

        // ===== ORBIT BOOST =====
        Vector2 perpendicular = new Vector2(-steeredDir.y, steeredDir.x);
        Vector2 finalDir = (steeredDir + perpendicular * orbitStrength).normalized;

        Vector2 forceVector = Vector2.zero;

        if (Input.GetMouseButton(0))       // HÚT
            forceVector = finalDir * force;

        if (Input.GetMouseButton(1))       // ĐẨY
            forceVector = -finalDir * force;

        rb.AddForce(forceVector, ForceMode2D.Force);

        if (targetRb.bodyType == RigidbodyType2D.Dynamic)
        {
            targetRb.AddForce(-forceVector, ForceMode2D.Force);
        }
    }

    void ApplySoftSpeedLimit()
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

    void CheckGround()
    {
        if (groundCheck == null) return;

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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, magneticRadius);

        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
        }
    }
}