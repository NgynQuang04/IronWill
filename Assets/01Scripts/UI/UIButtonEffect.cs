using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonEffect : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    Vector3 originalScale;
    Coroutine scaleRoutine;

    void Awake()
    {
        originalScale = transform.localScale;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ScaleTo(originalScale * 0.9f);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ScaleTo(originalScale);
    }

    void ScaleTo(Vector3 target)
    {
        if (scaleRoutine != null)
            StopCoroutine(scaleRoutine);

        scaleRoutine = StartCoroutine(AnimateScale(target));
    }

    System.Collections.IEnumerator AnimateScale(Vector3 target)
    {
        float time = 0;
        Vector3 start = transform.localScale;

        while (time < 0.1f)
        {
            time += Time.deltaTime;
            transform.localScale = Vector3.Lerp(start, target, time / 0.1f);
            yield return null;
        }

        transform.localScale = target;
    }
}