using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor : MonoBehaviour
{
    [Header("Landing Settings")]
    public float landingRadius = 1.5f;
    public float maxLandingSpeed = 3f;

    Rigidbody2D playerRb;
    //GroundChecker groundChecker;
    Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (player == null) return;

        playerRb = player.GetComponent<Rigidbody2D>();
        //groundChecker = player.GetComponent<GroundChecker>();
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(player.position, transform.position);

        if (distance < landingRadius &&
            playerRb.linearVelocity.magnitude < maxLandingSpeed )
            //&&
            //groundChecker != null &&
            //groundChecker.IsGrounded())
        {
            LoadNextLevel();
        }
    }

    void LoadNextLevel()
    {
        int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextIndex);
        }
        else
        {
            Debug.Log("No more levels!");
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, landingRadius);
    }
}