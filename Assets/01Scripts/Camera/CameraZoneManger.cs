using UnityEngine;

public class CameraZoneManager : MonoBehaviour
{
    public static CameraZoneManager Instance;

    [Header("Start Zone")]
    [SerializeField] CameraZone startZone;

    [Header("Transition Settings")]
    [SerializeField] float smoothTime = 0.5f;

    Camera cam;
    CameraZone currentZone;

    Vector3 velocity;
    bool isTransitioning = false;
    Vector3 targetPosition;

    void Awake()
    {
        Instance = this;
        cam = Camera.main;
    }

    void Start()
    {
        if (startZone != null)
        {
            SetZone(startZone, true);
        }
    }

    void LateUpdate()
    {
        if (isTransitioning)
        {
            cam.transform.position = Vector3.SmoothDamp(
                cam.transform.position,
                targetPosition,
                ref velocity,
                smoothTime
            );

            if (Vector3.Distance(cam.transform.position, targetPosition) < 0.05f)
                isTransitioning = false;
        }
    }

    // 🎯 Chuyển camera sang zone mới
    public void SetZone(CameraZone zone, bool instant = false)
    {
        if (zone == null || zone == currentZone) return;

        currentZone = zone;

        Vector3 center = zone.GetCenter();
        targetPosition = new Vector3(center.x, center.y, -10);

        if (instant)
        {
            cam.transform.position = targetPosition;
            isTransitioning = false;
        }
        else
        {
            isTransitioning = true;
        }
    }

    // Clamp camera trong zone
    public Vector3 ClampToCurrentZone(Vector3 pos)
    {
        if (currentZone == null) return pos;
        return currentZone.ClampPosition(pos);
    }

    // 🎨 Gizmos
    void OnDrawGizmos()
    {
        if (Camera.main == null) return;
        Gizmos.color = Color.red;

#if UNITY_EDITOR
        if (currentZone != null)
            Gizmos.DrawWireCube(currentZone.GetCenter(), currentZone.size);
#endif
    }
}