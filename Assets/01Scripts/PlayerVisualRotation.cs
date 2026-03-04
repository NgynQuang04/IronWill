using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerVisualRotation : MonoBehaviour
{
    [Header("References")]
    public Transform sprite;   // Child sprite

    [Header("Magnetic Sync Settings")]
    public float magneticRadius = 8f;
    public LayerMask metalLayer;

    [Header("Rotation Settings")]
    public float rotationSpeed = 10f;

    private Rigidbody2D rb;
    private Camera cam;

    private Vector2 currentLookDirection = Vector2.right;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }

    void Update()
    {
        HandleRotation();
    }

    void HandleRotation()
    {
        Vector2 targetDirection = Vector2.zero;

        Vector2 mouseWorld = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mouseDir = (mouseWorld - rb.position).normalized;

        RaycastHit2D hit = Physics2D.Raycast(
            rb.position,
            mouseDir,
            magneticRadius,
            metalLayer
        );

        bool isPulling = Input.GetMouseButton(0);
        bool isPushing = Input.GetMouseButton(1);

        if (hit && (isPulling || isPushing))
        {
            // ===== BASE DIRECTION =====
            Vector2 baseDir = (hit.point - rb.position).normalized;

            // Không c?n steer/orbit cho visual quá ph?c t?p
            // ch? c?n h??ng chính xác v? m?c tiêu

            if (isPulling)
                targetDirection = baseDir;
            else if (isPushing)
                targetDirection = -baseDir;
        }
        else if (rb.linearVelocity.magnitude > 0.1f)
        {
            // Không thao tác ? xoay theo velocity
            targetDirection = rb.linearVelocity.normalized;
        }

        if (targetDirection == Vector2.zero)
            return;

        // Xoay m??t
        currentLookDirection = Vector2.Lerp(
            currentLookDirection,
            targetDirection,
            Time.deltaTime * rotationSpeed
        );

        float angle = Mathf.Atan2(
            currentLookDirection.y,
            currentLookDirection.x
        ) * Mathf.Rad2Deg;

        sprite.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}