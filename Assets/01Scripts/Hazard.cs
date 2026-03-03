using UnityEngine;

[RequireComponent(typeof(DamageSource))]
public class Hazard : MonoBehaviour
{
    void Awake()
    {
        DamageSource ds = GetComponent<DamageSource>();
        ds.ignoreVelocity = true;
        ds.flatDamage = 69f;
    }
}