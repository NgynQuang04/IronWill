using UnityEngine;

public class UIWin : MonoBehaviour
{
    [SerializeField] GameObject panel;

    void Start()
    {
        GameEvents.OnGameWin += Show;
    }

    void OnDestroy()
    {
        GameEvents.OnGameWin -= Show;
    }

    void Show()
    {
        panel.SetActive(true);
        Time.timeScale = 0f;
    }
}