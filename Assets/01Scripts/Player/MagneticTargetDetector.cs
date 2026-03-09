using UnityEngine;

public class MagneticTargetDetector : MonoBehaviour
{
    public float magneticRadius = 8f;
    public LayerMask metalLayer;

    public RaycastHit2D CurrentHit { get; private set; }

    public bool HasTarget => CurrentHit.collider != null;

    public void UpdateTarget(Vector2 origin, Vector2 direction)
    {
        CurrentHit = Physics2D.Raycast(
            origin,
            direction,
            magneticRadius,
            metalLayer
        );
    }
}