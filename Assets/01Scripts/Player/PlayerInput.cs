using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    Camera cam;

    [Header("Mode")]
    [SerializeField] private bool useMobileInput = false;

    [Header("Mobile")]
    [SerializeField] private Joystick joystick;

    public Vector2 MouseDirection { get; private set; }
    public bool Pull { get; private set; }
    public bool Push { get; private set; }

    // 👉 chỉ dùng cho mobile
    private bool mobilePull;
    private bool mobilePush;

    void Awake()
    {
        cam = Camera.main;
    }

    public void UpdateInput()
    {
        if (useMobileInput)
        {
            UpdateMobileInput();
        }
        else
        {
            UpdateMouseInput();
        }
    }

    // ================= PC =================
    void UpdateMouseInput()
    {
        Vector2 mouseWorld = cam.ScreenToWorldPoint(Input.mousePosition);
        MouseDirection = (mouseWorld - (Vector2)transform.position).normalized;

        Pull = Input.GetMouseButton(0);
        Push = Input.GetMouseButton(1);
    }

    // ================= MOBILE =================
    void UpdateMobileInput()
    {
        // joystick direction
        if (joystick != null)
        {
            Vector2 dir = new Vector2(joystick.Horizontal, joystick.Vertical);

            if (dir.magnitude > 0.1f)
                MouseDirection = dir.normalized;
        }

        // 👉 UI button
        bool uiPull = mobilePull;
        bool uiPush = mobilePush;

        // 👉 KEYBOARD TEST (QUAN TRỌNG)
        bool keyPull = Input.GetKey(KeyCode.A);
        bool keyPush = Input.GetKey(KeyCode.D);

        // 👉 kết hợp (UI hoặc phím đều được)
        Pull = uiPull || keyPull;
        Push = uiPush || keyPush;

        // 🚫 tránh conflict
        if (Pull && Push)
        {
            Push = false;
        }
    }

    // ================= UI BUTTON (ONLY MOBILE) =================
    public void OnPullDown()
    {
        if (!useMobileInput) return;
        mobilePull = true;
    }

    public void OnPullUp()
    {
        if (!useMobileInput) return;
        mobilePull = false;
    }

    public void OnPushDown()
    {
        if (!useMobileInput) return;
        mobilePush = true;
    }

    public void OnPushUp()
    {
        if (!useMobileInput) return;
        mobilePush = false;
    }
}