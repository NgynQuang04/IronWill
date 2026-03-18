using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    public Transform sprite;
    public float rotationSpeed = 10f;

    Vector2 currentLookDirection = Vector2.right;

    public void UpdateRotation(
    Rigidbody2D rb,
    MagneticTargetDetector detector,
    PlayerInput input)
    {
        Vector2 targetDir = Vector2.zero;

        // 1. Ưu tiên khi đang hút/đẩy
        if (detector.HasTarget && (input.Pull || input.Push))
        {
            Vector2 baseDir =
                (detector.CurrentHit.point - rb.position).normalized;

            targetDir = input.Pull ? baseDir : -baseDir;
        }
        // 2. Joystick direction (QUAN TRỌNG CHO MOBILE)
        else if (input.MouseDirection.magnitude > 0.1f)
        {
            targetDir = input.MouseDirection;
        }
        // 3. fallback: theo velocity
        else if (rb.linearVelocity.magnitude > 0.1f)
        {
            targetDir = rb.linearVelocity.normalized;
        }

        if (targetDir == Vector2.zero)
            return;

        currentLookDirection = Vector2.Lerp(
            currentLookDirection,
            targetDir,
            Time.deltaTime * rotationSpeed
        );

        float angle =
            Mathf.Atan2(currentLookDirection.y, currentLookDirection.x)
            * Mathf.Rad2Deg;

        sprite.rotation = Quaternion.Euler(0, 0, angle);
    }
}