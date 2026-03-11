using UnityEngine;

public class FloatingTrap : Trap
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;

    Vector3 target;

    void Start()
    {
        target = pointB.position;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            speed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, target) < 0.05f)
        {
            target = target == pointA.position ? pointB.position : pointA.position;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject); // hoặc gọi hàm chết
        }
    }
}