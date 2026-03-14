using UnityEngine;
using System.Collections;

public class PortalNotificationUI : MonoBehaviour
{
    [SerializeField] private GameObject notificationPanel;

    [SerializeField] private float showTime = 2.5f;

    void OnEnable()
    {
        GameEvents.OnPortalActivated += ShowNotification;
    }

    void OnDisable()
    {
        GameEvents.OnPortalActivated -= ShowNotification;
    }

    void ShowNotification()
    {
        StartCoroutine(ShowRoutine());
    }

    IEnumerator ShowRoutine()
    {
        notificationPanel.SetActive(true);

        yield return new WaitForSeconds(showTime);

        notificationPanel.SetActive(false);
    }
}