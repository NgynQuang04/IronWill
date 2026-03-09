using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    Camera cam;

    public Vector2 MouseDirection { get; private set; }
    public bool Pull { get; private set; }
    public bool Push { get; private set; }

    void Awake()
    {
        cam = Camera.main;
    }

    public void UpdateInput()
    {
        Vector2 mouseWorld = cam.ScreenToWorldPoint(Input.mousePosition);
        MouseDirection = (mouseWorld - (Vector2)transform.position).normalized;

        Pull = Input.GetMouseButton(0);
        Push = Input.GetMouseButton(1);
    }
}