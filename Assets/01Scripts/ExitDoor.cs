using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor : MonoBehaviour
{
    public float landingRadius = 1.5f;
    public float maxLandingSpeed = 3f;

    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;

        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        MagneticMovement movement = player.GetComponent<MagneticMovement>();

        float distance = Vector2.Distance(player.transform.position, transform.position);

        if (distance < landingRadius &&
            rb.linearVelocity.magnitude < maxLandingSpeed &&
            movement.IsGrounded())
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