using UnityEngine;

public class CameraZoneTrigger : MonoBehaviour
{
    [SerializeField] CameraZone zone;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        CameraZoneManager.Instance.SetZone(zone);
    }
}