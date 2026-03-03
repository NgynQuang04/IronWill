using UnityEngine;

public class MetalObject : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
}