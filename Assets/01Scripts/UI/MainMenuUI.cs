using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject levelSelectPanel;

    private void Start()
    {
        // đảm bảo panel level tắt khi bắt đầu
        if (levelSelectPanel != null)
            levelSelectPanel.SetActive(false);
    }

    // ======================
    // MAIN MENU BUTTONS
    // ======================

    public void PlayGame()
    {
        SceneManager.LoadScene("LV1");
    }

    public void OpenLevelSelect()
    {
        if (levelSelectPanel != null)
            levelSelectPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");

        Application.Quit();
    }

    // ======================
    // LEVEL SELECT PANEL
    // ======================

    public void CloseLevelSelect()
    {
        if (levelSelectPanel != null)
        {
            levelSelectPanel.SetActive(false);
                Debug.Log("đóng");
        }

        
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("LV1");
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene("LV2");
    }
}