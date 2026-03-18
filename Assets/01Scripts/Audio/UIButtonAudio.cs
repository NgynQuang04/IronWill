using UnityEngine;

public class UIButtonAudio : MonoBehaviour
{
    // Hàm này gọi từ Button → OnClick()
    public void PlayClick()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayButtonClick();
        }
    }
}