using UnityEngine;

public class EnergyCoreAnimation : MonoBehaviour
{
    public float hoverHeight = 0.2f;
    public float hoverSpeed = 2f;

    public float rotateSpeed = 90f;

    public float pulseSpeed = 2f;
    public float pulseAmount = 0.1f;

    Vector3 startPos;
    Vector3 baseScale;

    void Start()
    {
        startPos = transform.position;
        baseScale = transform.localScale;
    }

    void Update()
    {
        // hover
        float y = Mathf.Sin(Time.time * hoverSpeed) * hoverHeight;
        transform.position = startPos + new Vector3(0, y, 0);

        // rotate
        //transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);

        // pulse
        float scale = 1 + Mathf.Sin(Time.time * pulseSpeed) * pulseAmount;
        transform.localScale = baseScale * scale;
    }
}