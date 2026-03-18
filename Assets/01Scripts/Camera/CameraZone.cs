using UnityEngine;

public class CameraZone : MonoBehaviour
{
    [Header("Zone Size")]
    public Vector2 size = new Vector2(20, 12);

    public Vector3 GetCenter()
    {
        return transform.position;
    }

    public Vector3 ClampPosition(Vector3 camPos)
    {
        Vector3 center = transform.position;
        float halfW = size.x / 2;
        float halfH = size.y / 2;

        camPos.x = Mathf.Clamp(camPos.x, center.x - halfW, center.x + halfW);
        camPos.y = Mathf.Clamp(camPos.y, center.y - halfH, center.y + halfH);

        return camPos;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, size);
    }
}