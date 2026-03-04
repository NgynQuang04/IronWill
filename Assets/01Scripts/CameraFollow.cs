using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    [Header("Follow")]
    public float followSmoothTime = 0.15f;

    [Header("Zoom Base")]
    public float normalSize = 5f;
    public float zoomedOutSize = 8f;
    public float zoomOutSpeedThreshold = 10f;

    [Header("Zoom Smooth")]
    public float zoomInSmoothTime = 0.4f;
    public float zoomOutSmoothTime = 0.15f;

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

        // ================= FOLLOW =================
        Vector3 targetPosition = new Vector3(
            player.position.x,
            player.position.y,
            -10f
        );

        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref velocity,
            followSmoothTime
        );

        // ================= ZOOM =================
        float speed = playerRb.linearVelocity.magnitude;

        float targetSize = normalSize;

        if (speed > zoomOutSpeedThreshold)
        {
            targetSize = zoomedOutSize;
        }

        float smoothTime = (targetSize > cam.orthographicSize)
            ? zoomOutSmoothTime   // zoom out nhanh
            : zoomInSmoothTime;   // zoom in chậm hơn

        cam.orthographicSize = Mathf.SmoothDamp(
            cam.orthographicSize,
            targetSize,
            ref zoomVelocity,
            smoothTime
        );
    }
}