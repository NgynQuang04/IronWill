using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject gameOverPanel;

    [Header("Scenes")]
    [SerializeField] private string mainMenuScene = "MainMenu";

    void OnEnable()
    {
        GameEvents.OnPlayerDeath += ShowGameOver;
    }

    void OnDisable()
    {
        GameEvents.OnPlayerDeath -= ShowGameOver;
    }

   /* void Start()
    {
        gameOverPanel.SetActive(false); // reset UI
        Time.timeScale = 1f;            // đảm bảo game chạy bình thường
    }*/

    void ShowGameOver()
    {
        gameOverPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(mainMenuScene);
    }
}