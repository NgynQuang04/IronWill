using UnityEngine;

public class CameraZone : MonoBehaviour
{
    public Vector3 GetCenter()
    {
        return transform.position;
    }

    public Vector2 size = new Vector2(20, 12);

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        Gizmos.DrawWireCube(transform.position, size);
    }
}