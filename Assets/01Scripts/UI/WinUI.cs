using UnityEngine;
using UnityEngine.SceneManagement;

public class WinUI : MonoBehaviour
{
    [SerializeField] GameObject winPanel;

    void OnEnable()
    {
        GameEvents.OnGameWin += ShowWin;
    }

    void OnDisable()
    {
        GameEvents.OnGameWin -= ShowWin;
    }

    void ShowWin()
    {
        winPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("MainMenu");
    }
}