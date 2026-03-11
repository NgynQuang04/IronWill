using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform[] cameraPoints;
    [SerializeField] float speed = 3f;

    Transform target;

    void Start()
    {
        GameEvents.OnAreaReached += MoveCamera;
        target = cameraPoints[0];
    }

    void OnDestroy()
    {
        GameEvents.OnAreaReached -= MoveCamera;
    }

    void MoveCamera(int id)
    {
        target = cameraPoints[id];
    }

    void Update()
    {
        transform.position = Vector3.Lerp(
            transform.position,
            new Vector3(target.position.x, target.position.y, transform.position.z),
            speed * Time.deltaTime
        );
    }
}