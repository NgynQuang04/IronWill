using UnityEngine;
using UnityEngine.SceneManagement;

public class WinUI : MonoBehaviour
{
    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject nextButton;
    [SerializeField] GameObject comingSoonText; 

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

        bool hasNextLevel = SceneManager.GetActiveScene().buildIndex + 1
                            < SceneManager.sceneCountInBuildSettings;

        if (hasNextLevel)
        {
            nextButton.SetActive(true);
            comingSoonText.SetActive(false);
        }
        else
        {
            nextButton.SetActive(false);
            comingSoonText.SetActive(true);
        }

        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;

        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextScene);
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}