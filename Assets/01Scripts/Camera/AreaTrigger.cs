using UnityEngine;

public class AreaTrigger : MonoBehaviour
{
    [SerializeField] int areaID;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        GameEvents.OnAreaReached?.Invoke(areaID);
    }
}