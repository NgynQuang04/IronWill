using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [Header("Scene Names")]
    [SerializeField] private string firstLevelScene = "HD";

    public void PlayGame()
    {
        SceneManager.LoadScene(firstLevelScene);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

    public void OpenSettings()
    {
        Debug.Log("Open Settings");
    }
}