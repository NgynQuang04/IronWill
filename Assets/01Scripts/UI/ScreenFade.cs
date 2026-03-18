using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFade : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    [SerializeField] private float duration = 0.5f;

    public void FadeIn()
    {
        StartCoroutine(Fade(0, 1));
    }

    IEnumerator Fade(float start, float end)
    {
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;

            Color c = fadeImage.color;
            c.a = Mathf.Lerp(start, end, t);
            fadeImage.color = c;

            yield return null;
        }
    }
}