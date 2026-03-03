using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 0.1f;   // đổi từ 5f → 0.1f

    public float minSize = 5f;
    public float maxSize = 8f;
    public float zoomMultiplier = 1f;

    private Camera cam;
    private Rigidbody2D playerRb;

    private Vector3 velocity = Vector3.zero;
    private float zoomVelocity = 0f;

    void Start()
    {
        cam = GetComponent<Camera>();
        playerRb = player.GetComponent<Rigidbody2D>();
    }

    void LateUpdate()
    {
        if (player == null) return;

        // FOLLOW (SmoothDamp thay vì Lerp)
        Vector3 targetPosition = new Vector3(
            player.position.x,
            player.position.y,
            -10f
        );

        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref velocity,
            smoothSpeed
        );

        // ZOOM (SmoothDamp thay vì Lerp)
        float speed = playerRb.linearVelocity.magnitude;
        float targetSize = minSize + speed * zoomMultiplier;
        targetSize = Mathf.Clamp(targetSize, minSize, maxSize);

        cam.orthographicSize = Mathf.SmoothDamp(
            cam.orthographicSize,
            targetSize,
            ref zoomVelocity,
            0.3f
        );
    }
}