using UnityEngine;

public class AudioEventListener : MonoBehaviour
{
    void OnEnable()
    {
        GameEvents.OnGameWin += OnWin;
        GameEvents.OnPlayerDeath += OnLose;
        GameEvents.OnEnergyChanged += OnEnergyChanged;
    }

    void OnDisable()
    {
        GameEvents.OnGameWin -= OnWin;
        GameEvents.OnPlayerDeath -= OnLose;
        GameEvents.OnEnergyChanged -= OnEnergyChanged;
    }

    private void OnWin()
    {
        AudioManager.Instance.PlayMusic(AudioManager.Instance.winMusic);
    }

    private void OnLose()
    {
        AudioManager.Instance.PlayMusic(AudioManager.Instance.loseMusic);
    }

    private void OnEnergyChanged(int current, int max)
    {
        if (current > 0)
            AudioManager.Instance.PlayEnergyCollect();
    }
}