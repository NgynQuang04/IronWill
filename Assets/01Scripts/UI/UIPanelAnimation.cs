using UnityEngine;
using System.Collections;

public class UIPanelAnimation : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private float animationTime = 0.25f;

    private Coroutine currentAnim;

    private void Start()
    {
        panel.transform.localScale = Vector3.zero;
        panel.SetActive(false);
    }

    public void OpenPanel()
    {
        panel.SetActive(true);

        if (currentAnim != null)
            StopCoroutine(currentAnim);

        currentAnim = StartCoroutine(ScalePanel(Vector3.zero, Vector3.one));
    }

    public void ClosePanel()
    {
        if (currentAnim != null)
            StopCoroutine(currentAnim);

        currentAnim = StartCoroutine(CloseRoutine());
    }

    IEnumerator CloseRoutine()
    {
        yield return ScalePanel(Vector3.one, Vector3.zero);
        panel.SetActive(false);
    }

    IEnumerator ScalePanel(Vector3 start, Vector3 end)
    {
        float time = 0f;

        while (time < animationTime)
        {
            time += Time.deltaTime;
            float t = time / animationTime;

            // ease-out
            t = 1 - Mathf.Pow(1 - t, 3);

            panel.transform.localScale = Vector3.Lerp(start, end, t);

            yield return null;
        }

        panel.transform.localScale = end;
    }
}