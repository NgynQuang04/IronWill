using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class magneticSwing : MonoBehaviour
{
    [Header("Swing Settings")]
    public LayerMask metalLayer;
    public float maxDistance = 25f;

    [Header("Arcade Feel")]
    public float gravityBoost = 1.2f;     // tăng tốc khi rơi
    public float releaseBoost = 1.1f;     // boost khi thả
    public float minSpeed = 8f;           // giữ tốc độ tối thiểu

    Rigidbody2D rb;

    bool swinging;
    Vector2 anchorPoint;
    float ropeLength;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            TryStartSwing();

        if (Input.GetMouseButtonUp(0))
            StopSwing();
    }

    void FixedUpdate()
    {
        if (swinging)
        {
            ApplyConstraint();
            ApplyArcadeBoost();
        }
    }

    void TryStartSwing()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = (mousePos - (Vector2)transform.position).normalized;

        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            dir,
            maxDistance,
            metalLayer);

        if (hit.collider != null)
        {
            swinging = true;
            anchorPoint = hit.point;
            ropeLength = Vector2.Distance(rb.position, anchorPoint);

            // khóa movement nếu có script di chuyển
            var move = GetComponent<PlayerMovement>();
            if (move != null)
                move.enabled = false;
        }
    }

    void StopSwing()
    {
        if (!swinging) return;

        swinging = false;

        // boost nhẹ khi thả
        rb.linearVelocity *= releaseBoost;

        var move = GetComponent<PlayerMovement>();
        if (move != null)
            move.enabled = true;
    }

    void ApplyConstraint()
    {
        Vector2 toPlayer = rb.position - anchorPoint;
        Vector2 radialDir = toPlayer.normalized;

        // 1️⃣ Ép player nằm trên vòng tròn
        rb.position = anchorPoint + radialDir * ropeLength;

        // 2️⃣ Xóa velocity hướng tâm
        Vector2 velocity = rb.linearVelocity;
        Vector2 radialVelocity = Vector2.Dot(velocity, radialDir) * radialDir;
        rb.linearVelocity -= radialVelocity;
    }

    void ApplyArcadeBoost()
    {
        Vector2 toPlayer = rb.position - anchorPoint;
        Vector2 radialDir = toPlayer.normalized;

        // vector tiếp tuyến
        Vector2 tangent = new Vector2(-radialDir.y, radialDir.x);

        // kiểm tra đang rơi xuống
        if (Vector2.Dot(rb.linearVelocity, Vector2.down) > 0)
        {
            rb.AddForce(tangent * gravityBoost, ForceMode2D.Force);
        }

        // giữ tốc độ tối thiểu
        if (rb.linearVelocity.magnitude < minSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * minSpeed;
        }
    }

    void OnDrawGizmos()
    {
        if (!swinging) return;

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(anchorPoint, transform.position);
        Gizmos.DrawSphere(anchorPoint, 0.2f);
    }
}