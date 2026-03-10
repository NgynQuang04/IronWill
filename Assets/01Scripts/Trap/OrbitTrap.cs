using UnityEngine;

public class OrbitTrap : MonoBehaviour
{
    public Transform center;
    public float radius = 2f;
    public float speed = 1f;

    float angle;

    void Update()
    {
        angle += speed * Time.deltaTime;

        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;

        transform.position = center.position + new Vector3(x, y, 0);
    }
}