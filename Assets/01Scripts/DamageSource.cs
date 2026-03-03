using UnityEngine;

public class DamageSource : MonoBehaviour
{
    [Header("Damage Settings")]
    public float baseDamageMultiplier = 1f;
    public float flatDamage = 0f; // nếu muốn damage cố định

    public bool ignoreVelocity = false;
    // true = dùng flatDamage
    // false = scale theo va chạm
}