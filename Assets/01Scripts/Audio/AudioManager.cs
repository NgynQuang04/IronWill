using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Background Music")]
    public AudioClip mainMenuMusic;
    public AudioClip winMusic;
    public AudioClip loseMusic;
    public AudioClip lv1Music;
    public AudioClip lv2Music;

    [Header("UI Sounds")]
    public AudioClip buttonClick;
    public AudioClip energyCollect;

    public AudioSource musicSource;
    public AudioSource sfxSource;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu")
            PlayMusic(mainMenuMusic);
        else if (scene.name.StartsWith("LV1"))
            PlayMusic(lv1Music);
        else if (scene.name.StartsWith("LV2"))
            PlayMusic(lv2Music);
    }

    // 🔹 Music control cơ bản
    public void PlayMusic(AudioClip clip)
    {
        if (clip == null) return;
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    

    // 🔹 SFX cơ bản
    public void PlaySFX(AudioClip clip)
    {
        if (clip == null || sfxSource == null || !sfxSource.gameObject.activeInHierarchy) return;
        sfxSource.PlayOneShot(clip);
    }

    public void PlayButtonClick()
    {
        PlaySFX(buttonClick);
    }

    public void PlayEnergyCollect()
    {
        PlaySFX(energyCollect);
    }

    public void WinMusic()
    {
        PlaySFX(winMusic);
    }

    public void LoseMusic()
    {
        PlaySFX(loseMusic);
    }
}