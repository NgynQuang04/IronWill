using UnityEngine;

public class CameraZoneManager : MonoBehaviour
{
    public static CameraZoneManager Instance;

    [SerializeField] float moveSpeed = 5f;

    CameraZone currentZone;

    Vector3 targetPosition;

    Camera cam;

    void Awake()
    {
        Instance = this;
        cam = Camera.main;
    }

    void Update()
    {
        if (currentZone == null)
            return;

        Vector3 camPos = cam.transform.position;

        camPos = Vector3.Lerp(
            camPos,
            targetPosition,
            moveSpeed * Time.deltaTime
        );

        camPos.z = -10;

        cam.transform.position = camPos;
    }

    public void SetZone(CameraZone zone)
    {
        currentZone = zone;

        targetPosition = zone.GetCenter();
    }
}