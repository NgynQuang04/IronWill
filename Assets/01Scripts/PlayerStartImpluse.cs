using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerStartImpulse : MonoBehaviour
{
    [Header("Start Impulse Settings")]
    public bool useStartImpulse = true;

    [Tooltip("Hướng bay ban đầu")]
    public Vector2 startDirection = new Vector2(1f, 0.2f);

    [Tooltip("Độ mạnh lực ban đầu")]
    public float startImpulse = 1.5f;

    [Tooltip("Delay trước khi phóng (0 = ngay lập tức)")]
    public float startDelay = 0f;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        if (!useStartImpulse) return;

        if (startDelay <= 0f)
        {
            ApplyImpulse();
        }
        else
        {
            Invoke(nameof(ApplyImpulse), startDelay);
        }
    }

    void ApplyImpulse()
    {
        rb.AddForce(startDirection.normalized * startImpulse, ForceMode2D.Impulse);
    }
}