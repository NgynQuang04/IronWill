using UnityEngine;

public class UIGameOver : MonoBehaviour
{
    [SerializeField] GameObject panel;

    void Start()
    {
        GameEvents.OnPlayerDeath += Show;
    }

    void OnDestroy()
    {
        GameEvents.OnPlayerDeath -= Show;
    }

    void Show()
    {
        panel.SetActive(true);
        Time.timeScale = 0f;
    }
}